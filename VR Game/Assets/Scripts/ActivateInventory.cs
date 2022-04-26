using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    XRSocketInteractorBelt beltInteractor;
    MeshRenderer meshRenderer;

    private void Start()
    {
        beltInteractor = GetComponent<XRSocketInteractorBelt>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "GameController" && !beltInteractor.isHoldingObject)
        {            
            beltInteractor.socketActive = true;
            meshRenderer.enabled = true;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController" && !beltInteractor.isHoldingObject)
        {
            beltInteractor.socketActive = false;
            meshRenderer.enabled = false;
        }
    }
}
