using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopControl : MonoBehaviour
{
    public CommandProcessor Commands = new CommandProcessor();

    [Header("Scoop Motor")]
    public float wantedVelocity = 150f;

    private float motorCmd = 0;

    Transform hinge;

    private void Awake()
    {
        hinge = GetComponent<Transform>();
    }

    void updateVelocity()
    {
        //var motor = hinge.motor;
        float z = wantedVelocity * motorCmd;
        hinge.localEulerAngles = new Vector3(hinge.localEulerAngles.x, hinge.localEulerAngles.y, hinge.localEulerAngles.z + z);

        //hinge.motor = motor;
    }

    public void setCmd(float cmd)
    {
        motorCmd = cmd;
    }

    public float getCmd()
    {
        return motorCmd;
    }
}
