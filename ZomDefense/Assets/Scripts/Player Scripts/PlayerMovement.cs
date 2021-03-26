using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // reference to character controller

    public float speed = 12f; // how fast the player moves
    public float gravity = -29.43f; // force of gravity applied
    public float jumpHeight = 3f; // how high the player can jump

    public Transform groundCheck; // transform of object checking if the player collides with the ground
    public float groundDistance = 0.4f; // radius of sphere used to check if the player is colliding with the ground
    public LayerMask groundMask; // controls what objects the sphere checks for

    Vector3 velocity; // how fast we fall
    bool isGrounded; // are we on the ground or not

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // creates tiny sphere physics check and if it collides with groundmask, isGrounded is true, else false

        if(isGrounded && velocity.y < 0) // is the player on the ground and not falling
        {
            velocity.y = -2f; // stops player from not properly colliding with the ground by setting to -2f instead of 0f
        }

        float x = Input.GetAxis("Horizontal"); // get horizontal input axis
        float z = Input.GetAxis("Vertical"); // get vertical input axis

        Vector3 move = transform.right * x + transform.forward * z; // arrow that points in direction we want to move, transform allows us to move based on local position instead of absolute

        controller.Move(move * speed * Time.deltaTime); // Move is function of character controller, moves the player. speed allows control of player speed and Time.deltaTime makes movement framerate independant

        if(Input.GetButtonDown("Jump") && isGrounded) // checks if the player is on the ground and if "Jump" (default to space) is pressed
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // use physics formula v = sqrt(height * -2 * gravity) to jump
        }

        velocity.y += gravity * Time.deltaTime; // velocity increased determined by gravity effect over time

        controller.Move(velocity * Time.deltaTime); // add velocity to player (physics of free fall require time squared, which is why velocity is multiplied by Time.deltaTime again
    }
}
