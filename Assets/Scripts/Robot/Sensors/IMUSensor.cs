using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class IMUSensor : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void updateIMUSensorData(float x,float y, float z, float angularX, float angularY, float angularZ,float positionX,float positionY,float positionZ, float orientationX, float orientationY, float orientationZ);

    private ArticulationBody ab; 

    private Vector3 lastVelocity = Vector3.zero;

    // wpk temp only for debug
    public Vector3 Accelerations;
    public Vector3 AngularVelocities;
    public Vector3 Position;
    public Vector3 Orientation;

    void Awake()
    {
        ab = transform.GetComponentInParent<ArticulationBody>();
    }
    void FixedUpdate()
    {
        Vector3 acceleration = (ab.velocity - lastVelocity) / Time.fixedDeltaTime;        
        Vector3 angularVelocity = ab.angularVelocity;
        Vector3 pos = this.transform.position;
        Vector3 orientation = this.transform.rotation.eulerAngles;

        // wpk temp only for debug
        Accelerations = acceleration;
        AngularVelocities = angularVelocity;
        Position = pos;
        Orientation = orientation;

        //debug.Instance.SetText("Acceleration x: " + (int)acceleration.x + "\n y: " + (int)acceleration.y + "\n z: " + (int)acceleration.z);
#if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            // the order of reporting the axes intentionally changed to adjust from Unity coordinate system to REV Gyro
            updateIMUSensorData(acceleration.z,acceleration.x,acceleration.y,angularVelocity.z,angularVelocity.x,angularVelocity.y, pos.z,pos.x,pos.y, 180.0f-orientation.z, (( orientation.x + 180.0f ) % 360.0f ) - 180.0f ,  180.0f-orientation.y );
        }
        catch {
            Debug.LogError("Error invoking updateIMUSensorData");
        }
#endif

        lastVelocity = ab.velocity;
    }
    
}
