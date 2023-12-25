using UnityEngine;
namespace Essentials
{
    public class ExampleInputListener : MonoBehaviour
    {
        public Vector2 movementVector;
        public bool jumping;
        public bool sprinting;
        public bool interacting;
        public bool button1Pressed;
        public bool button2Pressed;
        public bool button3Pressed;
        public bool pausePressed;

        private void Start()
        {
            InputManager.Instance._MovementEvent += OnMove;
            InputManager.Instance._JumpEvent += OnJump;
            InputManager.Instance._InteractEvent += OnInteract;
            InputManager.Instance._Button1Event += OnButton1Press;
            InputManager.Instance._Button2Event += OnButton2Press;
            InputManager.Instance._Button3Event += OnButton3Press;
            InputManager.Instance._PauseEvent += OnPausePressed;
            InputManager.Instance._SprintEvent += OnSprint;
        }
        private void OnDisable()
        {
            InputManager.Instance._MovementEvent -= OnMove;
            InputManager.Instance._JumpEvent -= OnJump;
            InputManager.Instance._InteractEvent -= OnInteract;
            InputManager.Instance._Button1Event -= OnButton1Press;
            InputManager.Instance._Button2Event -= OnButton2Press;
            InputManager.Instance._Button3Event -= OnButton3Press;
            InputManager.Instance._PauseEvent -= OnPausePressed;
            InputManager.Instance._SprintEvent -= OnSprint;
        }


        //RECIEVE INPUTS FORM INPUTREADER -> 
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