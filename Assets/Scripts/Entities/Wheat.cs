using UnityEngine;
using Momentum;

public class Wheat : Entity
{
    Tile tile;
    Task task;

    public void Init(Tile tile)
    {
        this.tile = tile;

        task = Task.Add()
            .Time(1f)
            .OnUpdate(_ => Debug.Log("Growing..."))
            .OnComplete(_ => Debug.Log("Grown!"));
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
        return Assets.wheat;
    }

}
