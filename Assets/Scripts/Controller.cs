using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    public Dictionary<CommanderID, Commander> commanders = new Dictionary<CommanderID, Commander>();

    public Grid grid;
    public RectTransform unitsRoot;

    void Awake()
    {
        Instance = this;

        Assets.Configure();

        commanders.Add(CommanderID.Player, new Commander(5, 5, Config.colors.blue, CommanderID.Player));
        commanders.Add(CommanderID.Opponent, new Commander(5, 5, Config.colors.red, CommanderID.Opponent));

        grid.Build(6, 6, 36);

        commanders[CommanderID.Player].Build(new Windmill(), 4, 4);
        commanders[CommanderID.Opponent].Build(new Windmill(), 1, 1);
        commanders[CommanderID.Opponent].Build(new Windmill(), 2, 1);

        for (int i = 0; i < 2; i++)
        {
            Hatchery.SpawnPeasant(4, 4, CommanderID.Player);
        }

        for (int i = 0; i < 2; i++)
        {
            Hatchery.SpawnPeasant(1, 1, CommanderID.Opponent);
        }
    }

    void FixedUpdate()
    {
        grid.TickAll();
    }
}
