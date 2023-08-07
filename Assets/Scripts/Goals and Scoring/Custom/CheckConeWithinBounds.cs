using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConeWithinBounds : MonoBehaviour, ICustomGoalChecker
{
    GoalZoneScoreLink goalZoneScoreLink;
    [SerializeField] GameObject goalBoundsObject;
    CapsuleCollider goalBounds;

    // Start is called before the first frame update
    void Start()
    {
        goalZoneScoreLink = GetComponent<GoalZoneScoreLink>();
        goalBounds = goalBoundsObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoCustomCheck()
    {

    }

    bool CheckConeBounds(GameObject objectToCheck)
    {

        MeshCollider objectToCheckMeshCollider = objectToCheck.GetComponentInParent<Cone>().ConeMeshObject.GetComponent<MeshCollider>();

        List<Vector3> pointsToCheck = new List<Vector3>();
        pointsToCheck.Add(objectToCheckMeshCollider.bounds.center + objectToCheckMeshCollider.bounds.extents);
        pointsToCheck.Add(objectToCheckMeshCollider.bounds.center - objectToCheckMeshCollider.bounds.extents);

        bool containsObject = false;

        foreach(Vector3 vector3 in pointsToCheck)
        {
            if (goalBounds.bounds.Contains(vector3))
                containsObject = true;
            else
            {
                containsObject = false;
                break;
            }
        }

        return containsObject;
    }

    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        if (objectToCheck.GetComponentInParent<Cone>() == null)
            return;

        if (CheckConeBounds(objectToCheck))
            goalZoneScoreLink.OptionalBoolValue = true;
        else if (!CheckConeBounds(objectToCheck) || scoreDirection < 0)
            goalZoneScoreLink.OptionalBoolValue = false;
    }
}
