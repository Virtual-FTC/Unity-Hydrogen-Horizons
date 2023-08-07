using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOccupancyChecker : MonoBehaviour
{
    public bool IsEmptyNoCone { get; private set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Cone>() != null)
        {
            IsEmptyNoCone = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Cone>() != null)
            IsEmptyNoCone = true;
    }
}
