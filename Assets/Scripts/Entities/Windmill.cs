using UnityEngine;
using Momentum;

public class Windmill : Entity
{
    Task wheatGrowingTask;
    Task animationTask;

    public override void Init()
    {
        animationTask = Task.Add()
           .Time(0.5f)
           .Loop(-1)
           .OnRepeat(t => tile.SetEntitySprite(t.currentLoop % 2 == 0 ? Assets.Windmill_0 : Assets.Windmill_1));

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
        return Assets.Windmill_0;
    }

}
