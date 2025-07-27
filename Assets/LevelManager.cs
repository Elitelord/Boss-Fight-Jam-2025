using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetLevel(int level)
    {
        if (level != currentLevel)
        {
            currentLevel = level;
            Debug.Log("Level changed to: " + currentLevel);
            // You can trigger events here too
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}
