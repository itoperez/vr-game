using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    public GameObject laser;
    public GameObject saberTip;
    private Vector3 fullsize;

    private AudioSource audioSource;
    public AudioClip lightsaberMovingSound;
    public AudioClip lightsaberHum;
    public AudioClip lightsaberOn;
    public AudioClip lightsaberOff;

    private Quaternion previousRotation;
    private Vector3 angularVelocity;


    private bool lightsaberIsOn = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1;

        fullsize = laser.transform.localScale;
        laser.SetActive(false);

        previousRotation = saberTip.transform.rotation;
    }

    private void Update()
    {
        LaserControl();

        if (lightsaberIsOn)
        {
            angularVelocity = (saberTip.transform.rotation.eulerAngles - previousRotation.eulerAngles) / Time.deltaTime;
        }
        else
        {
            angularVelocity = Vector3.zero;
        }                      

        if (lightsaberIsOn && ((angularVelocity.magnitude / 100) > 285f))
        {
            audioSource.PlayOneShot(lightsaberMovingSound);
        }
        else if (lightsaberIsOn && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(lightsaberHum);
        }

        previousRotation = saberTip.transform.rotation;
    }

    public void ToggleLightsaber()
    {
        lightsaberIsOn = !lightsaberIsOn;
        if (lightsaberIsOn)
        {
            audioSource.PlayOneShot(lightsaberOn);
        }
        else
        {
            audioSource.PlayOneShot(lightsaberOff);
        }
    }

    private void LaserControl()
    {
        if (lightsaberIsOn && laser.transform.localScale.y < fullsize.y)
        {
            laser.SetActive(true);
            laser.transform.localScale += new Vector3(0f, 0.0005f, 0f);

            if (laser.transform.localScale.x < fullsize.x || laser.transform.localScale.z < fullsize.z)
            {
                laser.transform.localScale += new Vector3(0.005f, 0f, 0.005f);
            }
        }
        else if (!lightsaberIsOn && (laser.transform.localScale.x >= 0 || laser.transform.localScale.z >= 0))
        {
            if (laser.transform.localScale.y > 0)
            {
                laser.transform.localScale -= new Vector3(0f, 0.00015f, 0f);
                laser.transform.localScale -= new Vector3(0.000015f, 0f, 0.000015f);
            }
            else
            {
                laser.SetActive(false);
                //laser.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

}
