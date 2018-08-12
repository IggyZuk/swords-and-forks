using UnityEngine;

public static class Hatchery
{
    public static Peasant SpawnPeasant(int x, int y, CommanderID comID)
    {
        Peasant peasant = Object.Instantiate(Assets.Peasant);
        peasant.Init(x, y, comID);
        peasant.transform.SetParent(Controller.Instance.unitsRoot, false);
        peasant.transform.position = Controller.Instance.grid.GetTile(x, y).transform.position;
        SpawnEffect(x, y, Config.colors.white);
        return peasant;
    }

    public static Effect SpawnEffect(int x, int y, Color color)
    {
        Effect effect = Object.Instantiate(Assets.Effect);
        effect.transform.SetParent(Controller.Instance.UI.Root, false);
        effect.transform.position = Controller.Instance.grid.GetTile(x, y).transform.position;
        effect.Init(color);
        return effect;
    }
}
