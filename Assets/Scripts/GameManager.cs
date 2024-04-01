using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Scrolling scrolling;
    [SerializeField] ScoreUI scoreUI;
    [SerializeField] private int maxLevelSpeed;
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private int speedIncreaseFactor;
    public EventHandler OnStateChanged;
    public EventHandler OnNewScore;

    public float newScore;
    
    private float levelSpeed;
    private int increaseSpeed;
    private int saveIncreaseSpeed;
    private int score;
    private int highScore;
    private float gameTime;
    private float lastIncreaseSpeed;
    private bool firstGameSession;

    private const string highScoreKey = "HighScore";
    private const string firstGameSessionKey = "FirstGameSession";

    private enum State
    {
        WaitingToStart,
        GamePlaying,
        GameOver,
    }

    private State state;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is more than one GameManager Instance!");
        }
        else
        {
            Instance = this;
        }

        state = State.WaitingToStart;

        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        // firstGameSession = PlayerPrefs.GetInt;

        //Load the boolean value from PlayerPrefs
        firstGameSession = PlayerPrefs.GetInt(firstGameSessionKey, 0) == 0;

    }

    private void Start()
    {
        lastIncreaseSpeed = increaseSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (!firstGameSession)
                {
                    state = State.GamePlaying;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        state = State.GamePlaying;
                        PlayerPrefs.SetInt(firstGameSessionKey, true ? 1 : 0);
                        // firstGameSession = true;
                    }
                }

                // PlayerPrefs.SetInt(firstGameSessionKey, true ? 1 : 0);
                // firstGameSession = false;


                // state = State.GamePlaying;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;

            case State.GamePlaying:
                if (!Player.Instance.IsDead())
                {
                    UpdateScore();
                }
                else
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
    }

    private void UpdateScore()
    {
        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        gameTime += Time.deltaTime * scoreMultiplier;
        score = Mathf.FloorToInt(gameTime);
    }

    public float Level(float defaultSpeed)
    {
        // Calculate the Increase speed according to the Score
        increaseSpeed = score * 1 / speedIncreaseFactor;
        saveIncreaseSpeed = score * 1 / speedIncreaseFactor;

        // Limits the maximum speed of the Level (game difficulty)
        if (increaseSpeed > maxLevelSpeed)
        {
            increaseSpeed = maxLevelSpeed;
            // Debug.Log(increaseSpeed);
        }

        // Check if there is a New score and fire the event
        if (saveIncreaseSpeed > lastIncreaseSpeed)
        {
            OnNewScore?.Invoke(this, EventArgs.Empty);

            newScore = saveIncreaseSpeed * speedIncreaseFactor;
        }
        lastIncreaseSpeed = saveIncreaseSpeed;

        levelSpeed = defaultSpeed + increaseSpeed;
        return levelSpeed;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(highScoreKey);
        PlayerPrefs.DeleteKey(firstGameSessionKey);
    }

}
