using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    [RequireComponent(typeof(JackCollisions01))]
    public class JackController01 : MonoBehaviour
    {
        [Header("Setup Fields")]
        [SerializeField] private LayerMask WalkArea;
        [SerializeField] private Vector2 GroundedOffset = new Vector2(0f, 0.02f);
        [SerializeField] private float GroundedRadius = 0.16f;
        [SerializeField] private float gravityForce = 35;

        [SerializeField] private float speed = 4f;
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float jumpForce = 10f;


        [SerializeField] private bool Grounded = false;
        [SerializeField] private bool canJump = false;
        [SerializeField] private bool isInAir = true;

        private Rigidbody rb;
        private InputManager inputManager;
        private void Awake()
        {
            Physics.gravity = Vector3.down * gravityForce;
            rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            if (!inputManager) inputManager = FindObjectOfType<InputManager>();
            if (!inputManager) { Debug.LogWarning("INPUTMANAGER IS NOT ASSIGNED"); return; }
            inputManager._inputReader.JumpEvent += CanJump;
            inputManager._inputReader.TestEvent += ToggleAnimation;

        }
        private void OnDisable()
        {
            inputManager._inputReader.JumpEvent -= CanJump;
            inputManager._inputReader.TestEvent -= ToggleAnimation;
        }
        private void FixedUpdate()
        {
            Moving();
            Jumping();

        }
        private void Moving()
        {
            Vector3 horizontalVelocity = Vector3.right * speed;
            Vector3 verticalVelocity = Vector3.up * rb.velocity.y;
            Vector3 combinedVelocity = horizontalVelocity + verticalVelocity;
            rb.velocity = Vector3.ClampMagnitude(combinedVelocity, maxSpeed);
        }

        public void Jumping()
        {
            if (canJump && Grounded && !isInAir)
            {
                isInAir = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //StartCoroutine(jumpTimer());
                canJump = false;
            }
            Vector3 spherePosition = new Vector3(transform.position.x - GroundedOffset.x, transform.position.y - GroundedOffset.y,
              transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, WalkArea,
                QueryTriggerInteraction.Ignore);
        }
        private void OnCollisionEnter(Collision other)
        {
            if ((WalkArea.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                isInAir = false;
            }
        }
        // private IEnumerator jumpTimer()
        // {
        //     yield return new WaitForSeconds(0.15f);
        //     canJump = true;
        // }

        public void CanJump(bool state) => canJump = state;
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Vector3 center = new Vector3(transform.position.x - GroundedOffset.x, transform.position.y - GroundedOffset.y, transform.position.z);
            Gizmos.DrawSphere(center, GroundedRadius);
        }
        public void ToggleAnimation(bool state)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetBool("crawl", state);
            ResetCapsuleCollider(state);
        }
        public void ResetCapsuleCollider(bool state)
        {
            CapsuleCollider coll = GetComponent<CapsuleCollider>();
            if (!state)
            {
                coll.height = 2f;
                coll.center = new Vector3(0f, 1f, 0f);
            }
            else
            {
                coll.height = 1f;
                coll.center = new Vector3(0f, 0.5f, 0f);
            }
        }

    }
}