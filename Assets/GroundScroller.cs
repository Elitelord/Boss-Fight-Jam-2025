using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float resetPositionX = -20f; // X position at which the ground resets
    public float startPositionX = 20f;  // X position it resets to

    void Update()
    {
        // Move the ground left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // If ground moves past reset point, move it back to start
        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPos = new Vector3(startPositionX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
