using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDispenser : MonoBehaviour
{
    [SerializeField] GameObject dummyCones;

    GameObject[] cones;

    int nextConeNumber = 0;

    public GameObject DummyCones { get => dummyCones; }

    // Start is called before the first frame update
    void Start()
    {
        cones = new GameObject[5];

        for(int i = 0; i < DummyCones.transform.childCount; i++)
        {
            cones[i] = DummyCones.transform.GetChild(i).gameObject;
        }
        
    }

    internal void DispenseCone()
    {
        cones[nextConeNumber].SetActive(false);
        nextConeNumber++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
