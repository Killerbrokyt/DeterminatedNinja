using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the highest unlocked level from PlayerPrefs
        int unLockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        //Get the Layout Group gameobject with all the levels buttons
        Transform levels = transform.GetChild(0).GetChild(1);

        for (int i = 0; i < levels.childCount; i++)
        {
            Button levelButton = levels.GetChild(i).GetComponent<Button>();
            // Enable the button if the level is unlocked
            levelButton.interactable = (i < unLockedLevel);
        }

    }
}
