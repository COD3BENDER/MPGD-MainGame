using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] float jetpackForce = 3.5f;
    bool jump;
    bool jetpackjump;
    [SerializeField] LayerMask groundMask; // to make the player fall down slowly and not speed up the more the player is in the air
    bool isGrounded; // to make the player fall down slowly and not speed up the more the player is in the air


    Vector3 verticalVelocity = Vector3.zero;
    Vector2 horizontalInput;


    public float jetpackLevel = 1f;
    public float oxygenLevel = 1f;
    public float jetpackDepletionRate;
    public float jetpackRefillRate;
    public float oxygenDepletionRate;

    public Slider oxygenSlider;
    public Slider jetpackSlider;

    public Vector3 horizontalVelocity;
    public float horizontalFriction;
    public float horizontalAirFriction;



    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask); // make player fall at a constant rate and not speed up the more the player has been in the air
        if (isGrounded)
        {
            verticalVelocity.y = 0;
            jetpackLevel += jetpackRefillRate;
            if (oxygenLevel > 1f)
            {
                oxygenLevel = 1f;
            }
        }
        else
        {
            if (jetpackjump)
            {
                verticalVelocity.y += Mathf.Sqrt(-0.2f * jetpackForce * gravity);
                jetpackLevel -= jetpackDepletionRate;
            }
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


        Vector3 horizontalMovementVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        horizontalVelocity += horizontalMovementVelocity;
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxSpeed);
        //Vector3 slowdownHorizontalVelocity = new Vector3(1, 2, 3);
        
        //Scale(horizontalVelocity, slowdownHorizontalVelocity);
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (isGrounded)
        {
            horizontalVelocity *= horizontalFriction;
        }
        else
        {
            horizontalAirFriction *= horizontalFriction;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        oxygenLevel -= oxygenDepletionRate;
        if (oxygenLevel < 0f)
        {
            oxygenLevel = 0f;
        }
        oxygenSlider.value = oxygenLevel;
        jetpackSlider.value = jetpackLevel;
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

    public void OnJetpackJumpPressed()
    {
        jetpackjump = true;
    }

    public void OnJetpackJumpCanceled()
    {
        jetpackjump = false;
    }

}
