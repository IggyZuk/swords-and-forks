using UnityEngine;
using Momentum;

public class Tower : Entity
{
    public override void Init()
    {
    }

    public override void Deinit()
    {
    }

    public override Sprite GetSprite()
    {
        return Assets.Tower;
    }

    public override int GetPrice()
    {
        return Config.prices.tower;
    }
}
