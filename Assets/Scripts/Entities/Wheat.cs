using UnityEngine;
using Momentum;

public class Wheat : Entity
{
    Tile tile;
    Task task;

    public bool isGrown = false;

    public void Init(Tile tile)
    {
        this.tile = tile;

        tile.SetEntityColor(Config.colors.neutral);

        Sprite[] wheatProgressSprites = 
        {
            Assets.Wheat_0,
            Assets.Wheat_1,
            Assets.Wheat_2
        };

        task = Task.Add()
            .Time(10f)
            .Random(5f)
            .OnUpdate(t => tile.SetEntitySprite(wheatProgressSprites[(int)(Mathf.Clamp(t.progress * 2f, 0, 2))]))
            .OnComplete(_ =>
            {
                isGrown = true;
                tile.SetEntityColor(Config.colors.yellow);
                Task.Add().Time(500f).OnComplete(__ => tile.Entity = null);
            });
    }

    public void Deinit()
    {
        Core.Juggler.Remove(task);
    }

    public void Tick()
    {
    }

    public Sprite GetSprite()
    {
        return Assets.Wheat_0;
    }

}
