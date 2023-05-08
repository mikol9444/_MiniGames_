using UnityEngine;

public class _02CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the camera should follow the player
    public  Vector3 offset; //  camera offst

    private Vector3 targetPosition; // Position where the camera should be

    private void FixedUpdate()
    {
        // Calculate the target position of the camera
        targetPosition = new Vector3(player.position.x, 0f, offset.z);

        // Move the camera towards the target position using lerp for smoothness
        transform.position = Vector3.Lerp(transform.position, targetPosition + offset, followSpeed * Time.fixedDeltaTime);
    }

}
