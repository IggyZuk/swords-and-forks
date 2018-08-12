using UnityEngine;

public static class Hatchery
{
    public static Peasant SpawnPeasant(int x, int y, CommanderID comID)
    {
        Peasant peasant = Object.Instantiate(Assets.Peasant);
        peasant.Init(x, y, comID);
        peasant.transform.SetParent(Controller.Instance.unitsRoot, false);
        peasant.transform.position = Controller.Instance.grid.GetTile(x, y).transform.position;
        return peasant;
    }
}
