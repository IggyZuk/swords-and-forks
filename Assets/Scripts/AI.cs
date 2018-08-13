using UnityEngine;
using Momentum;
using System.Collections.Generic;

public class AI
{
    CommanderID comID;

    List<Building> commands = new List<Building>();

    public AI(CommanderID comID)
    {
        this.comID = comID;

        Commander com = Controller.Instance.commanders[CommanderID.Opponent];

        commands.Add(Building.Windmill);
        commands.Add(Building.Lumberyard);
        commands.Add(Building.House);
        commands.Add(Building.Windmill);
        commands.Add(Building.Windmill);
        commands.Add(Building.House);
        commands.Add(Building.House);
        commands.Add(Building.Lumberyard);
        commands.Add(Building.Tower);
        commands.Add(Building.Tower);
        commands.Add(Building.House);
        commands.Add(Building.House);
        commands.Add(Building.Tower);
        commands.Add(Building.Castle);
        commands.Add(Building.House);
        commands.Add(Building.Castle);
        commands.Add(Building.House);
        commands.Add(Building.Windmill);
        commands.Add(Building.Windmill);
        commands.Add(Building.House);
        commands.Add(Building.House);
        commands.Add(Building.Lumberyard);
        commands.Add(Building.Tower);
        commands.Add(Building.Tower);
        commands.Add(Building.House);
        commands.Add(Building.House);

        Task.Add().Time(5f).Random(4f).Loop(-1).OnRepeat(_ =>
        {
            Debug.Log("Thinking...");

            if (commands.Count > 0)
            {
                Pos[] borderPositions = Controller.Instance.grid.GetBorderPositions(CommanderID.Opponent);
                Pos randomPos = borderPositions[Random.Range(0, borderPositions.Length)];

                if (com.TryBuild(Builder.BuildingToEntity(commands[0]), randomPos.x, randomPos.y))
                {
                    commands.RemoveAt(0);
                }
                return;
            }
        });
    }
}
