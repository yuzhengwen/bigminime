using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public Transform collectPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.AddCoin(coinValue);
            AudioHandler.Instance.PlayAudio("CoinGet", 1.3f);
            GetComponent<MovingObjectNoRigidbody>().enabled = false;
            Tween.Position(transform, collectPosition.position, 1f, Ease.InOutCubic).OnComplete(() => Destroy(gameObject));
        }
    }
}
