using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollisionOnRelease : MonoBehaviour,IGrabEvent
{
    public float reEnableDelay = 1;
    private List<Collider> colliders = new List<Collider>();

    public void OnGrab(GameObject grabbingObject)
    {
    }

    public void OnRelease(GameObject releasingObject)
    {
        enableColliders(false);
        Invoke("ReEnable", reEnableDelay);
    }

    void ReEnable()
    {
        enableColliders(true);
    }

    void enableColliders(bool setActive)
    {
        foreach(Collider collider in colliders)
        {
            if (collider.isTrigger) { continue; }
            collider.enabled = setActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        colliders.AddRange(GetComponentsInChildren<Collider>());  
    }
}
