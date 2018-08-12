using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Momentum;

public class Tile : MonoBehaviour
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

        glowTask = Task.Add()
            .Time(5f)
            .Random(0.5f)
            .OnUpdate(tileGlowTask =>
            {
                borderImage.color = Color.Lerp(
                    color,
                    Config.colors.neutral,
                    Ease.InOutSine(tileGlowTask.progress)
                );
            })
            .OnComplete(_ =>
            {
                borderImage.color = border;
            });
    }
}
