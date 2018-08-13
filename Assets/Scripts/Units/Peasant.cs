using UnityEngine;
using UnityEngine.UI;
using Momentum;
using System.Collections.Generic;

public class Peasant : MonoBehaviour
{
    [SerializeField] Image body;
    [SerializeField] Image outline;
    [SerializeField] Image resourceImage;

    bool isActive = true;
    Pos pos = new Pos();
    CommanderID comID;

    int hunger = Config.HungerMax;
    int energy = Config.EnergyMax;
    Resource resource = Resource.None;

    Task updateTask;
    Task movementTask;

    public void Init(int x, int y, CommanderID comID)
    {
        pos.x = x;
        pos.y = y;

        this.comID = comID;

        body.color = Controller.Instance.commanders[comID].color;

        resourceImage.enabled = false;

        List<Pos> path = new List<Pos>();

        updateTask = Task.Add().Name("Peasant(tick)").Loop(-1).OnRepeat(t =>
        {
            if (movementTask != null) return;

            if (path.Count > 0)
            {
                MoveTo(path[0]);
                path.RemoveAt(0);
                return;
            }

            path = PickNextPath();

            if (path.Count > 0)
            {
                MoveTo(path[0]);
                path.RemoveAt(0);
                return;
            }

            MoveToRandomNeighbour();

            if (!isActive)
            {
                Core.Juggler.Remove(updateTask);
                if (movementTask != null)
                {
                    Core.Juggler.Remove(movementTask);
                }
                Object.Destroy(this.gameObject);

                Hatchery.SpawnEffect(pos.x, pos.y, Config.colors.pink);
            }
        });
    }

    List<Pos> PickNextPath()
    {
        if (resource == Resource.None)
        {
            Pos[] ns = Controller.Instance.grid.GetNeighbours(pos);
            foreach (var n in ns)
            {
                Tile nt = Controller.Instance.grid.GetTile(n);
                if (nt.Entity is Lumber)
                {
                    if (((Lumber)nt.Entity).isGrown)
                    {
                        return CalculatePath(pos, nt.pos);
                    }
                }
                else if (nt.Entity is Wheat)
                {
                    if (((Wheat)nt.Entity).isGrown)
                    {
                        return CalculatePath(pos, nt.pos);
                    }
                }
            }
        }
        else if (resource == Resource.Lumber)
        {
            Tile t = Controller.Instance.grid.FindClosestTileWithEntity<Lumberyard>(pos, comID);
            if (t == null)
            {
                DropResource();
                return new List<Pos>();
            }

            return CalculatePath(pos, t.pos);
        }
        else if (resource == Resource.Wheat)
        {
            Tile t = Controller.Instance.grid.FindClosestTileWithEntity<Windmill>(pos, comID);
            if (t == null)
            {
                DropResource();
                return new List<Pos>();
            }

            return CalculatePath(pos, t.pos);
        }

        return new List<Pos>();
    }

    public List<Pos> CalculatePath(Pos from, Pos to)
    {
        List<Pos> path = new List<Pos>();

        Pos currentPos = from;

        while (currentPos.x != to.x || currentPos.y != to.y)
        {
            Pos nextPos = new Pos(
                Mathf.Clamp(to.x - currentPos.x, -1, 1),
                Mathf.Clamp(to.y - currentPos.y, -1, 1)
            );

            currentPos.x += nextPos.x;
            currentPos.y += nextPos.y;

            path.Add(currentPos);
        }

        return path;
    }

    void MoveToRandomNeighbour()
    {
        Pos[] neighbours = Controller.Instance.grid.GetNeighbours(pos);
        MoveTo(neighbours[Random.Range(0, neighbours.Length)]);
    }

    public void MoveTo(Pos targetPos)
    {
        if (movementTask != null) return;

        Tile originTile = Controller.Instance.grid.GetTile(pos);
        Tile targetTile = Controller.Instance.grid.GetTile(targetPos);

        Vector2 origin = originTile.transform.position;
        Vector2 target = targetTile.transform.position;

        movementTask = Task.Add()
           .Name("Peasant (move)")
           .Time(1f)
           .Random(0.25f)
           .OnUpdate(t =>
           {
               this.transform.position = Vector2.LerpUnclamped(origin, target, Ease.InOutBack(t.Progress));
           })
           .OnComplete(t =>
           {
               pos = targetPos;

               movementTask = null;

               //targetTile.Glow(Controller.Instance.commanders[comID].color);

               if (resource == Resource.Lumber)
               {
                   if (targetTile.Entity is Lumberyard && targetTile.Entity.comID == comID)
                   {
                       ((Lumberyard)targetTile.Entity).ProcessLumber();
                       DropResource();
                   }
               }
               else if (resource == Resource.Wheat)
               {
                   if (targetTile.Entity is Windmill && targetTile.Entity.comID == comID)
                   {
                       ((Windmill)targetTile.Entity).ProcessWheat();
                       DropResource();
                   }
               }


               if (resource != Resource.None) return;

               if (targetTile.Entity is Lumber)
               {
                   if (((Lumber)targetTile.Entity).isGrown)
                   {
                       targetTile.Entity = null;
                       TakeResource(Resource.Lumber);
                   }
               }
               else if (targetTile.Entity is Wheat)
               {
                   if (((Wheat)targetTile.Entity).isGrown)
                   {
                       targetTile.Entity = null;
                       TakeResource(Resource.Wheat);
                   }
               }

               DoHunger();
           });
    }

    void DoHunger()
    {
        //body.color = Color.Lerp(
        //    Config.colors.black,
        //    Controller.Instance.commanders[comID].color,
        //    (float)hunger / Config.HungerMax
        //);

        if (--hunger <= 0)
        {
            if (Controller.Instance.commanders[comID].wheat > 0)
            {
                hunger += Config.HungerMax;

                if (comID == CommanderID.Player)
                {
                    Controller.Instance.UI.AddResourceBit(
                         Resource.Wheat,
                         Controller.Instance.UI.Wheat.position,
                         this.transform.position,
                         () => Controller.Instance.commanders[comID].RemoveWheat()
                     );
                }
                else
                {
                    Controller.Instance.commanders[comID].RemoveWheat();
                }
            }
            else
            {
                isActive = false;
            }
        }

        if (--energy <= 0)
        {
            // TODO: go to townhall and rest
        }
    }

    public void TakeResource(Resource resource)
    {
        this.resource = resource;

        resourceImage.enabled = true;

        switch (resource)
        {
            case Resource.Lumber:
            resourceImage.sprite = Assets.Lumber4;
            resourceImage.color = Config.colors.green;
            break;
            case Resource.Wheat:
            resourceImage.sprite = Assets.Wheat4;
            resourceImage.color = Config.colors.yellow;
            break;

        }
    }

    public void DropResource()
    {
        resource = Resource.None;

        resourceImage.enabled = false;
    }
}
