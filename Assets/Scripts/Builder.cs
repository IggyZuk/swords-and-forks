public class Builder
{
    public static bool Build(Entity entity, int x, int y)
    {
        Tile tile = Controller.Instance.grid.GetTile(x, y);

        if (tile == null) return false;
        if (tile.Entity != null) return false;

        tile.Entity = entity;
        return true;
    }
}
