using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetToggleOption : MonoBehaviour
{
    public int itemIndex = 0;
    public UnityEvent<int> onSelected;

    public void onToggle(bool isOn)
    {
        if (isOn) { onSelected.Invoke(itemIndex); }
    }
}
