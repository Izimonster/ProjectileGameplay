using System;
using TMPro;
using UnityEngine;

public enum GameState
{
    Playing,
    Won,
    Lost
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int targetScore = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI resultText;
    public GameState currentState = GameState.Playing;
    public int ammo = 10;
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        UpdateScoreUI();
        UpdateAmmoUI();
        UpdateGameState(GameState.Playing);
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + ammo;
    }

    void UpdateGameState(GameState newState)
    {
        currentState = newState;
        if (currentState == GameState.Playing)
        {
            resultText.text = String.Empty;
        }
        else if (currentState == GameState.Won)
        {
            resultText.text = "YOU WIN!";
        }
        else if (currentState == GameState.Lost)
        {
            resultText.text = "YOU LOSE";
        }
    }
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        if (score >= targetScore)
        {
            UpdateGameState(GameState.Won);
        }
        UpdateScoreUI();
    }
    public bool CanShoot()
    {
        return currentState ==
            GameState.Playing && ammo > 0;
    }
    public void UseAmmo()
    {
        ammo--;
        UpdateAmmoUI();
        if (ammo == 0 && score < targetScore)
        {
            UpdateGameState(GameState.Lost);
        }
    }
}