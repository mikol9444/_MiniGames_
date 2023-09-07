using UnityEngine;
using UnityEngine.UI;
using Essentials;
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class _01EvaMovementController : MonoBehaviour
{

    [Header("Settings - Movement")]
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float maxVelocityX = 10f;
    [SerializeField] private float maxVelocityY = 25f;

    [Header("Settings - FlyMode")]
    [SerializeField] private float maxFlyDuration = 1.75f;
    [SerializeField] private float timeLeftToFly = 1.75f;
    [SerializeField] private bool canFly = true;
    [SerializeField] private float gravityMultiplikator = 2f;
    [SerializeField] private float jetpackPower = 10f;
    [SerializeField] public Slider jetpackFuelSlider;

    //Intern
    [HideInInspector] private bool isPlayingSound = false;
    [HideInInspector] public bool isAlive = true;
    private Rigidbody rb;
    private CapsuleCollider coll;
    private _01EvaInputListener input;
    public _01EvaAnimatorController anim;
    private TrailRenderer[] trails = new TrailRenderer[2];

    // PROPERTIES
    [SerializeField] public bool isGrounded = false; // Flag to track if the player is currently jumping.
    [SerializeField] public bool firstJumpPerforemed = false;
    [SerializeField] public bool secondJumpPerformed = false;
    [SerializeField] public int jumpCount = 0; // Track the number of jumps.
    [SerializeField] private float lastJumpTime = 0f; // Record the time of the last jump.
    [SerializeField] private float jumpCooldown = 1f;
    [SerializeField] private float jumpPower = 25f;
    public float LastJumpTime { get { return lastJumpTime; } set { if (value < 0) return; else lastJumpTime = value; } }
    public float sphereCastRadius = 1.0f; // Adjust the radius in the Inspector
    public LayerMask hitLayers; // Define the layers you want to consider for hits
    public string targetTag = "Target"; // Specify the tag you want to check
    public Vector3 sphereCastOffset; // Adjust the offset in the Inspector

    private float TimeLeftToFly
    {
        get => timeLeftToFly;
        set
        {
            timeLeftToFly = value;
            if (timeLeftToFly < 0f)
            {
                timeLeftToFly = 0f;
                canFly = false;
                jumpCount = 0;
            }
            if (timeLeftToFly / maxFlyDuration > 0.5f)
            {
                canFly = true;
            }
            if (timeLeftToFly > maxFlyDuration)
            {
                timeLeftToFly = maxFlyDuration;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        input = FindObjectOfType<_01EvaInputListener>();
        if (!input) { Debug.LogWarning("no inputlistenere in scene"); enabled = false; }
        trails = GetComponentsInChildren<TrailRenderer>();
        Physics.gravity = new Vector3(0f, -9.81f * gravityMultiplikator, 0f);
        jetpackFuelSlider = FindObjectOfType<_01PlaceHolder>()?.GetComponent<Slider>();


    }
    private void Start()
    {
        InputManager.Instance._CrouchEvent += OnCrouchPerformed;
        InputManager.Instance._MovementEvent += OnMovementActive;
        InputManager.Instance._JumpEvent += CountJumps;
        // anim.GetComponent<_01EvaAnimatorController>();
    }
    private void OnDisable()
    {

        InputManager.Instance._CrouchEvent -= OnCrouchPerformed;
        InputManager.Instance._MovementEvent -= OnMovementActive;
        InputManager.Instance._JumpEvent -= CountJumps;
    }
    private void CountJumps(bool performed)
    {
        jumpCount++;
        if (performed)
        {

            if (jumpCount > 3) jumpCount = 3;
            switch (jumpCount)
            {
                case 0:

                    break;
                case 1:
                    if (isGrounded)
                    {
                        anim.setGroundedValue(false);
                        firstJumpPerforemed = true;
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        LastJumpTime = jumpCooldown;
                        anim.SetBlendValue(0f);
                    }

                    break;
                case 2:
                    if (firstJumpPerforemed)
                    {
                        firstJumpPerforemed = false;
                        secondJumpPerformed = true;
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        anim.SetBlendValue(1f);

                    }
                    break;
            }
        }

    }
    private void FixedUpdate()
    {
        float value = Mathf.Clamp(timeLeftToFly / maxFlyDuration, 0f, 1f);
        jetpackFuelSlider.value = value;
        LastJumpTime -= Time.deltaTime;

        // Vertical Movement
        bool ableToFly = !isGrounded && canFly && input.IsJumpingPressed;
        if (ableToFly)
        {
            if (jumpCount >= 3)
            {
                anim.SetBlendValue(2f);
                rb.AddForce(Vector3.up * jetpackPower, ForceMode.Force);
                TimeLeftToFly -= Time.deltaTime;
                foreach (var item in trails)
                {
                    item.emitting = input.IsJumpingPressed && canFly;
                }
                if (!isPlayingSound)
                {
                    AudioManager_Test.Instance.PlaySound("jetpack");
                    isPlayingSound = true;
                }
            }


        }
        else
        {
            foreach (var item in trails)
            {
                item.emitting = false;
            }
            TimeLeftToFly += Time.deltaTime;
            if (isPlayingSound)
            {
                AudioManager_Test.Instance.StopSound("jetpack");
                isPlayingSound = false;
            }
        }
        if (rb.velocity.y <= -maxVelocityY)
        {
            anim.OnJumpPerformed(true);
            anim.SetBlendValue(3f);
            jumpCount = 3;
        }




        // Horizontal Movement
        if (input.MoveDirection != Vector3.zero)
        {
            rb.AddForce(new Vector3(input.MoveDirection.x, 0f) * movementSpeed);
        }
        else
        {
            if (Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                rb.velocity = Vector3.zero;
            }
        }

        //BREAKING

        //Horizontal
        if (rb.velocity.x > maxVelocityX)
        {
            rb.AddForce(Vector3.left * movementSpeed);
        }
        else if (rb.velocity.x < -maxVelocityX)
        {
            rb.AddForce(Vector3.right * movementSpeed);
        }

        //Vertical
        if (rb.velocity.y > maxVelocityY)
        {
            rb.AddForce(-Vector3.up * gravityMultiplikator * 10f);
        }
        else if (rb.velocity.y < -maxVelocityY)
        {
            rb.AddForce(Vector3.up * gravityMultiplikator * 8.5f);
        }

    }
    private void AddHorizontalVelocity(Vector3 dir, float speed)
    {

    }

    private void OnCrouchPerformed(bool state)
    {
        coll.center = state ? new Vector3(0f, .5f, 0f) : new Vector3(0f, 1.5f, 0f);
        coll.height = state ? 1.5f : 2.8f;
    }
    private void OnMovementActive(Vector2 dir)
    {
        if (dir.x > 0) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        else if (dir.x < 0) transform.rotation = Quaternion.Euler(0f, -90f, 0f);


    }


}








