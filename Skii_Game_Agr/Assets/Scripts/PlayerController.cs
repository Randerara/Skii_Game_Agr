using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = -10;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabled;
    [SerializeField] private float disableTime = 0.7f;
    private float lastDisableTime;
    private Rigidbody rb;
    
    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.Linecast(transform.position, transform.position - transform.up, groundLayer);

        if (Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;
        
        if (isGrounded && !disabled)
        {
            Vector2 moveVector = move.ReadValue<Vector2>();
                    
            float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
            float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
                    
            transform.Rotate(0, moveVector.x * rotationSpeed * Time.fixedDeltaTime, 0);
            rb.AddForce(transform.forward * moveSpeed * Time.fixedDeltaTime * speedMultiplier);
        }
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += TakeDamage;
    }

    void TakeDamage()
    {
        disabled = true;
        lastDisableTime = Time.timeSinceLevelLoad;
        rb.AddForce(pushbackForce);
        Debug.Log("Player got hit");
    }
}
