using UnityEngine;
using Momentum;

public class Wheat : Entity
{
    Task task;

    public bool isGrown = false;

    public override void Init()
    {
        tile.SetEntityColor(Config.colors.neutral);

        Sprite[] animSprites =
        {
            Assets.Wheat0,
            Assets.Wheat1,
            Assets.Wheat2,
            Assets.Wheat3,
            Assets.Wheat4
        };

        task = Task.Add()
            .Time(10f)
            .Random(5f)
            .OnUpdate(t => tile.SetEntitySprite(animSprites[
                (int)(Mathf.Clamp(t.progress * animSprites.Length - 1, 0, animSprites.Length - 1))
            ]))
            .OnComplete(_ =>
            {
                isGrown = true;
                tile.SetEntityColor(Config.colors.yellow);
            });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(task);
    }

    public override Sprite GetSprite()
    {
        return Assets.Wheat0;
    }

}
