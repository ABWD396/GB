using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public Text fixedUpdateValueText;
    public Text fixedUpdateValueFps;
    private float fixedUpdateValue;

    public Text deltaTimeValueText;
    public Text deltaTimeValueFps;
    private float deltaTimeValue;

    public Text timeTimeValueText;
    public Text timeTimeValueFps;
    private float timeTimeValue;

    float seconds = 0;
    float deltaSeconds = 0;
    float fixedSeconds = 0;
    float countUpdates = 0;
    float countDeltaUpdates = 0;
    float countFixedUpdates = 0;
    // Start is called before the first frame update
    void Start()
    {
        fixedUpdateValue = 0;
        deltaTimeValue = 0;
        timeTimeValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countUpdates++;
        countDeltaUpdates++;

        deltaTimeValue += Time.deltaTime;
        timeTimeValue = Time.time;

        timeTimeValueText.text = timeTimeValue.ToString();
        deltaTimeValueText.text = deltaTimeValue.ToString();

        if (timeTimeValue - seconds >= 1)
        {
            seconds++;
            timeTimeValueFps.text = countUpdates.ToString();
            countUpdates = 0;
        }

        if (deltaTimeValue - deltaSeconds >= 1)
        {
            deltaSeconds++;
            deltaTimeValueFps.text = countDeltaUpdates.ToString();
            countDeltaUpdates = 0;
        }
    }

    private void FixedUpdate()
    {
        countFixedUpdates++;

        fixedUpdateValue += Time.fixedDeltaTime;
        fixedUpdateValueText.text = fixedUpdateValue.ToString();

        if (fixedUpdateValue - fixedSeconds >= 1)
        {
            fixedSeconds++;
            fixedUpdateValueFps.text = countFixedUpdates.ToString();
            countFixedUpdates = 0;
        }
    }
}
