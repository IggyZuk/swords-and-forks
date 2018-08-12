using UnityEngine;
using Momentum;

public class Townhall : Entity
{
    Task lumberProductionTask;
    Task wheatProductionTask;

    public override void Init()
    {
        Hatchery.SpawnPeasant(tile.pos.x, tile.pos.y, comID);

        lumberProductionTask = Task.Add().Time(25f - Mathf.Clamp(level, 0, 20)).Loop(-1).OnRepeat(_ =>
        {
            if (comID == CommanderID.Player)
            {
                Task.Add().Time(0.03f).Random(0.015f).Loop(level).OnRepeat(__ =>
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
                Controller.Instance.commanders[comID].AddLumber(level);
            }
        });

        wheatProductionTask = Task.Add().Time(10f - Mathf.Clamp(level, 0, 8)).Loop(-1).OnRepeat(_ =>
        {
            if (comID == CommanderID.Player)
            {
                Task.Add().Time(0.03f).Random(0.015f).Loop(level).OnRepeat(__ =>
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
                Controller.Instance.commanders[comID].AddWheat(level);
            }
        });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(lumberProductionTask);
        Core.Juggler.Remove(wheatProductionTask);
    }

    public override Sprite GetSprite()
    {
        return Assets.Townhall;
    }

    public override int GetPrice()
    {
        return Config.prices.townhall;
    }
}
