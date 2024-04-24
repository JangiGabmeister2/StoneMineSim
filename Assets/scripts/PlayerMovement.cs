using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private bool _is2D = false;

    private bool _facingRight = false;
    private Vector3 _moveDirection = Vector3.zero;

    Rigidbody _rb => GetComponent<Rigidbody>();

    float horizontal => Input.GetAxisRaw("Horizontal");
    float vertical => Input.GetAxisRaw("Vertical");

    //changes player velocity according to movement direction.
    private void Update()
    {
        _rb.velocity = _moveDirection.normalized;
    }

    //creates a direction for the player according to the vertical and horizontal inputs.
    private void FixedUpdate()
    {
        if (_is2D)
        {
            _moveDirection = transform.right * horizontal + transform.up * vertical;
        }
        else
        {
            _moveDirection = transform.right * horizontal + transform.forward * vertical;
        }

        _moveDirection *= _moveSpeed * Time.deltaTime;

        if (_facingRight && horizontal < 0f || !_facingRight && horizontal > 0f)
        {
            Flip();
        }
    }

    //flips the transform of the player's sprite, as if it is turning around to move the opposite direction.
    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 scale = playerSprite.localScale;
        scale.x *= -1;
        playerSprite.localScale = scale;
    }
}