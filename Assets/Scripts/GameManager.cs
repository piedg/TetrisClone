using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score { get; private set; }
    public bool GameOver { get; set; }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
