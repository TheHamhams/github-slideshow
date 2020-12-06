using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerRaycast playerRaycastScript;

    public GameObject birdPrefab;
    public GameObject eatDialog;
    public GameObject gameOverScreen;
    public GameObject bringFoodDialog;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI subScoreText;
    public TextMeshProUGUI timeLeftBonusText;
    public TextMeshProUGUI totalScoreText;

    public Button restartButton;

    Vector2 spawnPosition;

    
    public int foodCollected;
    public int foodLeft = 15;
    
    public float totalScore;
    public float score = 0;
    public float timeLeft;
    float spawnRange = 5;
    float spawnPositionY;

    public bool isActive;
    public bool timerActive;
    public bool isGameOver;
    public bool victory;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRaycastScript = GameObject.Find("Player").GetComponent<PlayerRaycast>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        FoodCounter();
        BringFood();        
    }

    void BringFood()
    {
        if (foodCollected == foodLeft)
        {
            bringFoodDialog.SetActive(true);
        }
        else
        {
            bringFoodDialog.SetActive(false);
        }
    }

    void SpawnEnemy()
    {

        spawnPositionY = Random.Range(-spawnRange, spawnRange);
        spawnPosition = new Vector2(-14, spawnPositionY);
        if (isActive)
        {
            Instantiate(birdPrefab, spawnPosition, birdPrefab.transform.rotation);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        if (foodCollected < foodLeft)
        {
            score += scoreToAdd;
        }
        
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        isActive = true;
        timerActive = true;
        timeLeft = 60;
       
        InvokeRepeating("SpawnEnemy", 4, 1.25f);
        StartCoroutine(EatText());
        
    }
    
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isActive = false;
        timerActive = false;
        isGameOver = true;
        score = Mathf.Round(score);
        timeLeft = Mathf.Round(timeLeft);
        totalScore = score + (Mathf.Round(timeLeft * 5));
        subScoreText.text = "Sub Score: " + score;
        timeLeftBonusText.text = "Time Left Bonus: " + timeLeft + " X 5 = " + (Mathf.Round(timeLeft) * 5);
        totalScoreText.text = "Total Score: " + totalScore;
    }

    IEnumerator EatText()
    {
        eatDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        eatDialog.SetActive(false);
    }

    void Timer()
    {
        if (timerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
      
                timerText.text = ("Time left: " + Mathf.RoundToInt(timeLeft));
            }
            else
            {
                timeLeft = 0;
                timerActive = false;
                isActive = false;
                GameOver();
            }
        }
    }

    void FoodCounter()
    {
        if (foodCollected > foodLeft)
        {
            foodCollected = foodLeft;
        }
        foodText.text = ("Food Collected: " + foodCollected + " / " + foodLeft);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
