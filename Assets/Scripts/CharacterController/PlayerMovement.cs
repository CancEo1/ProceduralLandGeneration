using Unity.VisualScripting;
using UnityEngine;

// Player movement script using the old input system. Not permanent. Will switch to the new input system once I have a better understanding of it.
// The script allows for basic movement of the player character based on input from the keyboard, using a Rigidbody component for physics-based movement.
// The movement is influenced by the player's orientation, allowing for movement in the direction the player is facing.
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float roatationSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float hzInput;
    float vInput;

    Vector3 moveDir;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating due to physics interactions, ensuring that the player's movement is not affected by external forces that could cause unwanted rotation.
    }

    // Update is called once per frame
    void Update()
    {
        // Check for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        // Handle drag
        if (grounded)
        {
            rb.linearDamping = groundDrag; // Apply the specified ground drag to the Rigidbody when the player is grounded, providing resistance to movement and helping to create a more realistic feel when the player is on the ground.
        } else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        // HandleRotation();
    }

    private void PlayerInput()
    {
        hzInput = Input.GetAxisRaw("Horizontal"); // Get the horizontal input axis (e.g., A/D keys or left/right arrow keys) and store it in the hzInput variable for later use in movement calculations.
        vInput = Input.GetAxisRaw("Vertical"); // Get the vertical input axis (e.g., W/S keys or up/down arrow keys) and store it in the vInput variable for later use in movement calculations.
    }

    private void MovePlayer()
    {
        // Calculate movement direction.
        moveDir = orientation.forward * vInput + orientation.right * hzInput;

        // Apply force to the Rigidbody.
        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.y);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void HandleRotation()
    {
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, roatationSpeed * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        // Draw a ray in the editor to visualize the ground check, showing the distance from the player's position downwards to indicate where the ground check is being performed.
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f));
    }
}
