using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        // Блокировка курсора и скрытие его
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }


}
