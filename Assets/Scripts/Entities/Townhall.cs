using UnityEngine;
using Momentum;

public class Townhall : Entity
{
    public override void Init()
    {
        Hatchery.SpawnPeasant(tile.pos.x, tile.pos.y, comID);
    }

    public override void Deinit()
    {
    }

    public override Sprite GetSprite()
    {
        return Assets.Townhall;
    }

}
