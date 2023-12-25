using UnityEngine;
using UnityEngine.UI;
namespace Essentials
{
    public class UIExampleListener : MonoBehaviour
    {
        [SerializeField] private Color pressedColor = Color.cyan;
        [SerializeField] private Image W_Image;
        [SerializeField] private Image Up_Image;

        [SerializeField] private Image A_Image;
        [SerializeField] private Image Left_Image;

        [SerializeField] private Image S_Image;
        [SerializeField] private Image Down_Image;

        [SerializeField] private Image D_Image;
        [SerializeField] private Image Right_Image;

        [SerializeField] private Image P_Image;
        [SerializeField] private Image G_Image;
        private float deadZone = 0.5f;
        private bool pPressed = true;
        private void OnEnable()
        {
            InputManager.Instance._MovementEvent += MoveButton;
            InputManager.Instance._PauseEvent += P_Button;
            InputManager.Instance._SprintEvent += G_Button;
        }
        private void OnDisable()
        {
            InputManager.Instance._MovementEvent -= MoveButton;
            InputManager.Instance._PauseEvent -= P_Button;
            InputManager.Instance._SprintEvent -= G_Button;
        }

        private void MoveButton(Vector2 dir)
        {
            bool right = dir.x > deadZone;
            D_Image.color = right ? pressedColor : Color.white;
            Right_Image.color = right ? pressedColor : Color.white;

            bool left = dir.x < -deadZone;
            A_Image.color = left ? pressedColor : Color.white;
            Left_Image.color = left ? pressedColor : Color.white;

            bool up = dir.y > deadZone;
            W_Image.color = up ? pressedColor : Color.white;
            Up_Image.color = up ? pressedColor : Color.white;

            bool down = dir.y < -deadZone;
            S_Image.color = down ? pressedColor : Color.white;
            Down_Image.color = down ? pressedColor : Color.white;

        }

        private void P_Button()
        {
            P_Image.color = pPressed ? pressedColor : Color.white;
            pPressed = !pPressed;
        }
        private void G_Button(bool status)
        {
            G_Image.color = status ? pressedColor : Color.white;

        }

    }
}