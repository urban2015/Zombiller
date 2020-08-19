using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Player's current speed
    public float speed;
    // Values speed can be
    public float walkSpeed = 3f;
    public float runSpeed = 5f;

    Vector2 movementDirection;
    Vector2 target;

    public Rigidbody2D rb;

    public Camera playerCam;

    void Update()
    {
        // Value between -1 and 1 for each axis based on input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Vector using input directions (normalized)
        movementDirection = new Vector2(x, y).normalized;

        Run();

        // Find where the mouse is on the screen
        target = playerCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Move the player wherever the mouse target is
        rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);

        // Find the vector direction from the player to the target
        Vector2 direction = target - rb.position;
        // Find the angle at which the player needs to rotate to face the direction vector
        float turnAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        // Rotate the player
        rb.rotation = turnAngle;
    }

    void Run()
    {
        // Detect if the player is running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
