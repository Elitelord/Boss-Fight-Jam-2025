using UnityEngine;
using TMPro;

public class LevelTracker : MonoBehaviour
{
    public TMP_Text levelText; // Assign in Inspector
    public GameObject enemyTwo; // Drag Enemy Two object here in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Level1"))
        {
            levelText.text = "Level: 1";
        }
        else if (other.CompareTag("Level2"))
        {
            levelText.text = "Level: 2";

            if (enemyTwo != null)
            {
                enemyTwo.SetActive(true); // Enable shooter at Level 2
            }
        }
    }
}
