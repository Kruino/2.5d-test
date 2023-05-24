using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool rotationActive; 
    public float rotationSpeed = 500f;
    public float movementThreshold = 0.1f;
    public float jumpForce = 5f;

    private Rigidbody _rb;
    private bool _isJumping;

    public PlayerMovement()
    {
        _isJumping = false;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;

        if (movement.magnitude > movementThreshold && rotationActive)
        {
            var targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.Translate(movement * Time.deltaTime, Space.World);

        if (!Input.GetButtonDown("Jump") || _isJumping) return;
        
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        _isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}
