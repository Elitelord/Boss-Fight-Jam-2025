using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public GameObject[] groundPieces; // Assign all ground pieces in the inspector
    public float scrollSpeed = 5f;

    void Update()
    {
        foreach (GameObject ground in groundPieces)
        {
            if (ground != null)
            {
                ground.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
            }
        }
    }
}
