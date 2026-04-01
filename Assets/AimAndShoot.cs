using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    [Header("Aiming Settings")]
    public float currentAngle = 80f; 
    public float speed = 400f;
    
    // Limits: Start at 80, cycle down to -80
    private float minAngle = -80f;
    private float maxAngle = 80f;

    [Header("References")]
    public Rigidbody2D ballRb;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void Update()
    {
        // Clockwise rotation (Subtracting)
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            currentAngle -= 15f; 
            
            // If it goes past -80, reset to 80
            if (currentAngle < minAngle) currentAngle = maxAngle;
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            LaunchBall();
        }

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void LaunchBall()
    {
        if (ballRb != null)
        {
            // The math remains the same; it converts any Z rotation to a vector
            float adjustedRad = (currentAngle - 90f) * Mathf.Deg2Rad;
            
            float vx = speed * Mathf.Cos(adjustedRad);
            float vy = speed * Mathf.Sin(adjustedRad); 

            ballRb.linearVelocity = new Vector2(vx, vy);
        }
    }
}