using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10f;

    SpriteRenderer _spriteRenderer;
    Sprite _upSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidBody2D = player.GetComponent<Rigidbody2D>();
            if (rigidBody2D != null)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, _bounceVelocity);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}