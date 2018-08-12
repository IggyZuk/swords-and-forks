using UnityEngine;

public class Commander
{
    // TODO: Move hatchery & builder here

    public int wood = 0;
    public int food = 0;

    public Color color = Config.colors.neutral;

    public CommanderID comdID;

    public Commander(int wood, int food, Color color, CommanderID comID)
    {
        this.wood = wood;
        this.food = food;
        this.color = color;
        this.comdID = comID;
    }

    public void Build(Entity entity, int x, int y)
    {
        // TODO: check resources
        // TODO: check if entity exists

        Tile tile = Builder.Build(entity, x, y, comdID);
        if (tile != null)
        {
            tile.Border = color;
            tile.SetEntityColor(color);
        }
    }
}
