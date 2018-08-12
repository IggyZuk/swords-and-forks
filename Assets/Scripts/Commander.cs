﻿using UnityEngine;

public class Commander
{
    // TODO: Move hatchery & builder here

    public int lumber = 0;
    public int wheat = 0;

    public Color color = Config.colors.neutral;

    public CommanderID comID;

    public Commander(int lumber, int wheat, Color color, CommanderID comID)
    {
        this.lumber = lumber;
        this.wheat = wheat;
        this.color = color;
        this.comID = comID;
    }

    public void Build(Entity entity, int x, int y)
    {
        // TODO: check resources
        // TODO: check if entity exists

        Tile tile = Builder.Build(entity, x, y, comID);
        if (tile != null)
        {
            tile.Border = color;
            tile.SetEntityColor(color);
        }
    }

    public void AddWheat(int count)
    {
        wheat += count;
    }
}
