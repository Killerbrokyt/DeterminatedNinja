using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int gameTime = 120;
    private int multiplicator = 67;
    private float timeLeft;
    private bool timerRunning = true;
    private int gameShowedTime;


    private void Start()
    {
        timeLeft = gameTime;
        gameShowedTime = gameTime * multiplicator;
        scoreText.text = gameShowedTime.ToString("00000");
    }

    private void Update()
    {
        if (!timerRunning) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            timerRunning = false;
            ReloadScene();
        }

        gameShowedTime = Mathf.CeilToInt(timeLeft * multiplicator);
        scoreText.text = gameShowedTime.ToString("00000");
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
