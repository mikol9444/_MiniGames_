using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _02FlappyController : MonoBehaviour
{
    public float movementSpeed = 10f; // Speed at which the player moves
    public float jumpForce = 500f; // Force of the player's jump

    private Rigidbody rb; // Reference to the player's Rigidbody component
    private bool jumpPressed = false; // Flag indicating if the jump button has been pressed
    public float gravityMultiplikator = 2f;
    private PopUpManager man;

    private void Awake()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -9.81f * gravityMultiplikator, 0f);
        man = FindObjectOfType<PopUpManager>();
    }
    private void OnEnable()
    {
        InputManager.Instance._JumpEvent += ListenToJumpInput;
    }
    private void OnDisable()
    {
        InputManager.Instance._JumpEvent -= ListenToJumpInput;
    }
    private void ListenToJumpInput(bool state) { jumpPressed = state; }
    private void FixedUpdate()
    {
        // Apply horizontal movement to the player
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

        // Apply jump force if the jump button has been pressed and the player is on the ground
        if (jumpPressed)
        {
            if (rb.velocity.y <= 0)
            {
                rb.velocity = Vector3.zero;
            }
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            PlaySound();
            jumpPressed = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.LogWarning("GAME OVER!");
            AudioManager_Test.Instance.PlaySound("die");
            _02HighScore.Instance.UpdateHighScore(transform.position.x);
            rb.velocity = Vector3.zero;
            man.TogglePopup("distance " + transform.position.x.ToString("0") + " m");
            Destroy(this);
        }
    }
    public void PlaySound()
    {
        int i = Random.Range(1, 4);
        AudioManager_Test.Instance.PlaySound(i.ToString());
    }
}
