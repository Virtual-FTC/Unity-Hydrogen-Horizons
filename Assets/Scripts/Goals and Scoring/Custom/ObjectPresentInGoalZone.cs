using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPresentInGoalZone : MonoBehaviour, ICustomGoalEvents
{
    int numberOfObjects;
    public bool ObjectPresent { get { return (numberOfObjects > 0); } }
    public GameEvent checkCircuits;
    public void DoCustomOffEvent(Object objectToPass)
    {
        numberOfObjects--;
        checkCircuits.Raise();
    }

    public void DoCustomOnEvent(Object objectToPass)
    {
        numberOfObjects++;
        checkCircuits.Raise();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
