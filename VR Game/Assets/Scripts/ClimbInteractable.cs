using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        if (args.interactorObject is XRDirectInteractor)
        {
            //Debug.Log("SELECT ENTER");
            if (!Climber.controllerVelocityHandOne)
            {
                Climber.controllerVelocityHandOne = args.interactorObject.transform.GetComponent<ControllerVelocity>();
            }
            else if (!Climber.controllerVelocityHandTwo)
            {
                Climber.controllerVelocityHandTwo = args.interactorObject.transform.GetComponent<ControllerVelocity>();
            }
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        if (args.interactorObject is XRDirectInteractor)
        {
            //Debug.Log("SELECT EXIT");
            if (Climber.controllerVelocityHandOne && Climber.controllerVelocityHandOne.name == args.interactorObject.transform.name)
            {                
                Climber.controllerVelocityHandOne = null;
            }
            if (Climber.controllerVelocityHandTwo && Climber.controllerVelocityHandTwo.name == args.interactorObject.transform.name)
            {
                Climber.controllerVelocityHandTwo = null;
            }
        }
    }
}
