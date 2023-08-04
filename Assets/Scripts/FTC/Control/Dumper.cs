using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumper : MonoBehaviour
{
    public IntakeControl intake;
    // Start is called before the first frame update
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Cube" || collision.tag == "Ball" || collision.tag == "Duck")
        {
            if (intake.itemInBasket) intake.itemInBasket=false;
        }
    }
}
