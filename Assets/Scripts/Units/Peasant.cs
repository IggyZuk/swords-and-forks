﻿using UnityEngine;
using UnityEngine.UI;
using Momentum;
using System.Collections.Generic;

public class Peasant : MonoBehaviour
{
    [SerializeField] Image body;
    [SerializeField] Image outline;
    [SerializeField] Image resourceImage;

    Pos pos = new Pos();
    CommanderID comID;

    Resource resource = Resource.None;
    int hunger = 10;
    Task movementTask;

    public void Init(int x, int y, CommanderID comID)
    {
        pos.x = x;
        pos.y = y;

        this.comID = comID;

        body.color = Controller.Instance.commanders[comID].color;

        resourceImage.enabled = false;

        List<Pos> path = new List<Pos>();

        Task.Add().Loop(-1).OnRepeat(t =>
        {
            if (movementTask != null) return;

            if (resource != Resource.None)
            {
                //TODO: find closes windmill

                if (path.Count > 0)
                {
                    MoveTo(path[0]);
                    path.RemoveAt(0);
                    return;
                }

                Tile windmill = Controller.Instance.grid.FindClosestTileWithEntity(pos, typeof(Windmill), comID);
                path = CalculatePath(pos, windmill.pos);
                path.ForEach(g => Debug.Log(g));

                if (path.Count > 0)
                {
                    MoveTo(path[0]);
                    path.RemoveAt(0);
                    return;
                }
            }

            // Move to a random neighbour
            Pos[] neighbours = Controller.Instance.grid.GetNeighbours(pos);
            MoveTo(neighbours[Random.Range(0, neighbours.Length)]);
        });
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

               hunger--;
               //if (hunger <= 0) Object.Destroy(this.gameObject);

               if (targetTile.Entity is Windmill)
               {
                   if (targetTile.Entity.comID == comID)
                   {
                       if (resource != Resource.None)
                       {
                           DropResource();
                       }
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
