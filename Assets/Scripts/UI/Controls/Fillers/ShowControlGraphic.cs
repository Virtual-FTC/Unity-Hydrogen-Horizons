using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowControlGraphic : MonoBehaviour,I_UIDataFill
{
    public string Tag { get { return datatag; } set { datatag = value; } }
    public UIDataType Type { get { return dtype; } set { dtype = value; } }

    public string datatag;
    public UIDataType dtype;

    public List<Sprite> images;
    Image img;
    public GameObject keyIcon;
    public GameObject keyText;

    public void FillData(object data)
    {
        if(data.ToString().ToLower() == "gamepad")
        {
            img.sprite = images[0];
            keyIcon.SetActive(true);
            keyText.SetActive(false);
        }
        else
        {
            img.sprite = images[1];
            keyIcon.SetActive(false);
            keyText.SetActive(true);
        }
    }

    void OnEnable()
    {
        img = GetComponent<Image>();
    }
}
