using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class ExampleAnimator : MonoBehaviour
{
    [SerializeField] private SmoothPosition smoothPosition;

    [SerializeField] private float repeatRate = 1f;
    [SerializeField] private MovementType movementType = MovementType.MoveInSpiral;
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private float spiralFactor = 0.1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float duration = 25f;
    [SerializeField] private Vector3 moveByVector = Vector3.right;
    private bool isMoving = false;
    [SerializeField] private bool moveOnStart = true;
    [SerializeField] private bool endlessMove = true;


    public enum MovementType { MoveTo, MoveBy, MoveInCircle, MoveInSpiral, MoveInEight }

    private void Start()
    {
        smoothPosition = SmoothPosition.Instance;
        if (endlessMove)
        {
            InvokeRepeating(nameof(startTransition), 0f, repeatRate);
        }
        else if (moveOnStart)
        {
            startTransition();
        }

    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("T key was pressed.");

            startTransition();
        }

    }
    public void startTransition()
    {
        if (!isMoving)
        {
            switch (movementType)
            {
                case MovementType.MoveTo:
                    smoothPosition.StartMoveTo(transform, moveByVector, duration);
                    break;
                case MovementType.MoveBy:
                    smoothPosition.StartMoveBy(transform, moveByVector, duration);
                    break;
                case MovementType.MoveInCircle:
                    smoothPosition.StartMoveInCircle(transform, moveByVector.normalized, circleRadius, moveSpeed, duration);
                    break;
                case MovementType.MoveInSpiral:
                    smoothPosition.StartMoveInSpiral(transform, moveByVector.normalized, circleRadius, spiralFactor, moveSpeed, duration);
                    break;
                case MovementType.MoveInEight:
                    smoothPosition.StartMoveInEight(transform, moveByVector.normalized, circleRadius, moveSpeed, duration);
                    break;
            }
            StartCoroutine(WaitDuration());

        }
    }
    private IEnumerator WaitDuration()
    {
        isMoving = true;
        yield return new WaitForSeconds(duration);
        isMoving = false;

    }



}

