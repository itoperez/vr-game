using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform playerCamera;
    public float offset = 0f;
    
    void Update()
    {
        Vector3 reflectObjectHeight = transform.position;
        reflectObjectHeight.y = playerCamera.position.y + offset;
        transform.position = reflectObjectHeight;
    }
}
