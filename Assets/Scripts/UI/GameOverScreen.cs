using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class GameOverScreen : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] Canvas screen;

    static GameOverScreen instance;

    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void Enable()
    {
        instance.screen.enabled = true;
        Time.timeScale = 0f;
    }
}
