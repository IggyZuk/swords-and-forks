using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class Peasant : MonoBehaviour
{
    [SerializeField] Image body;
    [SerializeField] Image outline;
    [SerializeField] Image resourceImage;

    Pos pos = new Pos();
    CommanderID comID;

    Resource resource = Resource.None;
    Task movementTask;

    public void Init(int x, int y, CommanderID comID)
    {
        pos.x = x;
        pos.y = y;

        this.comID = comID;

        body.color = Controller.Instance.commanders[comID].color;

        resourceImage.enabled = false;

        Task.Add().Loop(-1).OnRepeat(t =>
        {
            if (movementTask != null) return;

            Pos[] neighbours = Controller.Instance.grid.GetNeighbours(pos);

            MoveTo(neighbours[Random.Range(0, neighbours.Length)]);
        });
    }

    public void MoveTo(Pos targetPos)
    {
        if (movementTask != null) return;

        Tile originTile = Controller.Instance.grid.GetTile(pos);
        Tile targetTile = Controller.Instance.grid.GetTile(targetPos);

        Vector2 origin = originTile.transform.position;
        Vector2 target = targetTile.transform.position;

        pos = targetPos;

        movementTask = Task.Add()
           .Time(1f)
           .Random(0.25f)
           .OnUpdate(t =>
           {
               this.transform.position = Vector2.LerpUnclamped(origin, target, Ease.InOutBack(t.progress));
           })
           .OnComplete(t =>
           {
               movementTask = null;

               if (targetTile.Entity is Windmill)
               {
                   if (resource != Resource.None)
                   {
                       DropResource();
                   }
               }

               if (resource != Resource.None) return;

               if (targetTile.Entity is Wheat)
               {
                   Wheat wheat = targetTile.Entity as Wheat;
                   if (wheat.isGrown)
                   {
                       targetTile.Entity = null;
                       TakeWheat();
                   }
               }
           });
    }

    public void TakeWheat()
    {
        resource = Resource.Wheat;

        resourceImage.enabled = true;
        resourceImage.sprite = Assets.Wheat_2;
        resourceImage.color = Config.colors.yellow;
    }

    public void DropResource()
    {
        // TODO: add resource to commander

        resource = Resource.None;

        resourceImage.enabled = false;
    }
}
