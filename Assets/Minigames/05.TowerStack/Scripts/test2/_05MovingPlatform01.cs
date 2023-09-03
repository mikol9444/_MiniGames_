using UnityEngine;
using System.Collections;


public class _05MovingPlatform01 : MonoBehaviour
{

    private int personalID;
    [SerializeField] private float moveSpeed = 1f;
    public float Speed { get { return moveSpeed; } set { moveSpeed = value; } }
    [SerializeField] private bool lookLeft = true; // New bool for looking direction
    public float movementDirection = 1f; // Forward or backward movement direction
    private Vector3 initialPosition; // Initial position to move back to
    [SerializeField] private float moveDistance { get; set; }


    private Coroutine movementCoroutine; // Reference to the movement coroutine

    public void Initialize(bool shouldLookLeft, int personalID, float dist = 1f)
    {
        moveDistance = dist;
        gameObject.name = gameObject.name + "_" + personalID++;
        lookLeft = shouldLookLeft;


        GetComponent<Renderer>().material.color = _05GameManager01.GetRandomColor();


        // Apply rotation based on the lookLeft bool
        if (lookLeft)
            transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Look left
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Look back

        initialPosition = transform.position; // Store the initial position

        // Start the movement coroutine
        movementCoroutine = StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {

        while (true)
        {
            // Calculate the new position based on the current movement direction
            Vector3 newPosition = transform.position + moveDistance * movementDirection * moveSpeed * Time.deltaTime * transform.forward;

            // Check if the destination is reached, then change the movement direction
            if (Vector3.Distance(transform.position, initialPosition) >= moveDistance)
            {
                movementDirection *= -1f; // Toggle the movement direction
                initialPosition = transform.position;
                transform.position = new Vector3(Mathf.Ceil(initialPosition.x), initialPosition.y, Mathf.Ceil(initialPosition.z));
            }

            // Move the platform
            transform.position = newPosition;

            yield return null;
        }
    }
   
}

