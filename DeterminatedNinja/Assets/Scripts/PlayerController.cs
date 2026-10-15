using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private ScoreManager scoreManager;

    public static class FloorHight
    {
        public const float down = 0.87f;
        public const float middle = 3.69f;
        public const float up = 8.51f;
    }

    int floorPosition = 1;

    InputAction moveAction;
    Vector2 moveInput;

    int activePowerUpsCount;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if (moveAction == null) return;

        if (moveAction.WasPressedThisFrame())
        {
            moveInput = moveAction.ReadValue<Vector2>();
            MovePlayer();
        }
    }


    void MovePlayer()
    {
        if (moveInput.y > 0)
        {
            if (floorPosition == 3) return;
            Vector3 newPosition;
            Vector3 newScale = new Vector3(4.4f, 4.4f, 4.4f);
            if (floorPosition == 1)
            {
                newPosition = new Vector3(-11, FloorHight.middle, 0);
                transform.position = newPosition;
                transform.localScale = newScale;
            } else 
            {
                newPosition = new Vector3(-11, FloorHight.up, 0);
                transform.position = newPosition;
            }
            floorPosition++;
        } else if (moveInput.y < 0) 
        {
            if (floorPosition == 1) return;
            Vector3 newPosition;
            Vector3 newScale = new Vector3(7f, 7f, 7f);
            if (floorPosition == 3)
            {
                newPosition = new Vector3(-11, FloorHight.middle, 0);
                transform.position = newPosition;
            } else
            {
                newPosition = new Vector3(-11, FloorHight.down, 0);
                transform.position = newPosition;
                transform.localScale = newScale;
            }
            floorPosition--;
        } 
    }

}
