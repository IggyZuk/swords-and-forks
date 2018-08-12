using UnityEngine;
using Momentum;

public class Windmill : Entity
{
    Task plantWheatTask;
    Task animTask;

    public override void Init()
    {
        int animIndex = 0;
        Sprite[] animSprites =
        {
            Assets.Windmill0,
            Assets.Windmill1,
            Assets.Windmill2
        };

        animTask = Task.Add()
           .Time(0.25f)
           .Loop(-1)
           .OnRepeat(t =>
           {
               tile.SetEntitySprite(animSprites[animIndex]);

               animIndex++;
               animIndex = animIndex %= animSprites.Length;
           });

        plantWheatTask = Task.Add()
            .Time(5f)
            .Loop(-1)
            .Random(2.5f)
            .OnRepeat(_ =>
            {
                Pos[] neighbours = Controller.Instance.grid.GetNeighbours(tile.pos);

                Pos randomNeighbour = neighbours[Random.Range(0, neighbours.Length)];

                Builder.Build(new Wheat(), randomNeighbour.x, randomNeighbour.y);
            });

        level = 5;
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(animTask);
        Core.Juggler.Remove(plantWheatTask);
    }

    public override void Tick()
    {
    }

    public override Sprite GetSprite()
    {
        return Assets.Windmill0;
    }

    public void ProcessWheat()
    {
        int count = level + 1;

        if (comID == CommanderID.Player)
        {
            Task.Add().Time(0.03f).Random(0.015f).Loop(count).OnRepeat(_ =>
            {
                Controller.Instance.UI.AddResourceBit(
                    Resource.Wheat,
                    tile.transform.position,
                    Controller.Instance.UI.Wheat.position,
                    () => Controller.Instance.commanders[comID].AddWheat(count)
                );
            });
        }
        else
        {
            Controller.Instance.commanders[comID].AddWheat(count);
        }
    }
}
