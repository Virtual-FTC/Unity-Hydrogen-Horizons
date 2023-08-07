using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRobotWithinBounds : MonoBehaviour, ICustomGoalChecker, ICustomGoalEvents
{
    GoalZoneScoreLink goalZoneScoreLink;
    GameObject goalBoundsObject;
    BoxCollider goalBounds;
    [SerializeField] ScoringGuide scoringGuide;
    private Collider objectToCheckCollider;

    Vector3[] pointsToCheck = new Vector3[2];
    private bool containsObject, scoreAdded;

    GameObject objectToCheck;

    // Start is called before the first frame update
    void Start()
    {
        goalBoundsObject = this.gameObject;
        goalBounds = GetComponent<BoxCollider>();
        goalZoneScoreLink = GetComponent<GoalZoneScoreLink>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoCustomCheck()
    {

    }

    IEnumerator CheckRobotBounds()
    {
        while (true)
        {
            pointsToCheck[0] = objectToCheckCollider.transform.position + objectToCheckCollider.bounds.extents;
            pointsToCheck[1] = objectToCheckCollider.transform.position - objectToCheckCollider.bounds.extents;

            containsObject = false;

            if (goalBounds.bounds.Contains(pointsToCheck[0]) && 
                goalBounds.bounds.Contains(pointsToCheck[1]))
            {
                containsObject = true;
            }
            else
            {
                containsObject = false;
            }

            if (!scoreAdded && containsObject)
            {
                TeamColor teamColor = goalZoneScoreLink.LastObjectTeamColor;
                goalZoneScoreLink.ChangeScore(2, scoringGuide, 1, teamColor);
                scoreAdded = true;
            }

            else if (scoreAdded && !containsObject)
            {
                TeamColor teamColor = goalZoneScoreLink.LastObjectTeamColor;
                goalZoneScoreLink.ChangeScore(2, scoringGuide, -1, teamColor);
                scoreAdded = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void DoCustomOnEvent(UnityEngine.Object objectToPass)
    {
        if(objectToCheck == null) { return; }
        objectToCheckCollider = objectToCheck.GetComponent<Collider>();
        StartCoroutine(CheckRobotBounds());
    }

    public void DoCustomOffEvent(UnityEngine.Object objectToPass)
    {
        goalZoneScoreLink.OptionalBoolValue = false;
        StopAllCoroutines();
    }

    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        if (objectToCheck.GetComponentInParent<ScoreObjectTypeLink>() == null || 
            objectToCheck.GetComponentInParent<ScoreObjectTypeLink>().ScoreObjectType_.name != "Robot")
            return;

        this.objectToCheck = objectToCheck;
        goalZoneScoreLink.OptionalBoolValue = true;
    }
}

