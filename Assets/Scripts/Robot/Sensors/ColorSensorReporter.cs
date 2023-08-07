using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ColorSensorReporter : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void updateColorSensorData(float r, float g, float b, float distance, string direction);
    private ColorSensor sensor;

    public string directionID;

    private void Start()
    {
        sensor = GetComponent<ColorSensor>();
    }

    private void FixedUpdate()
    {
        //DebugUI.instance?.Display($"color sensed: " + sensor.colorSensed.r + " : " + sensor.colorSensed.g + " : " + sensor.colorSensed.b + " : " + sensor.colorSensingRayLength);
        #if UNITY_WEBGL && !UNITY_EDITOR
        //Debug.Log($"color sensed: " + sensor.colorSensed.r + " : " + sensor.colorSensed.g + " : " + sensor.colorSensed.b + " : " + sensor.colorSensingRayLength);
        try
        {
            updateColorSensorData(sensor.colorSensed.r, sensor.colorSensed.g, sensor.colorSensed.b, sensor.colorSensingRayLength, directionID);
        }
        catch { }
#endif
    }
}
