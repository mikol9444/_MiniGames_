using UnityEngine;
using System.Collections;


public class _05MovingPlatform01 : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    public float Speed { get { return moveSpeed; } set { moveSpeed = value; } }
    [SerializeField] private bool lookLeft = true; // New bool for looking direction
    public float movementDirection = 1f; // Forward or backward movement direction
    private Vector3 initialPosition; // Initial position to move back to
    [SerializeField] public float moveDistance { get; set; }
    public static int ID { get; set; }
    public Coroutine movementCoroutine; // Reference to the movement coroutine

    public void Initialize(bool platformForward = true,float speed=1f)
    {
        Speed = speed;
        GetComponent<Renderer>().material.color = GetRandomColor();

        initialPosition = transform.position; // Store the initial position

        // Start the movement coroutine
        movementCoroutine = StartCoroutine(MovePlatform(platformForward));
        gameObject.name += ID++.ToString();
    }
    public static Color GetRandomColor()
    {
        // float[] colorValues = new float[3];
        // for (int i = 0; i < colorValues.Length; i++)
        //     colorValues[i] = UnityEngine.Random.Range(0, 1f);
        // return new Color(colorValues[0], colorValues[1], colorValues[2], 1f);
        return FindObjectOfType<_05GameManager01>().gradientValue();
    }
    private IEnumerator MovePlatform(bool platformForward = true)
    {
        if (platformForward)
        {
            while (true)
            {
                // Calculate the new position based on the current movement direction
                Vector3 newPosition = transform.position + transform.forward * moveDistance * movementDirection * moveSpeed * Time.deltaTime;

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
        else
        {
            while (true)
            {
                // Calculate the new position based on the current movement direction
                Vector3 newPosition = transform.position + moveDistance * movementDirection * moveSpeed * Time.deltaTime * transform.right;

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
    private void SplitCube(_05MovingPlatform01 LastCube)
    {
        if (LastCube == null)
        {
            Debug.LogWarning("Lastcube = null !??!!?");
            return;
        }

    }

}

