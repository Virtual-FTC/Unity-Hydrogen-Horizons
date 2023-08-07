using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugDriverAmounts : MonoBehaviour
{
    public Drive[] drivers;
    private TextMeshProUGUI textBox;
    private string permText;
    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        string text = permText + "\n";
        foreach(Drive drive in drivers)
        {
            text += drive.name + "x:" + drive.driveAmount.x + " y:" + drive.driveAmount.y + " z:" + drive.driveAmount.z + "\n";
        }
        textBox.text = text;
    }
    public void SetPermText(string text)
    {
        permText = text;
    }
}
