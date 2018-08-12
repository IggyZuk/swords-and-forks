public class Builder
{
    public static Tile Build(Entity entity, int x, int y, CommanderID comID)
    {
        Tile tile = Controller.Instance.grid.GetTile(x, y);

        if (tile == null) return null;
        if (tile.Entity != null) return null;

        tile.Entity = entity;
        tile.Entity.comID = comID;

        return tile;
    }
}
