using UnityEngine;
using Momentum;

public class Lumberyard : Entity
{
    Task plantLumberTask;

    public override void Init()
    {
        plantLumberTask = Task.Add()
            .Time(7f)
            .Random(2.5f)
            .Loop(-1)
            .OnRepeat(_ =>
            {
                Pos[] neighbours = Controller.Instance.grid.GetNeighbours(tile.pos);

                Pos randomNeighbour = neighbours[Random.Range(0, neighbours.Length)];

                Builder.Build(new Lumber(), randomNeighbour.x, randomNeighbour.y);
            });

        level = 5;
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(plantLumberTask);
    }

    public override Sprite GetSprite()
    {
        return Assets.Lumberyard;
    }

    public void ProcessLumber()
    {
        int count = level + 1;

        if (comID == CommanderID.Player)
        {
            Task.Add().Time(0.03f).Random(0.015f).Loop(count).OnRepeat(_ =>
            {
                Controller.Instance.UI.AddResourceBit(
                    Resource.Lumber,
                    tile.transform.position,
                    Controller.Instance.UI.Lumber.position,
                    () => Controller.Instance.commanders[comID].AddLumber()
                );
            });
        }
        else
        {
            Controller.Instance.commanders[comID].AddLumber(count);
        }
    }
}
