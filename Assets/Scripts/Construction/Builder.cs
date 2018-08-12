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

    public static Entity BuildingToEntity(Building building)
    {
        switch (building)
        {
            case Building.Townhall:
                return new Townhall();
            case Building.House:
                return new House();
            case Building.Lumberyard:
                return new Lumberyard();
            case Building.Windmill:
                return new Windmill();
            case Building.Tower:
                return new Tower();
            case Building.Castle:
                return new Castle();
            default:
                return null;
        }
    }
}
