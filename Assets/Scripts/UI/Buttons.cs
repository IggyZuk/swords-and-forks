using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class Buttons : MonoBehaviour
{
    [SerializeField] Button hirePeasant;
    [SerializeField] Button hireKnight;
    [SerializeField] Button constructHouse;
    [SerializeField] Button constructLumberyard;
    [SerializeField] Button constructWindmill;
    [SerializeField] Button constructTower;
    [SerializeField] Button constructCastle;
    [SerializeField] Button emptyBlock0;
    [SerializeField] Button emptyBlock1;

    [SerializeField] Text descriptionLabel;

    void Awake()
    {
        hirePeasant.onClick.AddListener(() =>
        {
            Commander com = Controller.Instance.commanders[CommanderID.Player];

            if (Config.prices.peasant <= com.wheat)
            {
                Tile t = Controller.Instance.grid.FindClosestTileWithEntity<Townhall>(new Pos(), CommanderID.Player);
                Hatchery.SpawnPeasant(t.pos.x, t.pos.y, CommanderID.Player);

                Task.Add().Time(0.03f).Random(0.015f).Loop(Config.prices.peasant).OnRepeat(_ =>
                {
                    Controller.Instance.commanders[CommanderID.Player].RemoveWheat();

                    Controller.Instance.UI.AddResourceBit(
                        Resource.Wheat,
                        Controller.Instance.UI.Wheat.position,
                        t.transform.position,
                        () => { }
                    );
                });
            }
        });

        constructHouse.onClick.AddListener(() => StartBuilding(Building.House));
        constructLumberyard.onClick.AddListener(() => StartBuilding(Building.Lumberyard));
        constructWindmill.onClick.AddListener(() => StartBuilding(Building.Windmill));
        constructTower.onClick.AddListener(() => StartBuilding(Building.Tower));
        constructCastle.onClick.AddListener(() => StartBuilding(Building.Castle));
    }

    void StartBuilding(Building building)
    {
        Controller.Instance.commanders[CommanderID.Player].building = building;
        DisableAll();

        SetDescription(
            building.ToString() +
            "\n-----\n" +
            "Lumber: " +
            Builder.BuildingToEntity(building).GetPrice().ToString()
        );
    }

    public void SetDescription(string desc)
    {
        descriptionLabel.text = desc;
    }

    public void DisableAll()
    {
        All(false);
    }

    public void EnableAll()
    {
        All(true);
    }

    void All(bool interactable)
    {
        hirePeasant.interactable = interactable;
        hireKnight.interactable = interactable;
        constructHouse.interactable = interactable;
        constructLumberyard.interactable = interactable;
        constructWindmill.interactable = interactable;
        constructTower.interactable = interactable;
        constructCastle.interactable = interactable;
        emptyBlock0.interactable = interactable;
        emptyBlock1.interactable = interactable;
    }
}
