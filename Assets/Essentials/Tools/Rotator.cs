using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Essentials
{
    public class Rotator : MonoBehaviour
    {
        public Transform targetTransform; // The Transform to rotate around
        public float rotationSpeed = 30f; // Rotation speed in degrees per second
        public bool clockwiseRotation = true; // Rotate clockwise or counter-clockwise


        public enum RotationType
        {
            RotateAroundSelf,
            RotateAroundTarget,
            Both
        }

        public RotationType rotationType = RotationType.RotateAroundSelf;

        private void FixedUpdate()
        {
            if (rotationType == RotationType.RotateAroundTarget)
            {
                if (targetTransform != null)
                {
                    RotateAroundTarget();
                }
                else
                {
                    Debug.LogWarning("Target Transform not set for Rotator on " + gameObject.name);
                }
            }
            else if (rotationType == RotationType.RotateAroundSelf)
            {
                RotateAroundSelf();
            }
            else if (rotationType ==RotationType.Both)
            {
                if (targetTransform != null)
                RotateAroundTarget();
                RotateAroundSelf();
            }
        }

        private void RotateAroundTarget()
        {
            // Determine the rotation direction based on the 'clockwiseRotation' flag
            float rotationDirection = clockwiseRotation ? -1f : 1f;

            // Calculate the rotation amount based on the speed and frame time
            float rotationAmount = rotationSpeed * rotationDirection * Time.deltaTime;

            // Rotate the object around the target Transform
            transform.RotateAround(targetTransform.position, Vector3.up, rotationAmount);
        }

        public void RotateAroundSelf()
        {
            // Rotate the object around its own center using the specified rotation direction
            float rotationDirection = clockwiseRotation ? -1f : 1f;
            float rotationAmount = rotationSpeed * rotationDirection * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationAmount);
        }
    }
}
