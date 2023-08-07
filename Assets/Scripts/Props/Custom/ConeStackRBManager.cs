using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Turns off individual cone RBs until the stack is knocked over
public class ConeStackRBManager : MonoBehaviour
{
    [SerializeField] GameObject physicalCones;
    [SerializeField] GameObject dummyCones;

    List<GameObject> physicalConeObjects = new List<GameObject>();
    public GameObject PhysicalCones { get => physicalCones; }

    // Start is called before the first frame update
    void Start()
    {
        PhysicalCones.SetActive(false);

        for(int i = 0; i < physicalCones.transform.childCount; i++)
        {
            physicalConeObjects.Add(physicalCones.transform.GetChild(i).gameObject);
        }
    }

    public void SwitchOnPhysicalCones()
    {
        PhysicalCones.SetActive(true);

        foreach(GameObject obj in physicalConeObjects)
        {
            obj.transform.SetParent(transform.parent);
        }

        dummyCones.SetActive(false);
    }    

    // Update is called once per frame
    void Update()
    {
        
    }

}
