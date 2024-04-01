using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Animator animator;
    private const string NEW_SCORE = "NewScore";

    private void Start()
    {
        // newScoreText.gameObject.SetActive(false);

        HideHighScoreText();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameManager.Instance.OnNewScore += GameManager_OnNewScore;

        if (GameManager.Instance.GetHighScore() > 0)
        {
            UpdateHighScoreText();
        }
    }

    private void GameManager_OnNewScore(object sender, EventArgs e)
    {
        // Desactivate the current score not its increment logic
        animator.SetTrigger(NEW_SCORE);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            UpdateHighScoreText();
        }
    }

    private void Update()
    {
        UpdateScoreText();

        if (IsAnimStatePlaying(NEW_SCORE, 0))
        {
            scoreText.text = GameManager.Instance.newScore.ToString("00000");
        }
        else
        {
            // The State Anim has ended
        }

    }

    private void UpdateScoreText()
    {
        // scoreText.gameObject.SetActive(continueScoring);

        string formattedScoreText = GameManager.Instance.GetScore().ToString("00000");
        scoreText.text = formattedScoreText;
        // newScoreText.text = formattedScoreText;
    }

    private void UpdateHighScoreText()
    {
        ShowHighScoreText();
        string formattedHighScoreText = GameManager.Instance.GetHighScore().ToString("HI  00000");
        highScoreText.text = formattedHighScoreText;
    }

    private void HideHighScoreText()
    {
        highScoreText.enabled = false;
    }
    private void ShowHighScoreText()
    {
        highScoreText.enabled = true;
    }

    // Check if a certain animation state from the Animator is playing
    bool IsAnimStatePlaying(string stateName, int animLayer)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(animLayer);

        if (animatorStateInfo.IsName(stateName) && animatorStateInfo.normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
