using System.Linq;
using UnityEngine;

public static class Assets
{
    public static Tile tile;

    public static Sprite tree;
    public static Sprite wheat;

    public static Sprite townhall;
    public static Sprite house;
    public static Sprite windmill;
    public static Sprite lumberyard;
    public static Sprite tower;
    public static Sprite castle;

    public static void Configure()
    {
        LoadPrefabs();
        LoadSprites();
    }
    static void LoadPrefabs()
    {
        string path = "Prefabs/";

        tile = Resources.Load<Tile>(path + "Tile");
    }

    static void LoadSprites()
    {
        string path = "Sprites/";

        Sprite[] atlas = Resources.LoadAll<Sprite>(path + "ld32-tileset");

        tree = atlas.Single(s => s.name == "ld32-tileset_11");
        wheat = atlas.Single(s => s.name == "ld32-tileset_14");

        townhall = atlas.Single(s => s.name == "ld32-tileset_3");
        house = atlas.Single(s => s.name == "ld32-tileset_4");
        windmill = atlas.Single(s => s.name == "ld32-tileset_5");
        lumberyard = atlas.Single(s => s.name == "ld32-tileset_6");
        tower = atlas.Single(s => s.name == "ld32-tileset_7");
        castle = atlas.Single(s => s.name == "ld32-tileset_8");
    }
}
