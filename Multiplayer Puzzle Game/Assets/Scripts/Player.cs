using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [SerializeField] float _speed = 1;
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;

    Vector3 _startPosition;
    int _jumpsRemaining;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;
    private string _jumpButton;
    private string _horizontalAxis;
    private int _layerMask;

    public int PlayerNumber { get { return _playerNumber; } }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        _layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        //update is grounded
        var hit = Physics2D.OverlapCircle(_feet.position, 0.2f, _layerMask);
        _isGrounded = hit != null;

        //read horizontal input
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;

        //move horizontal
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);


        //change direction
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
        
        //jump
        if (Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
            _jumpsRemaining--;
        }

        if (_isGrounded)
        {
            _jumpsRemaining = _maxJumps;
        }

        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPosition;
    }

    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
