using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public GameObject popup;
    private bool shown = false;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        popup.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !shown)
        {
            Time.timeScale = 0;
            shown = true;
            popup.SetActive(true);
            player.DisablePlay();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shown)
        {
            ClosePopup();
        }
    }
    public void ClosePopup()
    {
        popup.SetActive(false);
        Time.timeScale = 1;
        player.EnablePlay();
    }
}
