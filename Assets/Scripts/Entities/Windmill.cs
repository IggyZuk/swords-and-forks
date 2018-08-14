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

        animTask = Task.Run()
           .Name("Windmill(anim)")
           .Time(0.25f)
           .Loop(-1)
           .OnRepeat(t =>
           {
               tile.SetEntitySprite(animSprites[animIndex]);

               animIndex++;
               animIndex = animIndex %= animSprites.Length;
           });

        plantWheatTask = Task.Run()
            .Name("Windmill(produce)")
            .Time(5f)
            .Loop(-1)
            .Random(2.5f)
            .OnRepeat(_ =>
            {
                Pos[] neighbours = Controller.Instance.grid.GetNeighbours(tile.pos);

                Pos randomNeighbour = neighbours[Random.Range(0, neighbours.Length)];

                Wheat w = new Wheat();
                w.level = level;

                Builder.Build(w, randomNeighbour.x, randomNeighbour.y);
            });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(animTask);
        Core.Juggler.Remove(plantWheatTask);
    }

    public override Sprite GetSprite()
    {
        return Assets.Windmill0;
    }

    public override int GetPrice()
    {
        return Config.prices.windmill;
    }

    public void ProcessWheat()
    {
        int count = 2 * level;

        if (comID == CommanderID.Player)
        {
            Task.Run().Time(0.03f).Random(0.015f).Loop(count).OnRepeat(_ =>
            {
                Controller.Instance.UI.AddResourceBit(
                    Resource.Wheat,
                    tile.transform.position,
                    Controller.Instance.UI.Wheat.position,
                    () => Controller.Instance.commanders[comID].AddWheat()
                );
            });
        }
        else
        {
            Controller.Instance.commanders[comID].AddWheat(count);
        }
    }
}
