using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NotificationHandler.Instance.ShowNotification("WARNING: Cosmic Gale ahead!");
        }
    }
}
