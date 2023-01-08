using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score { get; private set; } = 0;
    public bool GameOver { get; set; } = false;
    public bool IsPause { get; set; } = false;
    public bool ShowingTutorial { get; set; } = true;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        Time.timeScale = IsGameInPause() ? 0 : 1;

        if (Input.GetKeyDown(KeyCode.P) ||
            Input.GetKeyDown(KeyCode.Escape) &&
            !ShowingTutorial &&
            !GameOver)
        {
            IsPause = !IsPause;
        }
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGameInPause()
    {
        return GameOver || IsPause || ShowingTutorial;
    }
}
