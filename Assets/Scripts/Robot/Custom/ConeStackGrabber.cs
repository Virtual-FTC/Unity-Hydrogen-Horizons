using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeStackGrabber : MonoBehaviour
{
    [SerializeField] Drive grabberDrive;
    bool isOnConeStack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ConeStackRBManager>() != null)
        {
            isOnConeStack = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<ConeStackRBManager>() != null)
        {
            if (grabberDrive.driveAmount.magnitude > .01f && isOnConeStack)
            {
                other.GetComponentInParent<ConeDispenser>().DispenseCone();
                GameObject newCone = 
                    Instantiate(other.GetComponentInParent<ConeStackRBManager>().PhysicalCones.transform.GetChild(0).gameObject);
                newCone.transform.position = this.transform.position;
                newCone.GetComponent<Cone>().MakeScorable();

                GetComponent<ObjectChecker>().CanPickUp = true;
                GetComponent<ObjectChecker>().ObjectInTrigger = newCone;
                GetComponent<ObjectGrabber>().PickUpOrPutDownObject();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ConeStackRBManager>() != null)
        {
            isOnConeStack = false;
        }
    }
}
