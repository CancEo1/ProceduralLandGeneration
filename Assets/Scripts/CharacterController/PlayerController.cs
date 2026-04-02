using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Manages player movement and jumping using Unity's CharacterController component. It processes input from the player to move the character in the game world,
// allowing for smooth movement and jumping mechanics while applying gravity to create a realistic experience.
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float playerSpeed = 5.0f;
    public float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction && groundedPlayer)
        {
            Movement();
            Debug.Log("Move action is active.");
        }

        if (jumpAction && groundedPlayer) 
        {
            Jump();
            Debug.Log("Jump action is active.");
        }
    }

    void Movement()
    {
        // Read input from the move action and calculate movement direction
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1); // Ensure diagonal movement isn't faster than straight movement

        if (move != Vector3.zero && groundedPlayer)
            transform.position = move;
        Debug.Log("Player is moving: " + move);

        // Move the player using the CharacterController
        Vector3 finalMove = move * playerSpeed * Time.deltaTime + playerVelocity * Time.deltaTime;
        controller.Move(finalMove * Time.deltaTime);
    }

    void Jump()
    {

        if (groundedPlayer)
        {
            // Slight downward force to keep the player grounded
            if (playerVelocity.y < -2f)
            {
                playerVelocity.y = -2f;
            }

            // Jump using WasPressedThisFrame()
            if (groundedPlayer && jumpAction.action.WasPressedThisFrame())
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }

            // Apply gravity to the player velocity
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
    }

    // Check if the player is grounded using the CharacterController's isGrounded property and update the groundedPlayer variable accordingly.
    bool isGrounded()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            groundedPlayer = true;
            return controller.isGrounded;
        }
        else
        {
            groundedPlayer = false;
            return controller.isGrounded;
        }
    }
}
