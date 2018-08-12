using System.Linq;
using UnityEngine;

public static class Assets
{
    public static Tile Tile;
    public static Peasant Peasant;
    public static ResourceBit ResourceBit;

    public static Sprite Wheat0;
    public static Sprite Wheat1;
    public static Sprite Wheat2;
    public static Sprite Wheat3;
    public static Sprite Wheat4;

    public static Sprite Lumber0;
    public static Sprite Lumber1;
    public static Sprite Lumber2;
    public static Sprite Lumber3;
    public static Sprite Lumber4;

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
        ResourceBit = Resources.Load<ResourceBit>(path + "ResourceBit");
    }

    static void LoadSprites()
    {
        string path = "Sprites/";

        Sprite[] atlas = Resources.LoadAll<Sprite>(path + "tileset");

        Wheat0 = atlas.Single(s => s.name == "wheat0");
        Wheat1 = atlas.Single(s => s.name == "wheat1");
        Wheat2 = atlas.Single(s => s.name == "wheat2");
        Wheat3 = atlas.Single(s => s.name == "wheat3");
        Wheat4 = atlas.Single(s => s.name == "wheat4");

        Lumber0 = atlas.Single(s => s.name == "lumber0");
        Lumber1 = atlas.Single(s => s.name == "lumber1");
        Lumber2 = atlas.Single(s => s.name == "lumber2");
        Lumber3 = atlas.Single(s => s.name == "lumber3");
        Lumber4 = atlas.Single(s => s.name == "lumber4");

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
