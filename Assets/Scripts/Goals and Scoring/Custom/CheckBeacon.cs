using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBeacon : MonoBehaviour, ICustomGoalChecker
{
    private GoalZoneScoreLink goalZoneScoreLink;

    // Start is called before the first frame update
    void Start()
    {
        goalZoneScoreLink = GetComponent<GoalZoneScoreLink>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoCustomCheck()
    {
        throw new System.NotImplementedException();
    }

    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        if (objectToCheck.GetComponentInParent<Beacon>() == null)
            return;

        goalZoneScoreLink.OptionalBoolValue = true;
    }

}
