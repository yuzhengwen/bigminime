using UnityEngine;

public class CosmicGale : MonoBehaviour
{
    private Player player;
    private bool playerInGaleZone = false;

    private Rigidbody2D rb;
    public float speed = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered gale zone");
            player = collision.GetComponent<Player>();
            playerInGaleZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited gale zone");
            playerInGaleZone = false;
        }
    }

    private void Update()
    {
        if (playerInGaleZone)
        {
            if (player.state == MorphState.Normal)
            {
                player.rb.AddForce(Vector2.up * 70);
            }
            else if (player.state == MorphState.Small)
            {
                player.rb.AddForce(Vector2.up * 150);
            }
        }
    }
}
