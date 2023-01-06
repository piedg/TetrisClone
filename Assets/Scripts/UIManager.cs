using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Button TryAgainBtn;

    private void Start()
    {
        TryAgainBtn.onClick.AddListener(() => GameManager.Instance.RestartScene());
    }

    private void Update()
    {
        ScoreText.text = "SCORE: " + GameManager.Instance.Score;

        if(GameManager.Instance.GameOver)
        {
            GameOverPanel.SetActive(true);
        }
    }
}
