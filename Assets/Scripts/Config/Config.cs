using UnityEngine;

public static class Config
{
    public class Colors
    {
        public Color black = new Color32(23, 25, 27, 255);
        public Color white = new Color32(240, 240, 220, 255);
        public Color neutral = new Color32(42, 55, 71, 255);
        public Color red = new Color32(210, 64, 64, 255);
        public Color blue = new Color32(0, 160, 200, 255);
        public Color yellow = new Color32(255, 196, 56, 255);
        public Color green = new Color32(16, 200, 64, 255);
        public Color purple = new Color32(40, 35, 123, 255);
        public Color pink = new Color32(201, 92, 209, 255);
    }

    public class Prices
    {
        public int peasant = 5;
        public int townhall = 5;
        public int house = 4;
        public int lumberyard = 6;
        public int windmill = 10;
        public int tower = 20;
        public int castle = 50;
    }

    public static Colors colors = new Colors();
    public static Prices prices = new Prices();

    public const int HungerMax = 5;
    public const int EnergyMax = 15;

    

}
