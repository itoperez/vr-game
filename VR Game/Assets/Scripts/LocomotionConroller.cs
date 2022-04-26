using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionConroller : MonoBehaviour
{
    public enum UseRayOrDirectInteractor { Ray, Direct, Both};
    public UseRayOrDirectInteractor useRayOrDirectInteractor;
      
    public ActionBasedController leftHandRay;
    public ActionBasedController rightHandRay;
    public ActionBasedController leftHandDirect;
    public ActionBasedController rightHandDirect;
    public ActionBasedController leftTeleportRay;
    public ActionBasedController rightTeleportRay;

    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    public bool EnableLeftRay { get; set; } = true;
    public bool EnableRightRay { get; set; } = true;
    public bool EnableLeftDirect { get; set; } = true;
    public bool EnableRightDirect { get; set; } = true;
    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    Vector3 pos = new Vector3();
    Vector3 norm = new Vector3();
    int index = 0;
    bool validTarget = false;

    private void Start()
    {
        ControllerOptions();
    }

    private void Update()
    {       
        if (leftTeleportRay)
        {
            bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
        }

        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }

        if (useRayOrDirectInteractor == UseRayOrDirectInteractor.Both)
        {
            PreventDoubleGrabPerController();
        }
    }

    public bool CheckIfActivated(ActionBasedController controller)
    {
        if (controller.activateAction.action.IsPressed())
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void ControllerOptions()
    {
        if (useRayOrDirectInteractor == UseRayOrDirectInteractor.Ray)
        {
            leftHandRay.gameObject.SetActive(true);
            rightHandRay.gameObject.SetActive(true);
        }
        else if (useRayOrDirectInteractor == UseRayOrDirectInteractor.Direct)
        {
            leftHandDirect.gameObject.SetActive(true);
            rightHandDirect.gameObject.SetActive(true);
        }
        else if (useRayOrDirectInteractor == UseRayOrDirectInteractor.Both)
        {
            leftHandRay.gameObject.SetActive(true);
            rightHandRay.gameObject.SetActive(true);
            leftHandDirect.gameObject.SetActive(true);
            rightHandDirect.gameObject.SetActive(true);
        }
    }

    public void PreventDoubleGrabPerController()
    {        
        leftHandRay.gameObject.SetActive(EnableLeftRay);
        rightHandRay.gameObject.SetActive(EnableRightRay);
        leftHandDirect.gameObject.SetActive(EnableLeftDirect);
        rightHandDirect.gameObject.SetActive(EnableRightDirect);        
    }

}
