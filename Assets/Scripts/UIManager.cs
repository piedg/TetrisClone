using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] Button TryAgainBtn;
    [SerializeField] Button PlayBtn;
    [SerializeField] Button TutorialQuitBtn;
    [SerializeField] Button PauseQuitBtn;

    private void Start()
    {
        ScoreText.enabled = false;
        TryAgainBtn.onClick.AddListener(() => GameManager.Instance.RestartScene());
        PlayBtn.onClick.AddListener(() => DisableTutorial());
        TutorialQuitBtn.onClick.AddListener(() => GameManager.Instance.QuitGame());
        PauseQuitBtn.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    private void Update()
    {
        ScoreText.text = "SCORE: " + GameManager.Instance.Score;
        
        PausePanel.SetActive(GameManager.Instance.IsPause);

        if (GameManager.Instance.GameOver)
        {
            GameOverPanel.SetActive(true);
        }
    }

    private void DisableTutorial()
    {
        GameManager.Instance.ShowingTutorial = false;
        TutorialPanel.SetActive(false);
        ScoreText.enabled = true;
    }
}
