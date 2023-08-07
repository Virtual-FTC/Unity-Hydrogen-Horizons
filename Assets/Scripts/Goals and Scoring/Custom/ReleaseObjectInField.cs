using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObjectInField : MonoBehaviour
{
    public List<GameObject> Objects;
    public List<GameObject> Location;
    public bool cloneObject = false;
    TeamColor team;

    private void Reset()
    {
        Objects.Clear();
        for(int i = 0; i < 2; i++)//for team colors.
        {
            Objects.Add(null);
            Location.Add(null);
        }  
    }

    public void Release(int team)
    {
        GameObject obj = Objects[(int)team];
        if(cloneObject)
        {
            obj = Instantiate(Objects[team]);
            obj.GetComponent<ColorSwitcher>().TeamColor_ = (TeamColor)team;
            obj.GetComponent<ColorSwitcher>().SetColor();
        }
        obj.transform.position = Location[(int)team].transform.position;
        obj.GetComponent<Rigidbody>().isKinematic = false;
    }

}
