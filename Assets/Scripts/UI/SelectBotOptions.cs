using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SelectBotOptions : MonoBehaviour
{
    public List<GameObject> botPrefabs = new List<GameObject>();
    TeamColor color = TeamColor.Blue;
    int selectedBot = 0;
    bool useLowerSpawn = false;
    bool preloadCone = false;

    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject ConePrefab;

    public UnityEvent FinishedStart;
    public GameObject spawnedBot;
    public GameObject selectBotScreen;

    bool useCustomBot = false;
    GameObject customPrefab;
    private bool autostart = false;

    // Start is called before the first frame update

    public void SelectBot(int index)
    {
        selectedBot = index;
    }

    public void SetSpawn(bool useLower)
    {
        useLowerSpawn = useLower;
    }

    public void SetCone(bool preload)
    {
        preloadCone = preload;
    }

    public void SelectColor(int index)
    {
        color = (TeamColor)index;
    }

    public void AutoStartGame()
    {
        int autostart = PlayerPrefs.GetInt("autostart", 0);
        if (autostart > 0)
        {
            this.autostart = true;
            StartCoroutine(DoAutoStart());
        }
    }

    public void SetAutoStart(int doAuto = 0)
    {
        PlayerPrefs.SetInt("autostart", doAuto);
        PlayerPrefs.Save();
    }

    IEnumerator DoAutoStart()
    {
        //wait for SetPrefs to read preferences and change all properties.
        SetAutoStart(0);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if(spawnAddressablePrefab.lastBot!="")
        {
            spawnAddressablePrefab spawner = FindObjectOfType<spawnAddressablePrefab>();
            spawner.LoadLast();//when loaded will call StartGame.
        }
        else
        {
            StartGame();
        }
        
    }

    public void SetCustomBot(GameObject obj)
    {
        selectBotScreen.SetActive(false);
        customPrefab = obj;
        useCustomBot = true;
        if(autostart)
        {
            StartGame();
        }
    }

    private void SetupControls(GameObject bot)
    {
        InputActionManager input = FindObjectOfType<InputActionManager>();

        List<Drive> drv = input.Drives;
        Dictionary<string, Drive> drives = drv.ToDictionary(x => x.name);
        DriveReceiver[] rcvrs = bot.GetComponentsInChildren<DriveReceiver>();
        foreach( DriveReceiver rcvr in rcvrs)
        {
            if (rcvr.drive == null) { continue; }
            string drvName = rcvr.drive.name;
            rcvr.drive = drives[drvName];
        }
        ObjectGrabber grabber = bot.GetComponentInChildren<ObjectGrabber>();
        if (grabber) { grabber.objectGrabberDrive = input.GetDrive("motor2"); }
        DriveReceiverSpinningWheels mainRcv = bot.GetComponent<DriveReceiverSpinningWheels>();
        if(mainRcv != null)
        {
            mainRcv.frontLeft = input.GetDrive("frontleft");
            mainRcv.frontRight = input.GetDrive("frontright");
            mainRcv.backLeft = input.GetDrive("backleft");
            mainRcv.backRight = input.GetDrive("backright");
        }
    }

    public void StartGame()
    {
        if (spawnedBot) { Destroy(spawnedBot); }
        int spawnIdx = (int)color;
        if (useLowerSpawn) { spawnIdx += 2; }

        GameObject prefab = botPrefabs[selectedBot];
        if (useCustomBot) { prefab = customPrefab; }

        GameObject bot = GameObject.Instantiate(prefab,spawnPoints[spawnIdx].position, spawnPoints[spawnIdx].rotation);
        bot.GetComponent<ColorSwitcher>().TeamColor_ = color;
        bot.GetComponent<ColorSwitcher>().SetColor();
        bot.GetComponent<ScoreObjectTypeLink>().LastTouchedTeamColor = color;
        SetupControls(bot);

        CameraSwitcher cams = FindObjectOfType<CameraSwitcher>();
        if (cams != null) 
        {
            Camera c = bot.GetComponentInChildren<Camera>();
            if (c) { cams.AddCam(bot.GetComponentInChildren<Camera>().gameObject); }
        }

        AdjustLaser laser = bot.GetComponentInChildren<AdjustLaser>();
        laser.ToggleLaser(false,true);
        spawnedBot = bot;
        FieldManager.botColor = color;
        if (preloadCone)
        {
            StartCoroutine(DoPreload(bot));
        }
        else { FinishedStart.Invoke(); }
        autostart = false;
    }

    IEnumerator DoPreload(GameObject bot)
    {
        GameObject cone = GameObject.Instantiate(ConePrefab, bot.transform.position, bot.transform.rotation);
        cone.GetComponent<ColorSwitcher>().TeamColor_ = color;
        cone.GetComponent<ColorSwitcher>().SetColor();
        cone.GetComponent<ScoreObjectTypeLink>().LastTouchedTeamColor = color;
        ObjectGrabber grabber = bot.GetComponentInChildren<ObjectGrabber>();
        ObjectChecker checker = grabber.GetComponent<ObjectChecker>();
        checker.CanPickUp = true;
        checker.ObjectInTrigger = cone;
        cone.transform.position = grabber.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        grabber.PickUpOrPutDownObject();
        FinishedStart.Invoke();
    }

    void Start()
    {
        AutoStartGame();
    }
}