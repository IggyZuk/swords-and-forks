using UnityEngine;

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
                tile.transform.SetParent(this.transform);

                tile.Entity = null;
                tile.Color = Config.colors.neutral;
                tile.Level = 0;
            }
        }
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

    public void TickAll()
    {
        foreach (Tile t in tiles)
        {
            t.Tick();
        }
    }
}
