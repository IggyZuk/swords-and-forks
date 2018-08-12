using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] Image entityImage;
    [SerializeField] Image borderImage;
    [SerializeField] TextMeshProUGUI levelLabel;

    public Pos pos;

    Entity entity;
    int level;
    Color border;

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

    public void Tick()
    {
        if (entity != null) entity.Tick();
    }
}
