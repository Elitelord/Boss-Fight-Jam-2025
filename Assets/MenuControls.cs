using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void OpenSettings(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
