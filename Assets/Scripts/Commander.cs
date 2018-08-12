using UnityEngine;

public class Commander
{
    // TODO: Move hatchery & builder here

    public int population = 0;
    public int maxPopulation = 0;

    public int lumber = 0;
    public int wheat = 0;

    public Color color = Config.colors.neutral;

    public CommanderID comID;

    public Building building = Building.None;

    public Commander(int lumber, int wheat, Color color, CommanderID comID)
    {
        this.lumber = lumber;
        this.wheat = wheat;
        this.color = color;
        this.comID = comID;
    }

    public bool TryBuild(Entity entity, int x, int y)
    {
        entity.comID = comID;

        if (entity.GetPrice() <= lumber)
        {
            Tile tile = Builder.Build(entity, x, y);
            if (tile != null)
            {
                lumber -= entity.GetPrice();

                tile.Border = color;
                tile.SetEntityColor(color);

                Hatchery.SpawnEffect(x, y, Config.colors.white);
            }

            Controller.Instance.UI.UpdateLumber();

            return true;
        }
        return false;
    }

    public void AddLumber(int count = 1)
    {
        lumber += count;
    }

    public void RemoveLumber(int count = 1)
    {
        lumber -= count;
        lumber = Mathf.Clamp(lumber, 0, int.MaxValue);
    }

    public void AddWheat(int count = 1)
    {
        wheat += count;
    }

    public void RemoveWheat(int count = 1)
    {
        wheat -= count;
        wheat = Mathf.Clamp(wheat, 0, int.MaxValue);
    }
}
