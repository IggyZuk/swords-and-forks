public class Builder
{
    public static Tile Build(Entity entity, int x, int y)
    {
        Tile tile = Controller.Instance.grid.GetTile(x, y);

        if (tile == null) return null;
        if (tile.Entity != null) return null;

        tile.Entity = entity;

        return tile;
    }
}
