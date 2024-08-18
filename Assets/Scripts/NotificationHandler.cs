using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzuValen.Utils;
using PrimeTween;

public class NotificationHandler : MonoBehaviourSingleton<NotificationHandler>
{
    [SerializeField] private TMPro.TextMeshProUGUI notificationText;

    private void Start()
    {
        notificationText.enabled = false;
    }
    public void ShowNotification(string text)
    {
        Reset();
        notificationText.enabled = true;
        Tween.Alpha(notificationText, 1, 0.6f, Ease.OutCirc);
        Tween.Custom(10, 36, 0.6f, (value) => notificationText.fontSize = (int)value);
        Tween.ShakeLocalPosition(notificationText.rectTransform, new Vector3(8, 20, 5), 0.6f, 10, true);
        notificationText.text = text;
        StartCoroutine(HideNotificationAfterDelay());
    }
    public IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Tween.Alpha(notificationText, 0, 0.6f, Ease.InCirc);
        Tween.Custom(36, 10, 0.6f, (value) => notificationText.fontSize = (int)value).OnComplete(() => notificationText.enabled = false);
    }

    private void Reset()
    {
        StopAllCoroutines();
        notificationText.alpha = 0;
        notificationText.fontSize = 10;
        notificationText.enabled = false;
    }
}
