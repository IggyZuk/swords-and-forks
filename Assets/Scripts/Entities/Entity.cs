using UnityEngine;

public abstract class Entity
{
    public Tile tile;
    public CommanderID comID;
    public int level = 0;

    public abstract void Init();
    public abstract void Deinit();
    public abstract void Tick();
    public abstract Sprite GetSprite();
}
