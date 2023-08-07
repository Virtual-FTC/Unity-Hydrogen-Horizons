using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLaser : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray r = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(r,out hit,10,layerMask,QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.isTrigger) { return; }
            Debug.DrawRay(transform.position, hit.point);
            float distance = hit.distance;
            Vector3 newScale = transform.localScale;
            newScale.z = distance * 0.3f;
            transform.localScale = newScale;
            //Debug.Log(hit.collider.name);
        }
    }

    public void ToggleLaser(bool on=true, bool turnBackOn = false)
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>(true);
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.gameObject.SetActive(on);
        }
        if(turnBackOn)
        {
            Invoke("LaserOn", 8);
        }
    }
    private void LaserOn()
    {
        ToggleLaser(true);
    }
}
