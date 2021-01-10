using UnityEngine;
using System;

public class GameController: MonoBehaviour {
    //Singleton
    public static GameController instance;

    //Conversion from seconds to C# ticks
    private const int SECOND_TO_TICK = 10000000;
    public const int MINUTE = 60;
    public const int HOUR = 60 * MINUTE;
    public const int DAY = 24 * HOUR;
    public const int MONTH = 30 * DAY;
    public const int YEAR = 12 * MONTH;

    private const int CELSIUS_TO_KELVIN = 273;

    //Control values
    //In Kelvin
    public int temperature = CELSIUS_TO_KELVIN + 20;
    public DateTime time;
    public float timeMultiplier = 1.0f;
    public int plants = 1;

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


    public float CalculateTimeMultiplier(int plants) {
        if (plants < 0) {
            throw new ArgumentException("Number of plants must be non-negative");
        } else if (plants <= 1) {
            //With a single plant, or no plants, time works as expected (1 second in real time is 1 second in game)
            return 1;
        } else if (plants < 10) {
            //As you get more plants, we see a higher rise in time taken
            //Exponential, since linear increases are harder to scale, and because the jumps feel better
            //Low coefficient means the first few plants have noticeable, but not absurd values
            //f(2) ~ 2.2, f(3) ~ 9.2
            //f(10) is close to, but not equal to 1000000, so it is excluded
            return 0.875f + 0.125f*(plants * plants) * Mathf.Exp(plants - 1);
        } else if (plants <= 20) {
            //At this high amount, we want the time to 'feel' stable while still increasing
            //Linear function means it feels like a slower relative increase, despite the overall higher slope
            //Key points: f(10)=100000 (a little over 1 second/dayk), f(20)=2000000 (a little under a month per second)
            return 100000 + 190000*(plants - 10);
        } else {
            //Sextic(?) function so you achieve the optimal 1 year/second by f(30)
            //Added to lienar function for continuity
            return 2.77f * Mathf.Pow((plants-20), 7) + 2000000 + 190000 * (plants - 20);
        }
    }
}