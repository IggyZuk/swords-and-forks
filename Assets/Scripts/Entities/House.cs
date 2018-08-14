using UnityEngine;
using Momentum;

public class House : Entity
{
    Peasant peasant;
    Task updateTask;

    public override void Init()
    {
        updateTask = Task.Run().Time(15).Random(15).Loop(-1).OnRepeat(_ =>
        {
            if (peasant == null)
            {
                peasant = Hatchery.SpawnPeasant(tile.pos.x, tile.pos.y, comID);
            }
        });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(updateTask);
    }

    public override Sprite GetSprite()
    {
        return Assets.House;
    }

    public override int GetPrice()
    {
        return Config.prices.house;
    }
}
