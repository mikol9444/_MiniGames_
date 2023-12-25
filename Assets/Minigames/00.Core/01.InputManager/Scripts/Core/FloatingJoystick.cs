using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace Essentials
{

    /// <summary>
    /// A stick control displayed on screen and moved around by touch or other pointer
    /// input. Floats to pointer down position.
    /// </summary>
    [AddComponentMenu("Input/Floating On-Screen Stick")]
    public class FloatingJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Initialization
        [SerializeField] private float range = 50;

        [InputControl(layout = "Vector2")]
        [SerializeField] private string Control;
        public float MovementRange { get => range; set => range = value; }

        protected override string controlPathInternal { get => Control; set => Control = value; }

        [SerializeField] private RectTransform stickArea;
        [SerializeField] private RectTransform Knob;
        [SerializeField] public Image[] quadrantImages = new Image[4];
        [SerializeField] private Color moveColor;



        private Vector2 startPos;
        private Vector2 pointerDownPos;
        private Vector2 dragPos;


        private void Start()
        {
            startPos = ((RectTransform)transform).anchoredPosition;
            EnableJoystick(false);
        }

        #endregion

        #region Functionality
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));
            EnableJoystick(true);
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, eventData.position, eventData.pressEventCamera, out pointerDownPos);
            Knob.anchoredPosition = pointerDownPos;
            stickArea.anchoredPosition = pointerDownPos;
            QuadrantMarker(Vector2.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, eventData.position, eventData.pressEventCamera, out dragPos);
            var delta = dragPos - pointerDownPos;

            delta = Vector2.ClampMagnitude(delta, MovementRange);
            Knob.anchoredPosition = pointerDownPos + delta;

            var newPos = new Vector2(delta.x / MovementRange, delta.y / MovementRange);
            //Debug.Log(newPos.magnitude);
            QuadrantMarker(newPos);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Knob.anchoredPosition = startPos;
            SendValueToControl(Vector2.zero);
            EnableJoystick(false);
            QuadrantMarker(Vector2.zero);
        }
        private void EnableJoystick(bool status)
        {
            Knob.gameObject.SetActive(status);
            stickArea.gameObject.SetActive(status);
        }
        #endregion

        //Lerp transparancy Color on 4 Images of a joystick from 0 to 1, depending on Vector2 direction as Parameter
        public void QuadrantMarker(Vector2 direction)
        {
            Color color = moveColor;
            color.a = 0;
            for (int i = 0; i < quadrantImages.Length; i++)
            {
                quadrantImages[i].color = color;
            }
            color.a = direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            if (angle >= 0 && angle <= 90)
            {
                quadrantImages[0].enabled = true;
                quadrantImages[0].color = color;
            }
            else if (angle > 90f && angle <= 180f)
            {
                quadrantImages[1].enabled = true;
                quadrantImages[1].color = color;
            }
            else if (angle < -90f && angle > -180f)
            {
                quadrantImages[2].enabled = true;
                quadrantImages[2].color = color;
            }
            else if (angle < 0f && angle >= -90f)
            {
                quadrantImages[3].enabled = true;
                quadrantImages[3].color = color;
            }
        }

    }
}