using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Color flashColor = Color.red;   // The color to flash
    public float flashDuration = 0.1f;     // How long the flash lasts
    public float flashSpeed = 5f;          // Speed of the color transition

    private Color originalColor;           // The original color of the object
    private Renderer objectRenderer;       // The Renderer component of the object

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void Flash()
    {
        if (objectRenderer != null)
        {
            StopAllCoroutines(); // Stop any ongoing flash coroutines
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            float lerpFactor = Mathf.PingPong(Time.time * flashSpeed, 1f);
            objectRenderer.material.color = Color.Lerp(originalColor, flashColor, lerpFactor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectRenderer.material.color = originalColor; // Ensure color is reset to original
    }
}
