using UnityEngine;

public interface Entity
{
    void Init(Tile tile);
    void Deinit();
    void Tick();
    Sprite GetSprite();
}
