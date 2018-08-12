using UnityEngine;
using Momentum;

public class Windmill : Entity
{
    Task wheatGrowingTask;
    Task animationTask;

    public override void Init()
    {
        int animIndex = 0;
        Sprite[] animSprites =
        {
            Assets.Windmill0,
            Assets.Windmill1,
            Assets.Windmill2
        };

        animationTask = Task.Add()
           .Time(0.25f)
           .Loop(-1)
           .OnRepeat(t =>
           {
               tile.SetEntitySprite(animSprites[animIndex]);

               animIndex++;
               animIndex = animIndex %= animSprites.Length;
           });

        wheatGrowingTask = Task.Add()
            .Time(5f)
            .Loop(-1)
            .Random(2.5f)
            .OnRepeat(_ =>
            {
                Pos[] neighbours = Controller.Instance.grid.GetNeighbours(tile.pos);

                Pos randomNeighbour = neighbours[Random.Range(0, neighbours.Length)];

                Builder.Build(new Wheat(), randomNeighbour.x, randomNeighbour.y, CommanderID.None);
            });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(animationTask);
        Core.Juggler.Remove(wheatGrowingTask);
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

        Controller.Instance.commanders[comID].AddWheat(count);

        if (comID == CommanderID.Player)
        {
            for (int i = 0; i < count; i++)
            {
                Controller.Instance.UI.AddResourceBit(Resource.Wheat, tile.pos);
            }
        }
    }
}
