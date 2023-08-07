using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bind : System.Object
{
    [SerializeField]
    public string bind { get; set; }
    public string name { get; set; }
    public string BINDASSHOLE { get { return bind; } }
}
