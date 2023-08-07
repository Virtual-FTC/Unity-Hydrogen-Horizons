using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> cameras = new List<GameObject>();
    int currentCameraNumber;
    // Start is called before the first frame update
    void Start()
    {
        currentCameraNumber = 0;
    }

    public void SwitchCamera(int cameraNumber)
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
        }

        cameras[cameraNumber].SetActive(true);
        currentCameraNumber = cameraNumber;
    }
    public void NextCamera()
    {
        currentCameraNumber = currentCameraNumber > cameras.Count - 2 ? currentCameraNumber = 0: currentCameraNumber + 1;
        cameras[currentCameraNumber].SetActive(true);
        int previousCamera = currentCameraNumber - 1 < 0 ? cameras.Count - 1 : currentCameraNumber - 1;
        cameras[previousCamera].SetActive(false);
    }
    public void PrevCamera()
    {
        currentCameraNumber = currentCameraNumber < 1 ? currentCameraNumber = cameras.Count - 1: currentCameraNumber - 1;
        cameras[currentCameraNumber].SetActive(true);
        int previousCamera = currentCameraNumber + 1 > cameras.Count - 1 ? 0 : currentCameraNumber + 1;
        cameras[previousCamera].SetActive(false);
    }

    public void AddCam(GameObject cam)
    {
        cameras.Add(cam);
        SwitchCamera(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSwitchCamera()
    {
        NextCamera();
    }
}
