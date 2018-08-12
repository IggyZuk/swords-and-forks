using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class UI : MonoBehaviour
{
    [SerializeField] RectTransform root;

    [SerializeField] Text lumberLabel;
    [SerializeField] Text wheatLabel;

    RectTransform Root { get { return root; } }

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

    public void AddResourceBit(Resource resource, Pos pos)
    {
        ResourceBit bit = Object.Instantiate(Assets.ResourceBit);
        bit.transform.SetParent(root, false);
        bit.Init(resource);

        Vector3 from = Controller.Instance.grid.GetTile(pos).transform.position;
        Vector3 to = wheatLabel.transform.position;

        Task.Add()
            .Time(0.5f)
            .OnUpdate(t =>
            {
                bit.transform.position = Vector3.LerpUnclamped(from, to, Ease.OutBack(t.progress));
            })
            .OnComplete(_ =>
            {
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