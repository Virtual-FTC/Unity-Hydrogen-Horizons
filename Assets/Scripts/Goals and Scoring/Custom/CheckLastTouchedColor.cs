using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLastTouchedColor : MonoBehaviour
{
    public TeamColor LastTouchedColor { get; private set; }

    [SerializeField] ObjectType objectTypeToCheck;
    private void OnCollisionEnter(Collision collision)
    {
        ScoreObjectTypeLink scoreObjectTypeLink = 
            collision.gameObject.GetComponentInParent<ScoreObjectTypeLink>();

        if (scoreObjectTypeLink == null)
            return;

        if (scoreObjectTypeLink.ScoreObjectType_ == objectTypeToCheck)
        {
            LastTouchedColor = scoreObjectTypeLink.LastTouchedTeamColor;
        }
    }
}
