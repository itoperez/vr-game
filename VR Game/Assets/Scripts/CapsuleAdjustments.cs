using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CapsuleAdjustments : MonoBehaviour
{
    private CharacterController characterController;
    private XROrigin rig;
    private float characterHeight;
    public float additionalHeadHeight = 0.2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
    }

    private void CapsuleFollowHeadset()
    {        
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        
        if ((characterController.collisionFlags & CollisionFlags.Above) == 0)
        {
            characterController.height = rig.CameraInOriginSpaceHeight + additionalHeadHeight;
            characterHeight = characterController.height / 2 + characterController.skinWidth;
        }        

        characterController.center = new Vector3(capsuleCenter.x, characterHeight, capsuleCenter.z);
        
    }
}
