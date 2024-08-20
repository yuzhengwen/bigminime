using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishZone : MonoBehaviour
{
    public bool isLastLevel = false;

    public SerializedDictionary<int, int> coinRequirements = new()
    {
        {0, 1 },
        {10, 2 },
        {20, 3 }
    };
    public int keyRequirement = 3;

    public GameObject finishScreen;
    public TMPro.TextMeshProUGUI finishText;
    public GameObject starsHolder;
    public TMPro.TextMeshProUGUI countdownText;
    private void Start()
    {
        if (finishScreen != null && finishScreen.activeSelf)
            finishScreen.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            finishScreen.SetActive(true);
            Time.timeScale = 0;
            player.DisablePlay();
            var stats = player.stats;

            bool success = stats.keys >= keyRequirement;
            ShowFinishScreen(success, CalculateStars(stats.coins));
        }
    }
    private void ShowFinishScreen(bool success, int stars)
    {
        finishScreen.SetActive(true);
        finishText.text = success ? (isLastLevel ? "Game End. Congrats!" : "Level Complete!") : "Level Failed!";
        DisplayStars(stars);
        StartCoroutine(Countdown(success, isLastLevel));
    }

    private IEnumerator Countdown(bool success, bool isLastLevel)
    {
        // countdown 5s
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        if (!success)
            // if fail restart the level
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        else
        {
            if (isLastLevel)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private int CalculateStars(int coins)
    {
        int stars = 0;
        foreach (var item in coinRequirements)
        {
            if (coins >= item.Key)
                stars = item.Value;
        }
        return stars;
    }
    private void DisplayStars(int stars)
    {
        Image[] starImages = new Image[starsHolder.transform.childCount];
        for (int i = 0; i < starsHolder.transform.childCount; i++)
        {
            starImages[i] = starsHolder.transform.GetChild(i).GetComponent<Image>();
            starImages[i].color = i < stars ? Color.white : Color.gray;
        }
    }
}
