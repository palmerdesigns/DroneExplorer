using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

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
