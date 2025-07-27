using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public Transform[] groundParts;       // The 5 ground pieces
    public float startSpeed = 2f;         // Initial speed
    public float speedIncreaseInterval = 10f;  // Every X seconds, increase speed
    public float speedIncrement = 0.5f;   // How much to increase each time

    private float currentSpeed;
    private float timer;

    void Start()
    {
        currentSpeed = startSpeed;
        timer = 0f;
    }

    void Update()
    {
        MoveGround();
        UpdateSpeed();
    }

    void MoveGround()
    {
        foreach (Transform part in groundParts)
        {
            part.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
    }

    void UpdateSpeed()
    {
        timer += Time.deltaTime;

        if (timer >= speedIncreaseInterval)
        {
            currentSpeed += speedIncrement;
            timer = 0f;
        }
    }
}
