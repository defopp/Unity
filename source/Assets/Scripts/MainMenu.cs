using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        int goTO = PlayerPrefs.GetInt("levelReached");
        SceneManager.LoadScene(goTO + 1);
    }

    public void OpenLevelsList()
    {
        SceneManager.LoadScene(1);
    }
}
