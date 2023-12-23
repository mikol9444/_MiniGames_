using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using TMPro;
public class _08PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask platformLayer;
    public float timeBetweenJumps = 2f; // Adjust as needed
    public Vector2 movementVector;
    public Rigidbody rb;
    public bool canJump = true;

    public float minX = -5f; // Minimum X-axis bound
    public float maxX = 5f;  // Maximum X-axis bound
    public float moveSpeedX=15f;
    public float gravity=-25f;
    public float heighttoLoose = -10f;
    public float heightThreshold = 10f;
    public TextMeshProUGUI heightText;
    public Transform lava;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, jumpForce, 0));
        Physics.gravity = new Vector3(0f,gravity,0f);
        InputManager.Instance._MovementEvent += OnMove;
    }
    private void OnDisable() {
        InputManager.Instance._MovementEvent -= OnMove;
    }
    private void Update() {
        if (rb.velocity.y <0)
        {
                        canJump=true;
        }
        else{
            heighttoLoose = transform.position.y -heightThreshold;
            lava.position = new Vector3(0,heighttoLoose,0);
            heightText.text = $"Height: {transform.position.y:F1}";

        }
        if (IsGrounded()&&canJump)
        {

            Jump();
        }
        MovePlayer(movementVector);
        if (transform.position.y< heighttoLoose)
        {
            FindObjectOfType<Popup>().OnActivate("YOU LOST, REPLAY ? ");
            gameObject.SetActive(false);
        }
    }
void MovePlayer(Vector2 movementVector)
{
    Vector3 newPosition = transform.position + new Vector3(movementVector.x, 0f, 0f) * Time.deltaTime * moveSpeedX;
    newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
    transform.position = newPosition;

    // Set rotation based on movement direction
    if (movementVector.x > 0)
    {
        // Rotate to face in the positive X direction (0, 90, 0)
        transform.eulerAngles = new Vector3(0f, -90f, 0f);
    }
    else if (movementVector.x < 0)
    {
        // Rotate to face in the negative X direction (0, -90, 0)
        transform.eulerAngles = new Vector3(0f, 90f, 0f);
    }
}
    public void MoveOnX() {

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,0f);
        rb.AddForce(new Vector3(0, jumpForce, 0));
        canJump = false;
    }
private void OnMove(Vector2 dir) => movementVector = dir;
    bool IsGrounded()
    {
        // Raycast to check if the player is on a platform
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, platformLayer)&& canJump)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                // Disable collisions when moving upwards
                // Physics.IgnoreCollision(GetComponent<Collider>(), hit.collider, rb.velocity.y > 0);
                Jump();
                return true;
            }
        }

        return false;
    }

}
