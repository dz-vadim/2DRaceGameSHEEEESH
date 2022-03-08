using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    
    int currentLevel = 0;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (currentLevel == 0)
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }

        if (currentLevel > levelButtons.Length)
        {
            currentLevel = levelButtons.Length;
        }

        for (int i = 0; i < currentLevel; i++)
        {
            levelButtons[i].interactable = true;
        }

    }

    public void loadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
