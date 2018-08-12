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

        Task.Add().Time(5f).Random(4f).Loop(-1).OnRepeat(_ =>
        {
            Debug.Log("Thinking...");

            if (commands.Count > 0)
            {
                if (com.TryBuild(Builder.BuildingToEntity(commands[0]), Random.Range(0, 5), Random.Range(0, 5)))
                {
                    commands.RemoveAt(0);
                }
                return;
            }
        });
    }
}
