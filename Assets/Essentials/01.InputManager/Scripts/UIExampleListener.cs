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
        private bool pPressed = false;
        private void OnEnable()
        {
            InputManager._MovementEvent += MoveButton;
            InputManager._PauseEvent += P_Button;
            InputManager._SprintEvent += G_Button;
        }
        private void OnDisable()
        {
            InputManager._MovementEvent -= MoveButton;
            InputManager._PauseEvent -= P_Button;
            InputManager._SprintEvent -= G_Button;
        }

        private void MoveButton(Vector2 dir)
        {
            D_Image.color = dir.x > deadZone ? pressedColor : Color.white;
            Right_Image.color = dir.x > deadZone ? pressedColor : Color.white;

            A_Image.color = dir.x < -deadZone ? pressedColor : Color.white;
            Left_Image.color = dir.x < -deadZone ? pressedColor : Color.white;

            W_Image.color = dir.y > deadZone ? pressedColor : Color.white;
            Up_Image.color = dir.y > deadZone ? pressedColor : Color.white;

            S_Image.color = dir.y < -deadZone ? pressedColor : Color.white;
            Down_Image.color = dir.y < -deadZone ? pressedColor : Color.white;

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