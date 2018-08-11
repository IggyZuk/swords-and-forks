using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    public Grid grid;

    public Commander player;
    public Commander opponent;

    void Awake()
    {
        Instance = this;

        Assets.Configure();

        player = new Commander(5, 5, Config.colors.blue);
        opponent = new Commander(5, 5, Config.colors.red);

        grid.Build(6, 6, 36);

        player.Build(new Windmill(), 1, 1);
        opponent.Build(new Windmill(), 4, 4);
    }

    void FixedUpdate()
    {
        grid.TickAll();
    }
}
