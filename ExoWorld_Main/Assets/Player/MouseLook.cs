using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
# pragma warning disable 649 // if we return to editor some errors will go away
    //Horizontal camera look
    [SerializeField] float sensitivityX = 20f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;

    //vertical camera look
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f; // clamp used so rotation does not go too high or too low
    float xRotation = 0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;

        playerCamera.eulerAngles = targetRotation;

    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;

    }

}
