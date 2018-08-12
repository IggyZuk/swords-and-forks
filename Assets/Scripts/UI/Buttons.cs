using UnityEngine;
using UnityEngine.UI;

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
            Tile t = Controller.Instance.grid.FindClosestTileWithEntity(new Pos(), typeof(Townhall), CommanderID.Player);
            Hatchery.SpawnPeasant(t.pos.x, t.pos.y, CommanderID.Player);
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

        descriptionLabel.text = "Lumber: " + Builder.BuildingToEntity(building).GetPrice().ToString();
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
