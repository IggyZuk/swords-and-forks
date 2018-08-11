using UnityEngine;
using Momentum;

public class Wheat : Entity
{
    Tile tile;
    Task task;

    public void Init(Tile tile)
    {
        this.tile = tile;

        Sprite[] wheatProgressSprites = new Sprite[]
        {
            Assets.Wheat_0,
            Assets.Wheat_1,
            Assets.Wheat_2
        };

        task = Task.Add()
            .Time(5f)
            .Random(4f)
            .OnUpdate(t => tile.SetEntitySprite(wheatProgressSprites[(int)(Mathf.Clamp(t.progress * 2f, 0, 2))]))
            .OnComplete(_ => tile.Color = Config.colors.yellow);
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
