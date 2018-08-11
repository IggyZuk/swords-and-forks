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
    Color color;
    int level;

    public Entity Entity
    {
        get { return entity; }
        set
        {
            if (entity != null) entity.Deinit();

            entity = value;

            if (value != null)
            {
                entityImage.enabled = true;
                entityImage.sprite = entity.GetSprite();
                entity.Init(this);
            }
            else
            {
                entityImage.enabled = false;
            }
        }
    }

    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            borderImage.color = value;
            entityImage.color = value;
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

    public void Tick()
    {
        if (entity != null) entity.Tick();
    }
}
