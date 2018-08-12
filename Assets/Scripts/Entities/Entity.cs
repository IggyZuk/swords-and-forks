using UnityEngine;

public abstract class Entity
{
    public Tile tile;
    public CommanderID comID;
    public int level = 1;

    public abstract void Init();
    public abstract void Deinit();
    public abstract Sprite GetSprite();
    public virtual int GetPrice() { return 0; }
}
