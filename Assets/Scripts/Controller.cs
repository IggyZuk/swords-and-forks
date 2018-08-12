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

        commanders[CommanderID.Player].TryBuild(new Townhall(), 1, 4);

        //commanders[CommanderID.Player].Build(new Windmill(), 4, 4);
        //commanders[CommanderID.Player].Build(new Lumberyard(), 2, 4);
        //commanders[CommanderID.Player].Build(new House(), 1, 5);

        commanders[CommanderID.Opponent].TryBuild(new Townhall(), 4, 1);

        AI ai = new AI(CommanderID.Opponent);

        //commanders[CommanderID.Opponent].TryBuild(new Windmill(), 1, 1);
        //commanders[CommanderID.Opponent].TryBuild(new Lumberyard(), 3, 1);
        //commanders[CommanderID.Opponent].TryBuild(new Tower(), 0, 0);
    }
}
