using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rotation : MonoBehaviour
{ 
    //varriables for aim movement
    private Camera mCamera;
    private Vector3 mousePosition;

    // varribales for firing
    public GameObject bullet;
    public Transform crossHair;
    public bool isFiring;
    public float timmer;
    public float firingTime;




    void Start()
    {
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length > 0)
        {
            mCamera = mainCameras[0].GetComponent<Camera>();
        }
    }

    private void Update()
    {
        mousePosition = mCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //if the isfriing is false, then time follows up, so there will be a delay between every shot
        if(!isFiring )
        {
            timmer += Time.deltaTime;
            if(timmer > firingTime)
            {
                isFiring = true;
                timmer = 0;
            }
        }
        ///left clicking to spawn a bullet, at the possiton of the cross hair
        if (Input.GetMouseButtonDown(0) && isFiring)
        {
            isFiring = false;
            Instantiate(bullet, crossHair.position, Quaternion.identity);

        }


    }
}
