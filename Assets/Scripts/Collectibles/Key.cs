using UnityEngine;
using PrimeTween;

public class Key : MonoBehaviour
{
    public Transform collectPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.AddKey();
            AudioHandler.Instance.PlayAudio("KeyGet", 1);
            GetComponent<MovingObjectNoRigidbody>().enabled = false;
            Tween.Position(transform, collectPosition.position, 1f, Ease.InOutCubic).OnComplete(() => Destroy(gameObject));
        }
    }
}