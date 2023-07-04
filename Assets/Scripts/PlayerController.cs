using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private GameObject bombPrefab; // Prefab of the bomb sprite

    [SerializeField]
    private int maxBombs = 3; // Maximum number of bombs that can be placed

    private Rigidbody2D rb;
    private Vector2 movementInput = Vector2.zero;
    private bool bombed = false;
    private int currentBombs = 0; // Current number of placed bombs

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

        // Calculate the rounded movement values
        float roundedX = Mathf.Round(movement.x);
        float roundedY = Mathf.Round(movement.y);

        // Check if the movement is mostly horizontal or vertical
        if (Mathf.Abs(roundedX) > Mathf.Abs(roundedY))
        {
            movementInput = new Vector2(roundedX, 0f);
        }
        else
        {
            movementInput = new Vector2(0f, roundedY);
        }
    }

    public void OnBomb(InputAction.CallbackContext context)
    {
        if (context.action.triggered && currentBombs < maxBombs)
        {
            bombed = true;
        }
    }

    private void Update()
    {
        Vector2 move = movementInput.normalized * playerSpeed;
        rb.velocity = move;

        if (bombed)
        {
            Vector3 bombPosition = transform.position; // Get the player's position
            Instantiate(bombPrefab, bombPosition, Quaternion.identity); // Instantiate the bomb at the player's position
            currentBombs++;
            bombed = false; // Reset the bombed flag
        }
    }
}
