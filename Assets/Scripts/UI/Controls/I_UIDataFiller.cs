using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UIDataFill
{
    public void FillData(System.Object data);
    public string Tag { get; set; }
    public UIDataType Type { get; set; }
}
