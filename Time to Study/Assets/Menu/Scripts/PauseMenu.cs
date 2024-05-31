using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;

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
        pauseMenu.SetActive(false); // diactivate menu pause
    }
    public void PauseGame()
    {
        isPaused = true;
        AudioListener.pause = true;
        Time.timeScale = 0f; // will stop move and animation   
        pauseMenu.SetActive(true); // activate menu pause
    }

    public void Backtomenu()
    {
        SceneManager.LoadScene(0);
        Resumethgame();
    }

    public void Backtosettings()
    {

    }

    public void Closethegame()
    {

    }


}
