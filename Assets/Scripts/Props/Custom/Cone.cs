using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [SerializeField] GameObject coneMeshObject;

    public GameObject ConeMeshObject { get => coneMeshObject; }

    [SerializeField] GameObject junctionSliderObject;
    public GameObject JunctionSliderObject { get => junctionSliderObject; }

    [SerializeField] GameObject coneBaseForStacking;
    public GameObject ConeBaseForStacking { get => coneBaseForStacking; }

    public GameObject ConeCollider;

    public void MakeScorable(bool scorable = true)
    {
        GetComponent<Cone>().ConeBaseForStacking.GetComponentInChildren<Collider>().enabled = !scorable;//turn off stack collider.
        GetComponent<Rigidbody>().isKinematic = !scorable;
        ConeCollider.GetComponent<Collider>().enabled = scorable;
    }

}
