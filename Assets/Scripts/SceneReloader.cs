using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        // Get the name of the currently active scene
        // string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

