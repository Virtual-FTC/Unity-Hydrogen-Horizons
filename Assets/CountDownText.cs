using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownText : MonoBehaviour
{
    TextMeshProUGUI text;

    float baseTextSize;

    float currentTextSize;

    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        baseTextSize = text.fontSize;

        text.fontSize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(text.fontSize != 0)
        {
            text.fontSize = Mathf.Lerp(text.fontSize, 0, speed * Time.deltaTime);
        }
    }

    public void CountDown()
    {
        StartCoroutine(TextCountDown());
    }

    IEnumerator TextCountDown()
    {

        for (int i = 3; i > 0; i--)
        {
            text.text = i.ToString();

            text.fontSize = baseTextSize; 
            yield return new WaitForSeconds(1);
        }
    }
}
