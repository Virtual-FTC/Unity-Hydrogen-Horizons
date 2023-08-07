using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowGamepadIcon : MonoBehaviour, I_UIDataFill
{
    public string Tag { get { return datatag; } set { datatag = value; } }
    public UIDataType Type { get { return dtype; } set { dtype = value; } }

    public string datatag;
    public UIDataType dtype;

    TextMeshProUGUI text;

    public void FillData(object data)
    {
        string iconKey = GamepadUI.GetGamepadIcon(data.ToString());
        text.text = iconKey;
    }

    void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}
