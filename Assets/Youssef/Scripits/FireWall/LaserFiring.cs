using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFiring : MonoBehaviour
{

    public GameObject laserPrefab;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.W))
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}
