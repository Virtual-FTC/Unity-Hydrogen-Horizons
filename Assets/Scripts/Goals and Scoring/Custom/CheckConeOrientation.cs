using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConeOrientation : MonoBehaviour, ICustomGoalChecker
{
    GoalZoneScoreLink goalZoneScoreLink;

    private void Start()
    {
        goalZoneScoreLink = GetComponent<GoalZoneScoreLink>();
    }

    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        if (objectToCheck.GetComponentInParent<Cone>() == null)
            return;

        ConeOrientation coneOrientation = null;

        // We don't care about the orientation when the cone is removed
        if (scoreDirection < 0)
        {
            goalZoneScoreLink.OptionalBoolValue = true;
            return;
        }

        if (objectToCheck.GetComponent<ConeOrientation>() != null)
            coneOrientation = objectToCheck.GetComponent<ConeOrientation>();
        else if (objectToCheck.GetComponentInChildren<ConeOrientation>() != null)
            coneOrientation = objectToCheck.GetComponentInChildren<ConeOrientation>();
        else if (objectToCheck.GetComponentInParent<ConeOrientation>() != null)
            coneOrientation = objectToCheck.GetComponentInParent<ConeOrientation>();

        if (coneOrientation.IsRightSideUp)
            goalZoneScoreLink.OptionalBoolValue = true;
        else
            goalZoneScoreLink.OptionalBoolValue = false;
    }

    public void DoCustomCheck()
    {
    }
}
