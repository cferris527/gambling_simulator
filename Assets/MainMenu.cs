using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Mines");
    }

    public void Quit()
    {
        QuitGame();
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            // If running in the Unity Editor, stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running as a standalone build, quit the application
            Application.Quit();
        #endif
    }
}
