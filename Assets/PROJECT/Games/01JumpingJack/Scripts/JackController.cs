using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackController : MonoBehaviour
{
    public Rigidbody rb;
    public InputManager inputManager;
    public float speed = 2f;
    public float jumpForce = 3f;
    public float maxMagnitude = 15f;

    public LayerMask GroundLayers;
    public float GroundedOffset = -0.25f;
    public float GroundedRadius;
    public bool Grounded;
    private void Start()
    {
        inputManager._inputReader.JumpEvent += Jumping;
    }
    private void Update()
    {
        GroundedCheck();
        Vector3 horizontalVelocity = Vector3.right * speed;
        Vector3 verticalVelocity = Vector3.up * rb.velocity.y;
        Vector3 combinedVelocity = horizontalVelocity + verticalVelocity;
        rb.velocity = Vector3.ClampMagnitude(combinedVelocity, maxMagnitude);

    }
    private void OnApplicationQuit()
    {
        inputManager._inputReader.JumpEvent -= Jumping;
    }

    public void Jumping(bool state)
    {
        if (state && Grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }
}
