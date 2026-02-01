using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class GameOverScreen : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] Canvas screen;
    [SerializeField] TMP_Text label;
    [SerializeField] TMP_Text moneyEarnedText;

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

    public static void Enable(bool victory)
    {
        instance.screen.enabled = true;
        Time.timeScale = 0f;
        instance.label.text = victory ? "You win!" : "You lose!";
        instance.moneyEarnedText.text = victory ? "You earned " + "$" + MainScreen.MoneyEarned.ToString("n2") + " in " + MainScreen.GameSpan.ToString(@"mm\:ss\.ff") + "!" : "";
    }
}
