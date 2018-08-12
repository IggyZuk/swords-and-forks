using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class Effect : MonoBehaviour
{
    [SerializeField] Image image;

    public void Init(Color color)
    {
        Sprite[] frames = new Sprite[] {
            Assets.Effect0,
            Assets.Effect1,
            Assets.Effect2,
            Assets.Effect3,
            Assets.Effect4,
            Assets.Effect5,
            Assets.Effect6,
            Assets.Effect6,
        };

        image.sprite = frames[0];
        image.color = color;

        Task.Add()
            .Time(0.75f)
            .OnUpdate(t =>
            {
                int frameIdx = (int)(t.progress * frames.Length - 1);
                frameIdx = Mathf.Clamp(frameIdx, 0, frames.Length - 1);

                image.sprite = frames[frameIdx];
            })
            .OnComplete(_ =>
            {
                Destroy(this.gameObject);
            });
    }
}
