using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaterialSwitch : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material[] materials;
    int count = 0;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void NextMaterial()
    {
        if (count == (materials.Length - 1))
        {
            count = 0;
        } else
        {
            count++;
        }      
        meshRenderer.material = materials[count];        
    }

    public void PreviousMaterial()
    {
        if (count == 0)
        {
            count = (materials.Length - 1);
        }
        else
        {
            count--;
        }
        meshRenderer.material = materials[count];
    }
}
