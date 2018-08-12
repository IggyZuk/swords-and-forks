using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    public Dictionary<CommanderID, Commander> commanders = new Dictionary<CommanderID, Commander>();

    public UI UI;
    public Grid grid;
    public RectTransform unitsRoot;

    void Awake()
    {
        Instance = this;

        Assets.Configure();

        commanders.Add(CommanderID.Player, new Commander(10, 25, Config.colors.blue, CommanderID.Player));
        commanders.Add(CommanderID.Opponent, new Commander(10, 25, Config.colors.red, CommanderID.Opponent));

        UI.UpdateResources();

        grid.Build(6, 6, 36);

        // TODO: pick random corners and spawn town hall

        commanders[CommanderID.Player].Build(new Windmill(), 4, 4);
        commanders[CommanderID.Player].Build(new Lumberyard(), 2, 4);
        commanders[CommanderID.Player].Build(new Townhall(), 0, 4);
        commanders[CommanderID.Player].Build(new House(), 1, 5);

        commanders[CommanderID.Opponent].Build(new Windmill(), 1, 1);
        commanders[CommanderID.Opponent].Build(new Lumberyard(), 3, 1);
        commanders[CommanderID.Opponent].Build(new Tower(), 0, 0);
        commanders[CommanderID.Opponent].Build(new Townhall(), 5, 0);

        //for (int i = 0; i < 2; i++)
        //{
        //    Hatchery.SpawnPeasant(4, 4, CommanderID.Player);
        //}

        //for (int i = 0; i < 2; i++)
        //{
        //    Hatchery.SpawnPeasant(1, 1, CommanderID.Opponent);
        //}
    }
}
