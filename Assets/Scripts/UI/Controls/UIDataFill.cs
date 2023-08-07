using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using TMPro;

public class UIDataFill : MonoBehaviour
{
    public void Fill(System.Object dataObject, GameObject objToFill)
    {
        I_UIDataFill[] fillers = objToFill.GetComponentsInChildren<I_UIDataFill>();
        //some kind of interface that will notify when data is changed.
        Dictionary<string,PropertyInfo> props= dataObject.GetType().GetProperties().ToDictionary(p=> p.Name);
        foreach (I_UIDataFill fill in fillers)
        {
            if (props.ContainsKey(fill.Tag)) 
            {
                fill.FillData(props[fill.Tag].GetValue(dataObject));
            }
        }
    }

    public void Fill(Dictionary<string,string> dataObject, GameObject objToFill)
    {
        UIDataTag[] tags = objToFill.GetComponentsInChildren<UIDataTag>();
        //some kind of interface that will notify when data is changed.
        foreach (UIDataTag tag in tags)
        {
            if (dataObject.ContainsKey(tag.tag))
            {
                TextMeshProUGUI text = tag.gameObject.GetComponent<TextMeshProUGUI>();
                text.text = dataObject[tag.tag];
            }
        }
        I_UIDataFill[] fillers = objToFill.GetComponentsInChildren<I_UIDataFill>();
        //some kind of interface that will notify when data is changed.
        foreach (I_UIDataFill fill in fillers)
        {
            if (dataObject.ContainsKey(fill.Tag))
            {
                fill.FillData(dataObject[fill.Tag]);
            }
        }
    }
}
