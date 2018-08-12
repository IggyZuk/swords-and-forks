using UnityEngine;
using Momentum;

public class Castle : Entity
{
    public override void Init()
    {
    }

    public override void Deinit()
    {
    }

    public override Sprite GetSprite()
    {
        return Assets.Castle;
    }

    public override int GetPrice()
    {
        return Config.prices.castle;
    }

}
