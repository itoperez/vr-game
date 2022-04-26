using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private CharacterController characterController;
    private Transform characterTransform;
    private ActionBasedContinuousMoveProvider continuousMoveProvider;

    public Transform topOfWallTP;

    public static ControllerVelocity controllerVelocityHandOne = null;
    public static ControllerVelocity controllerVelocityHandTwo = null;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = GetComponent<Transform>();
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
    }
    private void FixedUpdate()
    {
        if (controllerVelocityHandOne || controllerVelocityHandTwo)
        {
            continuousMoveProvider.enabled = false;
            Climb();
        }
        else
        {
            continuousMoveProvider.enabled = true;
        }
    }  
    

    public void Climb()
    {
        // check if it exist, ex sockets do not have velocity
        Vector3 velocityHandOne = controllerVelocityHandOne ? controllerVelocityHandOne.Velocity : Vector3.zero;
        Vector3 velocityHandTwo = controllerVelocityHandTwo ? controllerVelocityHandTwo.Velocity : Vector3.zero;

        characterController.Move(transform.rotation * -velocityHandOne * Time.fixedDeltaTime);
        characterController.Move(transform.rotation * -velocityHandTwo * Time.fixedDeltaTime);
    }

    public void TPTopOfClimbingWall()
    {
        if (controllerVelocityHandOne)
        {
            controllerVelocityHandOne = null;
        }
        if (controllerVelocityHandTwo)
        {
            controllerVelocityHandTwo = null;
        }

        characterTransform.position = topOfWallTP.position;
    }
}
