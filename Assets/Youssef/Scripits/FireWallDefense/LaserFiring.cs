using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFiring : MonoBehaviour
{

    public GameObject laserPrefab;

    public AudioSource laserSFX;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);

            laserSFX.Play();
        }

    }
}
