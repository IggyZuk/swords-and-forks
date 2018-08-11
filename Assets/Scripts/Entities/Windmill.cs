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
            .Time(1)
            .Loop(-1)
            .OnRepeat(_ =>
            {
                Debug.Log(_);

                Tile t1 = Controller.Instance.grid.GetTile(tile.pos.x, tile.pos.y - 1);
                if (t1 != null)
                {
                    t1.Entity = new Windmill();
                }
                Tile t2 = Controller.Instance.grid.GetTile(tile.pos.x, tile.pos.y + 1);
                if (t2 != null)
                {
                    t2.Entity = new Windmill();
                }
                Tile t3 = Controller.Instance.grid.GetTile(tile.pos.x - 1, tile.pos.y);
                if (t3 != null)
                {
                    t3.Entity = new Windmill();
                }
                Tile t4 = Controller.Instance.grid.GetTile(tile.pos.x + 1, tile.pos.y);
                if (t4 != null)
                {
                    t4.Entity = new Windmill();
                }
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
