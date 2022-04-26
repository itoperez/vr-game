using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTheSide : MonoBehaviour
{
    public Transform mainCamera;
    public CharacterController characterController;
    public Vector3 offset;

    private float belowMidBeltOffset = 1f;

    private void Update()
    {
        float characterColumnHeight = characterController.height - characterController.skinWidth - (characterController.radius * 2);

        if (transform.name != "Back Belt Inventory")
        {
            offset.y = -(characterColumnHeight / 2) * belowMidBeltOffset;
        }        
    }


    private void FixedUpdate()
    {
        transform.position = mainCamera.position + Vector3.up * offset.y 
            + Vector3.ProjectOnPlane(mainCamera.right, Vector3.up).normalized * offset.x 
            + Vector3.ProjectOnPlane(mainCamera.forward, Vector3.up).normalized * offset.z;

        transform.eulerAngles = new Vector3(0, mainCamera.eulerAngles.y, 0);    
    }
}
