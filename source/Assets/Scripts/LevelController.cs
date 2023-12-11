using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int score;
    private int enemiesOnScene;

    public static LevelController Instance { get; set; }
    int sceneIndex;
    int levelReached;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }

    private void Awake()
    {
        Instance = this;
    }

    public void EnemiesCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesOnScene = enemies.Length;
        Debug.Log(enemiesOnScene + " Осталось до перехода на сл. уровнь");
        
        if (enemiesOnScene == 0)
            Hero.Instance.Invoke("SetWinPanel", 1.1f); 
            PlayerPrefs.SetInt("levelReached", sceneIndex);

    }







}
