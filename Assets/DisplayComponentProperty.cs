using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;

public class DisplayComponentProperty : MonoBehaviour
{
    public MonoBehaviour component;
    private TextMeshProUGUI text;
    public string valueName;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = component.GetType().GetProperty(valueName).GetValue(component).ToString();
    }
}
