using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YuzuValen.Utils;

public class StartMenuHandler : MonoBehaviourSingleton<StartMenuHandler>
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private Player player;

    private void Start()
    {
        NotificationHandler.Instance.ShowNotification("Level " + (SceneManager.GetActiveScene().buildIndex + 1));
        if (SceneManager.GetActiveScene().buildIndex == 0)
            OpenStartMenu();
        else
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1;
        player.EnablePlay();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenStartMenu()
    {
        startMenu.SetActive(true);
        Time.timeScale = 0;
        player.DisablePlay();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
