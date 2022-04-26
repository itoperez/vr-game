using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public XRSimpleInteractable guardHandGrabPoint;

    private IXRSelectInteractor gripInteractor;
    private IXRSelectInteractor guardInteractor;
    private Quaternion initialAttachRotation;

    public ActionBasedController[] controllers;

    public enum TwoHandRotationType { None, Grip, Guard };
    public TwoHandRotationType twoHandRotationType;

    public bool snapToGuardHand = true;
    private Quaternion initialRotationOffset;

    private void Start()
    {
        guardHandGrabPoint.selectEntered.AddListener(OnSecondHandGrab);
        guardHandGrabPoint.selectExited.AddListener(OnSecondHandRelease);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if ((guardInteractor != null) && (gripInteractor != null))
        {
            if (snapToGuardHand)
            {
                gripInteractor.transform.rotation = GetTwoHandRotation();
            }
            else
            {
                gripInteractor.transform.rotation = GetTwoHandRotation() * initialRotationOffset;
            }
            
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        Vector3 lookDirection = guardInteractor.transform.position - gripInteractor.transform.position;

        if (lookDirection.magnitude > 0)
        {
            if (twoHandRotationType == TwoHandRotationType.None)
            {
                targetRotation = Quaternion.LookRotation(lookDirection);
            }
            else if (twoHandRotationType == TwoHandRotationType.Grip)
            {
                targetRotation = Quaternion.LookRotation(lookDirection, gripInteractor.transform.up);
            }
            else // if (twoHandRotationType == TwoHandRotationType.Guard)
            {
                targetRotation = Quaternion.LookRotation(lookDirection, guardInteractor.transform.up);
            }
        } else
        {
            targetRotation = Quaternion.identity;
        }
        
        return targetRotation;
    }

    private void OnSecondHandGrab(SelectEnterEventArgs arg0)
    {
        //Debug.Log("SECOND HAND GRAB");
        guardInteractor = arg0.interactorObject;
        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * gripInteractor.transform.rotation;
    }

    private void OnSecondHandRelease(SelectExitEventArgs arg0)
    {
        //Debug.Log("SECOND HAND RELEASE");
        guardInteractor = null;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //Debug.Log("FIRST HAND GRAB");
        base.OnSelectEntered(args);
        gripInteractor = firstInteractorSelecting;
        //initialAttachRotation = GetAttachTransform(args.interactorObject).localRotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        //Debug.Log("FIRST HAND RELEASE");
        base.OnSelectExited(args);
        gripInteractor = null;
        guardInteractor = null;
        //GetAttachTransform(args.interactorObject).localRotation = initialAttachRotation;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {       
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting) && CheckIfInteractorIsController(firstInteractorSelecting);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }

    
    public bool CheckIfInteractorIsController(IXRSelectInteractor interactor)
    {
        foreach (var controller in controllers)
        {
            if (controller.transform.name == interactor.transform.name)
            {
                return true;
            }
        }        
        return false;
    }
    
}
