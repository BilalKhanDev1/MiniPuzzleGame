using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;
    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onCenter;

    [SerializeField] Sprite _left;
    [SerializeField] Sprite _right;
    [SerializeField] Sprite _center;

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    enum ToggleDirection { Left, Center, Right}

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        var playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
            return;

        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = player.GetComponent<Rigidbody2D>().velocity.x > 0;
        bool playerWalkingLeft = player.GetComponent<Rigidbody2D>().velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
          SetToggleDirection(ToggleDirection.Right);
        else if (wasOnRight == false && playerWalkingLeft)
          SetToggleDirection(ToggleDirection.Left);
    }

    void SetToggleDirection(ToggleDirection direction,bool force = false)
    {
        if (force == false && _currentDirection == direction)
            return;
        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _left;
                _onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _center;
                _onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _right;
                _onRight.Invoke();
                break;
            default:
                break;
        }
    }
}
