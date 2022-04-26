using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class HandPresence : MonoBehaviour
{
    public enum ShowControllerAndOrHand { Hand, Controller, ControllerAndHand };
    public ShowControllerAndOrHand showContollerAndOrHand;

    public InputDeviceCharacteristics controllerCharacteristics;
    private List<InputDevice> devices;
    
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;    

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;

    private Animator handAnimator;

    private void Start()
    {
        TryInitialize();
    }   
    
    private void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        } else
        {
            if (showContollerAndOrHand == ShowControllerAndOrHand.ControllerAndHand)
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(true);
                UpdateHandAnimation();
            }
            else if (showContollerAndOrHand == ShowControllerAndOrHand.Controller)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else if (showContollerAndOrHand == ShowControllerAndOrHand.Hand)
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }            

        
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            //Debug.Log("Pressing Secondary button");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /*          
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Trigger pressed " + triggerValue);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }
        */
    }

    private void TryInitialize()
    {
        devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            //Debug.Log(item.name + item.characteristics);

        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }        

    } // Try to Initialize Controllers

    private void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

    } // Update Hand Animation

}
