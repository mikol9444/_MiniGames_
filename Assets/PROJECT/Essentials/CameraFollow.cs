using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 xBounds = new Vector3(-10f, 10f);
    [SerializeField] private Vector2 yBounds = new Vector3(-10f, 10f);
    [SerializeField] private Vector2 zBounds = new Vector3(-10f, 10f);

    public Vector3 cameraOffset;
    public Vector3 boundsOffset;
    public Color gizmoColor = Color.red;

    public bool lookAtPlayer;
    public bool rotateAroundPlayer;
    public float rotationSpeed = 5f;
    public float rotationOffsetX = 0f;
    public float rotationOffsetY = 0f;
    public float rotationOffsetZ = 0f;
    private void Awake()
    {
        if (!playerTransform) playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (!playerTransform) { Debug.LogWarning($"PlayerTransform is not attached to {this.name}"); this.enabled = false; }
    }

    private void LateUpdate()
    {
        if (lookAtPlayer)
        {
            Vector3 lookDirection = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(rotationOffsetX, rotationOffsetY, rotationOffsetZ);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        if (rotateAroundPlayer)
        {
            transform.RotateAround(playerTransform.position, Vector3.up, rotationSpeed);
            return;
        }
        // Calculate the desired position of the camera based on the player's position and offset
        Vector3 desiredPosition = playerTransform.position + cameraOffset;

        // Clamp the desired position to the given bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, xBounds.x + boundsOffset.x, xBounds.y + boundsOffset.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, yBounds.x + boundsOffset.y, yBounds.y + boundsOffset.y);
        float clampedZ = Mathf.Clamp(desiredPosition.z, zBounds.x + boundsOffset.z, zBounds.y + boundsOffset.z);
        desiredPosition = new Vector3(clampedX, clampedY, clampedZ);

        // Set the position of the camera to the desired position
        transform.position = desiredPosition;

        // Look at the player if necessary


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 center = new Vector3((xBounds.x + xBounds.y) / 2f, (yBounds.x + yBounds.y) / 2f, (zBounds.x + zBounds.y) / 2f) + boundsOffset;
        Vector3 size = new Vector3(Mathf.Abs(xBounds.y - xBounds.x), Mathf.Abs(yBounds.y - yBounds.x), Mathf.Abs(zBounds.y - zBounds.x));
        Gizmos.DrawCube(center, size);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying && playerTransform)
        {
            // Update camera position
            transform.position = playerTransform.position + cameraOffset;
            if (lookAtPlayer)
            {
                // Calculate the desired rotation of the camera based on the player's position and rotationOffsets
                Quaternion desiredRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
                Vector3 rotationOffsets = new Vector3(rotationOffsetX, rotationOffsetY, rotationOffsetZ);
                desiredRotation *= Quaternion.Euler(rotationOffsets);

                // Set the rotation of the camera to the desired rotation
                transform.rotation = desiredRotation;
            }

        }

    }

}
