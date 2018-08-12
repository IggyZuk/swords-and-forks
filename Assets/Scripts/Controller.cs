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

        commanders.Add(CommanderID.Player, new Commander(5, 5, Config.colors.blue));
        commanders.Add(CommanderID.Opponent, new Commander(5, 5, Config.colors.red));

        grid.Build(6, 6, 36);

        commanders[CommanderID.Player].Build(new Windmill(), 1, 1);
        commanders[CommanderID.Opponent].Build(new Windmill(), 4, 4);

        Peasant p = Hatchery.SpawnPeasant(4, 4, CommanderID.Player);
    }

    void FixedUpdate()
    {
        grid.TickAll();
    }
}
