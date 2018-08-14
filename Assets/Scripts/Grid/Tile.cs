using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Momentum;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image entityImage;
    [SerializeField] Image borderImage;
    [SerializeField] TextMeshProUGUI levelLabel;

    public Pos pos;

    Entity entity;
    int level;
    Color border;

    Task glowTask;

    public Entity Entity
    {
        get { return entity; }
        set
        {
            if (entity != null) entity.Deinit();

            entity = value;

            entityImage.enabled = value != null;

            if (value != null)
            {
                entity.tile = this;
                entity.Init();
                SetEntitySprite(entity.GetSprite());
            }
        }
    }

    public Color Border
    {
        get { return border; }
        set
        {
            border = value;
            borderImage.color = value;
        }
    }

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            levelLabel.text = level > 0 ? level.ToString() : string.Empty;
        }
    }

    public void SetEntitySprite(Sprite sprite)
    {
        entityImage.sprite = sprite;
    }

    public void SetEntityColor(Color color)
    {
        entityImage.color = color;
    }

    public void Glow(Color color)
    {
        if (entity != null && entity.comID != CommanderID.None) return;

        if (glowTask != null) Core.Juggler.Remove(glowTask);

        glowTask = Task.Run()
            .Name("Tile Glow")
            .Time(5f)
            .Random(0.5f)
            .OnUpdate(tileGlowTask =>
            {
                borderImage.color = Color.Lerp(
                    color,
                    Config.colors.neutral,
                    Ease.InOutSine(tileGlowTask.Progress)
                );
            })
            .OnComplete(_ =>
            {
                borderImage.color = border;
            });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (entity == null) return;


        Controller.Instance.UI.Buttons.SetDescription(entity.ToString() + "\n-----\nUpgrade: " + entity.GetPrice() * entity.level);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Commander com = Controller.Instance.commanders[CommanderID.Player];
        if (com.building != Building.None)
        {
            com.TryBuild(Builder.BuildingToEntity(com.building), pos.x, pos.y);
            com.building = Building.None;

            Controller.Instance.UI.Buttons.EnableAll();
        }
        else if (entity != null && entity.comID == CommanderID.Player)
        {
            int price = entity.GetPrice() * entity.level;
            if (price <= com.lumber)
            {
                Hatchery.SpawnEffect(pos.x, pos.y, Config.colors.purple);

                entity.level++;
                Level = entity.level;

                Task.Run().Time(0.03f).Random(0.015f).Loop(price).OnRepeat(_ =>
                {
                    Controller.Instance.UI.AddResourceBit(
                        Resource.Lumber,
                        Controller.Instance.UI.Lumber.position,
                        this.transform.position,
                        () => Controller.Instance.commanders[CommanderID.Player].RemoveLumber()
                    );
                });
            }
        }
    }
}
