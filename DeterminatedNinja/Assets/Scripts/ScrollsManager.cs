using TMPro;
using UnityEngine;

public class ScrollsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scrollsText;
    [SerializeField] TextMeshProUGUI totalScrollsText;
    [SerializeField] PlayerController playerController;
    [SerializeField] int totalScrolls;
    

    private void Start()
    {
        playerController.SetTotalScrolls(totalScrolls);
        totalScrollsText.text = totalScrolls.ToString("00");
    }

    public void AddScore(int score)
    {
        scrollsText.text = score.ToString("00");
    }
}
