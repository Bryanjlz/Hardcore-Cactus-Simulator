using UnityEngine;
using System;

public class GameController: MonoBehaviour {
    //Singleton
    public static GameController instance;

    //Conversion from seconds to C# ticks
    private const int SECOND_TO_TICK = 10000000;

    //Control values
    //In Kelvin
    public int temperature = 293;
    public DateTime time;
    public float timeMultiplier = 1.0f;

    public void Start() {
        if (!instance) {
            instance = this;
        }
        time = new DateTime(2000, 1, 1, 0, 0, 0);
    }

    public void Update() {
        float delta = Time.deltaTime;
        time = time.Add(new TimeSpan((long) (delta * timeMultiplier * SECOND_TO_TICK)));
    }
}