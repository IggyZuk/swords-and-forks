using UnityEngine;
using Momentum;

public class Lumber : Entity
{
    Task task;

    public bool isGrown = false;

    public override void Init()
    {
        tile.SetEntityColor(Config.colors.neutral);

        Sprite[] animSprites =
        {
            Assets.Lumber0,
            Assets.Lumber1,
            Assets.Lumber2,
            Assets.Lumber3,
            Assets.Lumber4
        };

        task = Task.Add()
            .Time(40f)
            .Random(10f)
            .OnUpdate(t => tile.SetEntitySprite(animSprites[
                (int)(Mathf.Clamp(t.progress * animSprites.Length - 1, 0, animSprites.Length - 1))
            ]))
            .OnComplete(_ =>
            {
                isGrown = true;
                tile.SetEntityColor(Config.colors.green);
                Task.Add().Time(99999f).OnComplete(__ => tile.Entity = null);
            });
    }

    public override void Deinit()
    {
        Core.Juggler.Remove(task);
    }

    public override Sprite GetSprite()
    {
        return Assets.Lumber0;
    }

}
