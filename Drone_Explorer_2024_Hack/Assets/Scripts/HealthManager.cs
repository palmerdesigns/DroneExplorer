using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This attribute will make the script run in edit mode
[ExecuteInEditMode]
public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int lives = 3;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;

    private void Update()
    {
        healthText.text = "Health: " + maxHealth;
        livesText.text = "Lives: " + lives;
        CheckHealthLimit(100);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "shockwave":
                Debug.Log("player hit shockwave");
                TakeDamage(10);
                break;
            case "health":
                Debug.Log("player hit health");
                Heal(10);
                break;
        }
    }
    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
    }

    public void Heal(int heal)
    {
        maxHealth += heal;
    }

    public void CheckHealthLimit(int limit)
    {
        if (maxHealth > limit)
        {
            maxHealth = limit;
        }
        else if (maxHealth < 0)
        {
            lives--;
            maxHealth = 100;
            if (lives < 0)
            {
                lives = 0;
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
