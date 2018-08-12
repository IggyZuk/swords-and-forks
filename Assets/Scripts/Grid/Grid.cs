using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Grid : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    Tile[,] tiles;

    public void Build(int w, int h, int tileSize)
    {
        tiles = new Tile[w, h];

        rectTransform.sizeDelta = new Vector2(w * tileSize, h * tileSize);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Tile tile = Instantiate(Assets.Tile);
                tiles[x, y] = tile;
                tile.pos = new Pos(x, y);
                tile.transform.SetParent(this.transform, false);

                tile.Entity = null;
                tile.Border = Config.colors.neutral;
                tile.Level = 0;
            }
        }

        Canvas.ForceUpdateCanvases();
    }

    public Tile GetTile(Pos pos)
    {
        return GetTile(pos.x, pos.y);
    }

    public Tile GetTile(int x, int y)
    {
        if (x < 0 ||
           y < 0 ||
           x >= tiles.GetLength(0) ||
           y >= tiles.GetLength(1))
        {
            return null;
        }

        return tiles[x, y];
    }

    public Pos[] GetNeighbours(Pos pos)
    {
        return GetNeighbours(pos.x, pos.y);
    }

    public Pos[] GetNeighbours(int x, int y)
    {
        int w = tiles.GetLength(0);
        int h = tiles.GetLength(1);

        List<Pos> neighbours = new List<Pos>();

        if (x - 1 >= 0) neighbours.Add(new Pos(x - 1, y));
        if (x + 1 < w) neighbours.Add(new Pos(x + 1, y));
        if (y - 1 >= 0) neighbours.Add(new Pos(x, y - 1));
        if (y + 1 < h) neighbours.Add(new Pos(x, y + 1));

        if (x - 1 >= 0 && y - 1 >= 0) neighbours.Add(new Pos(x - 1, y - 1));
        if (x + 1 < w && y - 1 >= 0) neighbours.Add(new Pos(x + 1, y - 1));
        if (x - 1 >= 0 && y + 1 < h) neighbours.Add(new Pos(x - 1, y + 1));
        if (x + 1 < w && y + 1 < h) neighbours.Add(new Pos(x + 1, y + 1));

        return neighbours.ToArray();

    }

    // TODO: needs to find the closest tile
    public Tile FindClosestTileWithEntity(Pos pos, System.Type entityType, CommanderID comID)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Entity == null) continue;
            if (tile.Entity.comID != comID) continue;

            if (tile.Entity.GetType().IsAssignableFrom(entityType))
            {
                return tile;
            }
        }
        return null;
    }

    public void TickAll()
    {
        foreach (Tile t in tiles)
        {
            t.Tick();
        }
    }
}
