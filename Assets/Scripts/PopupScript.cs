using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class PopupScript : MonoBehaviour
{
    public float lifetime = 0.3f;
    private TMPro.TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshPro>();
    }
    private void Start()
    {
        Tween.PositionY(transform, 1, lifetime, Ease.Linear, useUnscaledTime: true);
        Tween.Custom(1, 0, lifetime, (t) => text.alpha = t, useUnscaledTime: true);
        Destroy(gameObject, lifetime + 0.01f);
    }
    public void SetText(string message)
    {
        text.text = message;
    }
}
