﻿using UnityEngine;
using System.Collections.Generic;
using Momentum;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    public Dictionary<CommanderID, Commander> commanders = new Dictionary<CommanderID, Commander>();

    public UI UI;
    public Grid grid;
    public RectTransform unitsRoot;

    TaskDisposables disposables = new TaskDisposables();

    void Awake()
    {
        Instance = this;

        Assets.Configure();

        commanders.Add(CommanderID.Player, new Commander(30, 30, Config.colors.blue, CommanderID.Player));
        commanders.Add(CommanderID.Opponent, new Commander(30, 30, Config.colors.red, CommanderID.Opponent));

        UI.UpdateResources();

        grid.Build(6, 6, 36);

        // TODO: pick random corners and spawn town hall
        commanders[CommanderID.Player].TryBuild(new Townhall(), 0, 5);
        commanders[CommanderID.Opponent].TryBuild(new Townhall(), 5, 0);

        AI ai = new AI(CommanderID.Opponent);

        Task.Add()
            .Name("Overflow")
            .Time(2f)
            .Loop(-1)
            .Dispose(disposables)
            .OnRepeat(data =>
            {
                Pos[] ps = grid.GetBorderPositions(CommanderID.Opponent);

                Task.Add().Time(0.01f).Random(0.01f).Loop(ps.Length).OnRepeat(tt =>
                {
                    grid.GetTile(ps[tt.CurrentLoop - 1]).Glow(Config.colors.red);
                });
            });
    }

    void OnDestroy()
    {
        disposables.Dispose();
    }
}
