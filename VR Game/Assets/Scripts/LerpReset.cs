using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpReset : MonoBehaviour
{
    private Vector3 spawnPosition;
    private bool objectAtSpawnPosition;

    private Quaternion spawnRotation;
    private bool objectAtSpawnRotation;

    private float flyDuration = 3f;
    private float lerpDuration = 1.5f;

    private Rigidbody rb;

    void Start()
    {
        spawnPosition = transform.position;
        objectAtSpawnPosition = true;

        spawnRotation = transform.rotation;
        objectAtSpawnRotation = true;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((transform.position != spawnPosition && objectAtSpawnPosition) || (transform.rotation != spawnRotation && objectAtSpawnRotation))
        {
            objectAtSpawnPosition = false;
            objectAtSpawnRotation = false;
            StartCoroutine(resetObject());
        }
    }

    IEnumerator resetObject()
    {
        yield return new WaitForSeconds(flyDuration);        

        Vector3 pushedBackPosition = transform.position;
        Vector3 newTransformPosition = pushedBackPosition;

        Quaternion pushedBackRotation = transform.rotation;
        Quaternion newTransformRotation = pushedBackRotation;

        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            newTransformPosition.x = Mathf.Lerp(pushedBackPosition.x, spawnPosition.x, timeElapsed / lerpDuration);
            newTransformPosition.y = Mathf.Lerp(pushedBackPosition.y, spawnPosition.y, timeElapsed / lerpDuration);
            newTransformPosition.z = Mathf.Lerp(pushedBackPosition.z, spawnPosition.z, timeElapsed / lerpDuration);

            transform.position = newTransformPosition;

            newTransformRotation.x = Mathf.Lerp(pushedBackRotation.x, spawnRotation.x, timeElapsed / lerpDuration);
            newTransformRotation.y = Mathf.Lerp(pushedBackRotation.y, spawnRotation.y, timeElapsed / lerpDuration);
            newTransformRotation.z = Mathf.Lerp(pushedBackRotation.z, spawnRotation.z, timeElapsed / lerpDuration);

            transform.rotation = newTransformRotation;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        rb.isKinematic = true;
        rb.isKinematic = false;
        objectAtSpawnPosition = true;
        objectAtSpawnRotation = true;
    }
}
