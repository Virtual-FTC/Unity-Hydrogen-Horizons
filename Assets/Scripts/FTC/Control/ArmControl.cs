using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Control;

public class ArmControl : MonoBehaviour
{
    public CommandProcessor Commands = new CommandProcessor();

    public Transform Hinge;
    public Transform slide2;
    public Transform slide3;

    public float ArmSpeed = 0.3f;
    private float travel = 0.3f;

    public float RotateSpeed = 30f;
    public float RotMax = 350f;
    public float RotMin = 270f;
   
    private float armY = 0;
    private float armZ = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (!slide2) { slide2 = transform.Find("Drawer Slide:2"); }
        if (!slide3) { slide3 = transform.Find("Drawer Slide:3"); }
        armZ = Hinge.localEulerAngles.z;
    }

    bool extend = false;
    bool decend = false;
    bool rotateForward = false;
    bool rotateBackward = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        //extend arm
        Vector3 p2 = slide2.localPosition;
        Vector3 p3 = slide2.localPosition;
        if (extend)
        {
            if (p2.y > -travel)
            {
                float newY = p2.y -= ArmSpeed * Time.deltaTime;
            }
            else
            {
                if (p3.y > (-travel * 2f))
                {
                    float newY = p3.y -= ArmSpeed * Time.deltaTime;
                }
            }
            slide2.localPosition = p2;
            slide3.localPosition = p3;
        }
        if (decend)
        {
            if (p3.y < p2.y)
            {
                float newY = p3.y += ArmSpeed * Time.deltaTime;
            }
            else
            {
                if (p2.y <= 0)
                {
                    float newY = p2.y += ArmSpeed * Time.deltaTime;
                }
            }
            slide2.localPosition = p2;
            slide3.localPosition = p3;
        }
        extend = false;
        decend = false;

        //rotate arm
        if (rotateForward && Hinge.localEulerAngles.z < RotMax)
        {
            Vector3 p = Hinge.localEulerAngles;
            p.z += RotateSpeed * Time.deltaTime;
            Hinge.localEulerAngles = p;
        }
        if (rotateBackward && Hinge.localEulerAngles.z > RotMin)
        {
            Vector3 p = Hinge.localEulerAngles;
            p.z -= RotateSpeed * Time.deltaTime;
            Hinge.localEulerAngles = p;
        }
         rotateForward = false;
         rotateBackward = false;
    }

    public void ExtendArm()
    {
        extend = true;
    }
    public void DecendArm()
    {
        decend = true;
    }

    public void RotateArmForward()
    {
        rotateForward = true;
    }
    public void RotateArmBackward()
    {
        rotateBackward = true;
    }
}
