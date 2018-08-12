using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class UI : MonoBehaviour
{
    [SerializeField] RectTransform root;
    [SerializeField] Text lumberLabel;
    [SerializeField] Text wheatLabel;
    [SerializeField] RectTransform lumberIcon;
    [SerializeField] RectTransform wheatIcon;

    public RectTransform Root { get { return root; } }
    public RectTransform Lumber { get { return lumberIcon; } }
    public RectTransform Wheat { get { return wheatIcon; } }

    public void UpdateResources()
    {
        UpdateLumber();
        UpdateWheat();
    }

    public void UpdateLumber()
    {
        lumberLabel.text = Controller.Instance.commanders[CommanderID.Player].lumber.ToString();

        Task.Add()
            .Time(1f)
            .Random(0.5f)
            .OnUpdate(t =>
            {
                lumberLabel.transform.localScale = Vector3.one * Mathf.LerpUnclamped(2f, 1f, Ease.OutBack(t.progress));
                lumberLabel.color = Color.Lerp(Config.colors.white, Config.colors.green, Ease.InOutSine(t.progress));
            })
            .OnComplete(_ =>
            {
                lumberLabel.transform.localScale = Vector3.one;
                lumberLabel.color = Config.colors.green;
            });
    }

    public void UpdateWheat()
    {
        wheatLabel.text = Controller.Instance.commanders[CommanderID.Player].wheat.ToString();

        Task.Add()
            .Time(1f)
            .Random(0.5f)
            .OnUpdate(t =>
            {
                wheatLabel.transform.localScale = Vector3.one * Mathf.LerpUnclamped(2f, 1f, Ease.OutBack(t.progress));
                wheatLabel.color = Color.Lerp(Config.colors.white, Config.colors.yellow, Ease.InOutSine(t.progress));
            })
            .OnComplete(_ =>
            {
                wheatLabel.transform.localScale = Vector3.one;
                wheatLabel.color = Config.colors.yellow;
            });
    }

    public void AddResourceBit(Resource resource, Vector2 from, Vector2 to, System.Action onComplete)
    {
        ResourceBit bit = Object.Instantiate(Assets.ResourceBit);
        bit.transform.SetParent(root, false);
        bit.transform.position = from;
        bit.Init(resource);

        Task.Add()
            .Time(0.5f)
            .OnUpdate(t =>
            {
                bit.transform.position = Vector3.LerpUnclamped(from, to, Ease.InOutSine(t.progress));
            })
            .OnComplete(_ =>
            {
                onComplete();

                Destroy(bit.gameObject);

                switch (resource)
                {
                    case Resource.Lumber:
                        UpdateLumber();
                        break;
                    case Resource.Wheat:
                        UpdateWheat();
                        break;
                }
            });
    }
}