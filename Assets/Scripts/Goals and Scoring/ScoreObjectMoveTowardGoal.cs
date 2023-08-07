using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjectMoveTowardGoal : MonoBehaviour,IGrabEvent
{
    public float attractionRange = 1;
    public float maxMovement = 1;

    public void OnGrab(GameObject grabbingObject)
    {
        //not used
    }

    public void OnRelease(GameObject releasingObject)
    {
        Vector3 sphereCastPos = transform.position;
        sphereCastPos.y = 0;
        Collider[] hits = Physics.OverlapSphere(sphereCastPos, attractionRange);
        JunctionCapper capper = null;
        foreach (Collider hit in hits)
        {
            capper = hit.gameObject.GetComponentInChildren<JunctionCapper>();
            
            if(capper != null) { Debug.Log("moving to object: " + hit.transform.name); break; }
        }
        if (capper == null) { return; }
        Vector3 newPos = Vector3.MoveTowards(transform.position, capper.transform.position, maxMovement);
        newPos.y = transform.position.y;
        transform.position = newPos;

    }

}
