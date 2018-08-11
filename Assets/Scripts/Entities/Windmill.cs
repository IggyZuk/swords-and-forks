using UnityEngine;
using Momentum;

public class Windmill : Entity
{
    Tile tile;
    Task task;

    public void Init(Tile tile)
    {
        this.tile = tile;

        task = Task.Add()
            .Time(1f)
            .Loop(-1)
            .Random(0.5f)
            .OnRepeat(_ =>
            {
                Pos[] neighbours = {
                    new Pos(tile.pos.x - 1, tile.pos.y),
                    new Pos(tile.pos.x + 1, tile.pos.y),
                    new Pos(tile.pos.x, tile.pos.y - 1),
                    new Pos(tile.pos.x, tile.pos.y + 1),
                    new Pos(tile.pos.x - 1, tile.pos.y - 1),
                    new Pos(tile.pos.x + 1, tile.pos.y - 1),
                    new Pos(tile.pos.x - 1, tile.pos.y + 1),
                    new Pos(tile.pos.x + 1, tile.pos.y + 1),
                };

                Pos randomNeighbour = neighbours[Random.Range(0, neighbours.Length)];

                Builder.Build(new Wheat(), randomNeighbour.x, randomNeighbour.y);
            });
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
        return Assets.windmill;
    }

}
