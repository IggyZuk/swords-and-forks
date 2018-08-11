using UnityEngine;

public class Commander
{
    public int wood = 0;
    public int food = 0;
    public Color color = Config.colors.neutral;

    public Commander(int initialWood, int initialFood, Color teamColor)
    {
        wood = initialWood;
        food = initialFood;
        color = teamColor;
    }

    public void Build(Entity entity, int x, int y)
    {
        // TODO: check resources
        // TODO: check if entity exists

        Tile t = Controller.Instance.grid.GetTile(x, y);
        t.Entity = entity;
        t.Color = color;
    }
}
