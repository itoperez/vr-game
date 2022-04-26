using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 30f;
    public GameObject bullet;
    public Transform muzzle;
    public AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1;
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * muzzle.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }
}
