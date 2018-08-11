using System.Linq;
using UnityEngine;

public static class Assets
{
    public static Tile Tile;

    public static Sprite Wheat_0;
    public static Sprite Wheat_1;
    public static Sprite Wheat_2;

    public static Sprite Tree_0;
    public static Sprite Tree_1;
    public static Sprite Tree_2;

    public static Sprite Townhall;
    public static Sprite House;
    public static Sprite Windmill;
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
    }

    static void LoadSprites()
    {
        string path = "Sprites/";

        Sprite[] atlas = Resources.LoadAll<Sprite>(path + "ld32-tileset");

        Wheat_0 = atlas.Single(s => s.name == "ld32-tileset_9");
        Wheat_1 = atlas.Single(s => s.name == "ld32-tileset_10");
        Wheat_2 = atlas.Single(s => s.name == "ld32-tileset_11");

        Tree_0 = atlas.Single(s => s.name == "ld32-tileset_12");
        Tree_1 = atlas.Single(s => s.name == "ld32-tileset_13");
        Tree_2 = atlas.Single(s => s.name == "ld32-tileset_14");

        Townhall = atlas.Single(s => s.name == "ld32-tileset_3");
        House = atlas.Single(s => s.name == "ld32-tileset_4");
        Windmill = atlas.Single(s => s.name == "ld32-tileset_5");
        Lumberyard = atlas.Single(s => s.name == "ld32-tileset_6");
        Castle = atlas.Single(s => s.name == "ld32-tileset_7");
        Tower = atlas.Single(s => s.name == "ld32-tileset_8");
    }
}
