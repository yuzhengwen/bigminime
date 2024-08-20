using UnityEngine;

public class MovingObjectNoRigidbody : MonoBehaviour
{
    public float speed = 5;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
