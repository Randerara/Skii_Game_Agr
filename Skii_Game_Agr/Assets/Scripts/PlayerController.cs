using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = -10;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = move.ReadValue<Vector2>();
        
        float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
        float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        
        transform.Rotate(0, moveVector.x * rotationSpeed * Time.fixedDeltaTime, 0);
        rb.AddForce(transform.forward * moveSpeed * Time.fixedDeltaTime * speedMultiplier);
       
        
    }
}
