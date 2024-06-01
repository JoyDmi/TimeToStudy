using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;
    private bool wasCursorVisible;
    private CursorLockMode previousCursorLockMode;

    // Метод для установки видимости курсора
    public void SetCursorState(CursorLockMode lockMode, bool visible)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = visible;
    }

    void Start()
    {
        pauseMenu.SetActive(false); // inactive menupause on start game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resumethgame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resumethgame()
    {
        isPaused = false;
        AudioListener.pause = false;
        Time.timeScale = 1f; // resume time
        pauseMenu.SetActive(false); // deactivate menu pause

        // Restore cursor state
        SetCursorState(previousCursorLockMode, wasCursorVisible);
    }

    public void PauseGame()
    {
        isPaused = true;
        AudioListener.pause = true;
        Time.timeScale = 0f; // will stop move and animation   
        pauseMenu.SetActive(true); // activate menu pause

        // Save current cursor state
        wasCursorVisible = Cursor.visible;
        previousCursorLockMode = Cursor.lockState;

        // Ensure cursor is visible during pause
        SetCursorState(CursorLockMode.None, true);
    }

    public void Backtomenu()
    {
        SceneManager.LoadScene(0);
        Resumethgame();
    }

    public void Backtosettings()
    {
        // Implement settings logic if needed
    }

    public void Closethegame()
    {
        Application.Quit();
    }
}
