using UnityEngine;

namespace Momentum
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] Juggler juggler;

        void Awake()
        {
            juggler = Core.Juggler;
        }

        void Update()
        {
            float speed = 1f;

            if (Input.GetKey(KeyCode.Alpha1)) speed = 4f;
            else if (Input.GetKey(KeyCode.Alpha2)) speed = 8f;
            else if (Input.GetKey(KeyCode.Alpha3)) speed = 16f;
            else if (Input.GetKey(KeyCode.Alpha4)) speed = 32f;

            Core.Juggler.Update(Time.deltaTime * speed);
        }
    }
}