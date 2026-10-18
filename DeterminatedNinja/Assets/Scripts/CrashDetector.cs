using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private ScrollsManager scoreManager;
    [SerializeField] private BoxCollider2D atackCollider;

    int scrollsCount;
    bool isOnRage = false;
    private float crashExplosiveDelay = 0.5f;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            if(atackCollider.enabled && atackCollider.IsTouching(collision) || isOnRage)
            {
                Destroy(collision.gameObject);
                return;
            }
            ReloadScene();
        } else if (collision.CompareTag("Bomb"))
        {
            if (collision.TryGetComponent(out Animator bombAnimator)) // Busca el componente animator en la bomba
            {
                bombAnimator.enabled = true;
            }
            collision.enabled = false; // Para que solo explote una vez
            if(!isOnRage) Invoke(nameof(ReloadScene), crashExplosiveDelay);
        }else if (collision.CompareTag("Collectable"))
        {
            scrollsCount++;
            scoreManager.AddScore(scrollsCount);
            playerController.SetScrolls(scrollsCount);
            Destroy(collision.gameObject);
        }else if (collision.CompareTag("PowerUp"))
        {
            isOnRage = true;
            StartCoroutine(playerController.ActivateRage());
        }
    }

    public void DesactivateRage()
    {
        isOnRage= false;
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
