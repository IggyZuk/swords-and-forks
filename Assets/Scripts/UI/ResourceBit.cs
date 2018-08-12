using UnityEngine;
using UnityEngine.UI;

public class ResourceBit : MonoBehaviour
{
    [SerializeField] Image image;

    public void Init(Resource resource)
    {
        switch (resource)
        {
            case Resource.Lumber:
                image.sprite = Assets.Lumber4;
                image.color = Config.colors.green;
                break;
            case Resource.Wheat:
                image.sprite = Assets.Wheat4;
                image.color = Config.colors.yellow;
                break;
        }
        image.color = Config.colors.white;
    }
}
