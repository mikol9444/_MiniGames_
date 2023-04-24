using UnityEngine;

namespace Essentials
{
    [RequireComponent(typeof(InputReader))]
    public class InputManager : MonoBehaviour
    {
        public Vector2 movementVector;
        public bool jumping;
        public bool sprinting;
        public bool interacting;
        public bool button1Pressed;
        public bool button2Pressed;
        public bool button3Pressed;
        public bool pausePressed;

        private void OnEnable()
        {
            InputReader._MovementEvent += OnMove;
            InputReader._JumpEvent += OnJump;
            InputReader._InteractEvent += OnInteract;
            InputReader._Button1Event += OnButton1Press;
            InputReader._Button2Event += OnButton2Press;
            InputReader._Button3Event += OnButton3Press;
            InputReader._PauseEvent += OnPausePressed;
            InputReader._SprintEvent += OnSprint;
        }
        private void OnDisable()
        {
            InputReader._MovementEvent -= OnMove;
            InputReader._JumpEvent -= OnJump;
            InputReader._InteractEvent -= OnInteract;
            InputReader._Button1Event -= OnButton1Press;
            InputReader._Button2Event -= OnButton2Press;
            InputReader._Button3Event -= OnButton3Press;
            InputReader._PauseEvent -= OnPausePressed;
            InputReader._SprintEvent -= OnSprint;
        }
        private void OnMove(Vector2 dir) => movementVector = dir;
        private void OnJump(bool state) => jumping = state;
        private void OnSprint(bool state) => sprinting = state;
        private void OnInteract() => interacting = !interacting;
        private void OnButton1Press() => button1Pressed = !button1Pressed;
        private void OnButton2Press() => button2Pressed = !button2Pressed;
        private void OnButton3Press() => button3Pressed = !button3Pressed;
        private void OnPausePressed() => pausePressed = !pausePressed;
    }
}