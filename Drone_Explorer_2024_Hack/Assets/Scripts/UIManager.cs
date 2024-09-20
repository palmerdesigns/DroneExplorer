using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class UIManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;
    public HealthManager healthManager;

    #region Subscriptions

    private void OnEnable()
    {
        Bullet.OnBulletDestroyEnemy += UpdateScore;
        
    }

    private void OnDisable()
    {
        Bullet.OnBulletDestroyEnemy -= UpdateScore;
    }

    #endregion

    private void Update()
    {
        scoreText.text = "Score pts: " + score;
        healthText.text = "Health: " + healthManager.maxHealth;
        livesText.text = "Lives: " + healthManager.lives;
        
        Win(50);
    }

    public void UpdateScore(int points)
    {
        score += points;
    }


    

    public void Win(int maxScore)
    {
        if (score >= maxScore)
        {
            Debug.Log("You win!");
        }
    }
}
