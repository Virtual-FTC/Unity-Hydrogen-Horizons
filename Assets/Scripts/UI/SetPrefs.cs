using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPrefs : MonoBehaviour
{
    public string prefName;

    private void Start()
    {
        Toggle t = GetComponent<Toggle>();
        if (t)
        {
            if (!PlayerPrefs.HasKey(prefName)) { setValue(t.isOn); return; }
            t.isOn = Convert.ToBoolean((PlayerPrefs.GetInt(prefName)));
            t.onValueChanged.Invoke(t.isOn);
        }
        Slider s = GetComponentInChildren<Slider>();
        if (s)
        {
            if (!PlayerPrefs.HasKey(prefName)) { setValue(s.value); return; }
            s.value = PlayerPrefs.GetFloat(prefName);
            s.onValueChanged.Invoke(s.value);
        }
        OptionList list = GetComponentInChildren<OptionList>();
        if(list)
        {
            list.SetOption((int)PlayerPrefs.GetFloat(prefName));
            list.onChangeOptionIndex.Invoke((int)PlayerPrefs.GetFloat(prefName));
        }
    }

    public void setValue(bool val)
    {
        PlayerPrefs.SetInt(prefName, val ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void setValue(float val)
    {
        PlayerPrefs.SetFloat(prefName, val);
        PlayerPrefs.Save();
    }

    public void setValue(int val)
    {
        PlayerPrefs.SetFloat(prefName, val);
        PlayerPrefs.Save();
    }
}