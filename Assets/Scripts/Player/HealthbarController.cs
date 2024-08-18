using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
    private List<GameObject> hearts = new();
    private int maxHealth, currentHealth;
    public float offset = 1.2f;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite halfHeart;

    public void SetMaxHealth(int maxHealth)
    {
        if (maxHealth % 2 != 0)
        {
            Debug.LogError("Max health must be an even number");
            return;
        }
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        for (int i = 0; i < maxHealth/2; i++)
        {
            GameObject heart = new GameObject("Heart_" + i);
            SpriteRenderer spriteRenderer = heart.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = fullHeart;

            heart.transform.position = new Vector2(transform.position.x + i * offset, transform.position.y);
            heart.transform.parent = transform;

            hearts.Add(heart);
        }
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        int fullHearts = currentHealth / 2;
        int halfHearts = currentHealth % 2;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < fullHearts)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else if (i < fullHearts + halfHearts)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = halfHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }
    }
}
