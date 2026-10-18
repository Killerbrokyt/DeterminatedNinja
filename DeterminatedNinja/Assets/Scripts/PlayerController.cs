using System.Collections;
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

    public Animator _animator;
    ObjectsDeleter objectsDeleter;
    CrashDetector detector;

    [SerializeField] private float teleportOutTime = 0.4f;
    [SerializeField] private float teleportInTime = 0.4f;
    [SerializeField] private float atackAnimationTime = 0.2f;
    [SerializeField] private float atackAnimationRageTime = 0.01f;
    [SerializeField] private float rageTime = 5f;
    [SerializeField] private BoxCollider2D atackCollider;

    bool isTeleporting;
    bool isAtacking;
    bool isOnRage = false;
    bool levelFinished = false;
    private const int lastLevel = 4;
    private int scrolls;
    private int totalScrolls;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        objectsDeleter = GetComponent<ObjectsDeleter>();
        detector = GetComponent<CrashDetector>();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        if (moveAction == null || isTeleporting) return;
        if(scrolls >= totalScrolls)
        {
            NextLevel();
        }

        if (moveAction.WasPressedThisFrame())
        {
            moveInput = moveAction.ReadValue<Vector2>();
            MovePlayer();
            if (!isAtacking && !isOnRage)
            {
                StartCoroutine(Atack());
            }
        }
        if (isOnRage)
        {
            objectsDeleter.DeleteByTag("PowerUp");
            StartCoroutine(Atack());
        }
    }


    private void MovePlayer()
    {
        int targetFloor = floorPosition;

        if (moveInput.y > 0 && floorPosition < 3) targetFloor++;
        else if (moveInput.y < 0 && floorPosition > 1) targetFloor--;

        if (targetFloor == floorPosition) return;

        StartCoroutine(TeleportRoutine(targetFloor));
    }

    private IEnumerator TeleportRoutine(int targetFloor)
    {
        isTeleporting = true;

        // 1) Animación de salida
        _animator.SetBool("Teleporting", true);
        yield return new WaitForSeconds(teleportOutTime);

        // 2) Mover al nuevo piso
        float y = targetFloor switch
        {
            1 => FloorHight.down,
            2 => FloorHight.middle,
            _ => FloorHight.up
        };
        transform.position = new Vector3(-11, y, 0);

        if (targetFloor == 1) transform.localScale = new Vector3(7f, 7f, 7f);
        else if (targetFloor == 2 && floorPosition == 1) transform.localScale = new Vector3(4.4f, 4.4f, 4.4f);

        floorPosition = targetFloor;

        // 3) Animación de llegada
        _animator.SetBool("Teleporting", false);
        _animator.SetBool("Teleporting2", true);
        yield return new WaitForSeconds(teleportInTime);

        // 4) Volver a Run
        _animator.SetBool("Teleporting2", false);
        isTeleporting = false;
    }

    public void SetScrolls(int scrolls)
    {
        this.scrolls = scrolls;
    }

    public void SetTotalScrolls(int totalScrolls)
    {
        this.totalScrolls = totalScrolls;
    }

    public IEnumerator Atack()
    {
        if(moveInput.x > 0 || isOnRage)
        {
            isAtacking = true;
            _animator.SetBool("OnAtack",true);
            atackCollider.enabled = true;
            if(!isOnRage) yield return new WaitForSeconds(atackAnimationTime);
            else yield return new WaitForSeconds(atackAnimationRageTime);
            _animator.SetBool("OnAtack", false);
            atackCollider.enabled = false;
            isAtacking=false;
        }
    }

    public IEnumerator ActivateRage()
    {
        isOnRage = true;
        yield return new WaitForSeconds(rageTime);
        detector.DesactivateRage();
        isOnRage =false;
    }

    void NextLevel()
    {
        if (levelFinished) return;
        levelFinished = true;

        int currentLevel = GetCurrentLevel();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Desbloquea el siguiente solo si supera lo que ya tenía, sin pasar del último
        int newUnlocked = Mathf.Min(Mathf.Max(unlockedLevel, currentLevel + 1), lastLevel);
        PlayerPrefs.SetInt("UnlockedLevel", newUnlocked); // Unlock the next level
        PlayerPrefs.Save();
        // Reload the current scene
        if (currentLevel > lastLevel) // If the player has completed the last level, go back to the main menu
        {
            SceneManager.LoadScene("Menu"); //TODO: Go to Win Scene
        } else
        {
            SceneManager.LoadScene($"Level{currentLevel + 1}"); // Load the next level
        }

    }

    int GetCurrentLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return int.Parse(sceneName.Replace("Level", ""));
    }

}
