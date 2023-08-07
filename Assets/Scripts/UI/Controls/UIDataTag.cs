using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIDataType
{
    Text,Number,Image
}
public class UIDataTag : MonoBehaviour
{
    public UIDataType type;
    public string tag;

}
