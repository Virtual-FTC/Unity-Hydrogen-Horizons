using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestBotScoring : MonoBehaviour
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("PowerPlayNewBots", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator TestSpawn()
    {
        yield return TestBotSpawn();
    }

    public IEnumerator TestBotSpawn()
    {
        TestHelper.botOptions = GameObject.FindObjectOfType<SelectBotOptions>();
        TestHelper.botOptions.StartGame();
        DriveReceiverSpinningWheels wheels = TestHelper.botOptions.spawnedBot.GetComponent<DriveReceiverSpinningWheels>();
        yield return new WaitForSeconds(0.5f);
        TestHelper.botOptions.gameObject.SetActive(false);
        for (int i = 0; i < 1209; i++)
        {
            wheels.frontLeft.driveAmount.x = 1;
            wheels.frontRight.driveAmount.x = 1;
            yield return new WaitForEndOfFrame();
        }
    }
}
