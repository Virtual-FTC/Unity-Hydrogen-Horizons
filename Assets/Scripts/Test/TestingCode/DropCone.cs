using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DropCone : MonoBehaviour
{

    public GameObject BlueConesParent;
    public GameObject RedConesParent;

    List<GameObject> redCones;
    List<GameObject> blueCones;

    public TeamColor color = TeamColor.Blue;
    public float HeightOffset;
    public float XOffset;
    public float ZOffset;

    // Start is called before the first frame update
    void Start()
    {
        blueCones = new List<GameObject>();
        redCones = new List<GameObject>();
        foreach (Cone cone in BlueConesParent.GetComponentsInChildren<Cone>(true))
        {
            if(cone.GetComponent<Rigidbody>() == null) { continue; }//skip dummy cones
            blueCones.Add(cone.gameObject);
        }
        foreach (Cone cone in RedConesParent.GetComponentsInChildren<Cone>(true))
        {
            if (cone.GetComponent<Rigidbody>() == null) { continue; }//skip dummy cones
            redCones.Add(cone.gameObject);
        }


    }
#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            Drop(Selection.activeGameObject);
        }

    }
#endif
    public void Drop(GameObject dropTarget)
    {
        Vector3 newPos = dropTarget.transform.position;

        GameObject newCone;

        if (color == TeamColor.Blue)
        {
            newCone = blueCones[0];
            blueCones.RemoveAt(0);
        }
        else if (color == TeamColor.Red)
        {
            newCone = redCones[0];
            redCones.RemoveAt(0);
        }
        else
        {
            newCone = redCones[0];
            redCones.RemoveAt(0);
        }
        newCone.GetComponent<Cone>().MakeScorable();

        newPos.y += HeightOffset;
        newPos.x += XOffset;
        newPos.z += ZOffset;

        newCone.transform.position = newPos;
        newCone.transform.parent = null;//physical cone parent is going to be disabled.
    }

}