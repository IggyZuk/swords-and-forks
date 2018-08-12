using System.Linq;
using UnityEngine;

public static class Assets
{
    public static Tile Tile;
    public static Peasant Peasant;

    public static Sprite Wheat0;
    public static Sprite Wheat1;
    public static Sprite Wheat2;
    public static Sprite Wheat3;
    public static Sprite Wheat4;

    public static Sprite Tree0;
    public static Sprite Tree1;
    public static Sprite Tree2;
    public static Sprite Tree3;
    public static Sprite Tree4;

    public static Sprite Windmill0;
    public static Sprite Windmill1;
    public static Sprite Windmill2;

    public static Sprite Townhall;
    public static Sprite House;
    public static Sprite Lumberyard;
    public static Sprite Castle;
    public static Sprite Tower;

    public static void Configure()
    {
        LoadPrefabs();
        LoadSprites();
    }
    static void LoadPrefabs()
    {
        string path = "Prefabs/";

        Tile = Resources.Load<Tile>(path + "Tile");
        Peasant = Resources.Load<Peasant>(path + "Peasant");
    }

    static void LoadSprites()
    {
        string path = "Sprites/";

        Sprite[] atlas = Resources.LoadAll<Sprite>(path + "ld32-tileset");

        Wheat0 = atlas.Single(s => s.name == "wheat0");
        Wheat1 = atlas.Single(s => s.name == "wheat1");
        Wheat2 = atlas.Single(s => s.name == "wheat2");
        Wheat3 = atlas.Single(s => s.name == "wheat3");
        Wheat4 = atlas.Single(s => s.name == "wheat4");

        Tree0 = atlas.Single(s => s.name == "tree0");
        Tree1 = atlas.Single(s => s.name == "tree1");
        Tree2 = atlas.Single(s => s.name == "tree2");
        Tree3 = atlas.Single(s => s.name == "tree3");
        Tree4 = atlas.Single(s => s.name == "tree4");

        Windmill0 = atlas.Single(s => s.name == "windmill0");
        Windmill1 = atlas.Single(s => s.name == "windmill1");
        Windmill2 = atlas.Single(s => s.name == "windmill2");

        Townhall = atlas.Single(s => s.name == "townhall");
        House = atlas.Single(s => s.name == "house");
        Lumberyard = atlas.Single(s => s.name == "lumberyard");
        Castle = atlas.Single(s => s.name == "castle");
        Tower = atlas.Single(s => s.name == "tower");
    }
}
