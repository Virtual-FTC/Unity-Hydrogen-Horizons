using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOrientation : MonoBehaviour
{
    public bool IsRightSideUp { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(transform.up, Vector3.up) < 90)
            IsRightSideUp = true;
        else
            IsRightSideUp = false;
    }
}
