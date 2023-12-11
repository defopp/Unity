using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levels;
    public Button ToMainB;
    public Button ResetB;
    int levelReached;

    public void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached");

        for (int i = 0; i < levels.Length; i++)
            if (i + 1 > levelReached)
            levels[i].interactable = false;
    }

        
    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
    }

    public void ToMain() //(int level)
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("levelReached", 1);
        SceneManager.LoadScene(0);
    }
}






// public class LevelSelector : MonoBehaviour
// {
//     public Button level1b;
//     public Button level2b;
//     public Button ToMainB;
//     int levelComplete;

//     public void start()
//     {
//         levelComplete = PlayerPrefs.GetInt("LevelComplete"); 
//         level2b.interactable = false; // why dosent work blyat 

//         switch (levelComplete)
//         {
//             case 1:
//                 level2b.interactable = true;
//                 break;
//         }
//     }

//     public void LoadTo(int level)
//     {
//         SceneManager.LoadScene(level);
//     }
//     public void ToMain(int level)
//     {
//         SceneManager.LoadScene(0);
//     }

// }



