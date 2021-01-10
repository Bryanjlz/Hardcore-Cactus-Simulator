using UnityEngine;
using System;
using TMPro;

public class Thermostat: MonoBehaviour {

    //Conversion from seconds to C# ticks
    private const int SECOND_TO_TICK = 10000000;

    //Children GameObjects
    public TMPro.TMP_Text dateText;
    public TMPro.TMP_Text temperatureText;

    //Control values
    //In Kelvin
    [Tooltip("In Kelvin")]
    public int temperature = 293;
    public DateTime time;
    public float timeMultiplier = 1.0f;

    public void Start() {
        time = new DateTime(2000, 1, 1, 0, 0, 0);
        dateText.text = time.ToString("yyyy-MM-dd\nHH:mm:ss");
    }

    public void Update() {
        float delta = Time.deltaTime;
        time = time.Add(new TimeSpan((long) (delta * timeMultiplier * SECOND_TO_TICK)));
        dateText.text = time.ToString("yyyy-MM-dd\nHH:mm:ss");
    }
}