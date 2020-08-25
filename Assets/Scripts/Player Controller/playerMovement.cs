using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player's current speed
    public float speed;
    // Values speed can be
    public float walkSpeed = 3f;
    public float runSpeed = 5f;

    Vector3 movementDirection;
    Vector3 target;

    public Rigidbody rb;

    public Camera playerCam;
    public PlayerAnimation anim;

    void Update()
    {
        // Value between -1 and 1 for each axis based on input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Vector using input directions (normalized)
        movementDirection = new Vector3(x, 0, y).normalized;

        Run();

        if (x == 0 && y == 0){
            anim.currentAnim = PlayerAnimation.Anim.Idle;
        }
        
        // Find where the mouse is on the screen
        target = Input.mousePosition;
    }

    void FixedUpdate()
    {
        // Move the player wherever the mouse target is
        rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);

        // Find the vector direction from the player to the target
        Vector3 direction = target - playerCam.WorldToScreenPoint(transform.position);
        // Find the angle at which the player needs to rotate to face the direction vector
        float turnAngle = -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        // Rotate the player
        transform.eulerAngles = new Vector3(0, turnAngle, 0);
        //Debug.Log(new Vector3(0, turnAngle, 0));
    }

    void Run()
    {
        // Detect if the player is running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.currentAnim = PlayerAnimation.Anim.Run;
            speed = runSpeed;
        }
        else
        {
            anim.currentAnim = PlayerAnimation.Anim.Walk;
            speed = walkSpeed;
        }
    }
}
