using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] LayerMask groundMask; // to make the player fall down slowly and not speed up the more the player is in the air
    bool isGrounded; // to make the player fall down slowly and not speed up the more the player is in the air


    Vector3 verticalVelocity = Vector3.zero;
    Vector2 horizontalInput;


    

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask); // make player fall at a constant rate and not speed up the more the player has been in the air
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }
        // jump function
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }


        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
        print(horizontalInput);
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
   
}
