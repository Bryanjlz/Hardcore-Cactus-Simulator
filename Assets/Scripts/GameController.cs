using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController: MonoBehaviour {
    //Singleton
    public static GameController instance;

    public SeasonalController seasonalController;

    //Conversion from seconds to C# ticks
    private const int SECOND_TO_TICK = 10000000;
    public const int MINUTE = 60;
    public const int HOUR = 60 * MINUTE;
    public const int DAY = 24 * HOUR;
    public const int MONTH = 30 * DAY;
    public const int YEAR = 12 * MONTH;

    //Control values
    //In Fahrenheit
    public float temperature = 68;
    //In Celcius
    public float lowerTemp = 59;
    public float higherTemp = 77;
    public float tempSwap = 20;
    private float timeSinceTempSwap = 0;
    private float towardsTemperature = 0;

    public DateTime time;
    public float inGameDeltaTime;
    public static int score = 0;

    public DateTime timeToDie = new DateTime(2060, 1, 1, 0, 0, 0);

    public float timeMultiplier = 1.0f;
    public int plants = 1;

    // Mouse Positions
    public float mousex;
    public float mousey;

    public void Start() {
        if (!instance) {
            instance = this;
        }
        time = new DateTime(2000, 1, 1, 0, 0, 0);
        score = 0;
        towardsTemperature = lowerTemp;

        SceneManager.LoadScene("PosterScene", LoadSceneMode.Additive);
        CactiController.instance.AddCactus();
    }

    public void Update() {
        float delta = Time.deltaTime;
        timeSinceTempSwap += Time.deltaTime;
        inGameDeltaTime = delta * timeMultiplier;
        time = time.Add(new TimeSpan((long)(inGameDeltaTime * SECOND_TO_TICK)));

        mousex = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        mousey = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;

        if (time > timeToDie) {
            SceneManager.LoadScene("End Screen", LoadSceneMode.Single);
        }

        //float towardsTemperature = seasonalController.GetAmbientTemperature(time);
       
        if (timeSinceTempSwap > tempSwap) {
            if (towardsTemperature == higherTemp) {
                towardsTemperature = lowerTemp;
            } else {
                towardsTemperature = higherTemp;
            }
            timeSinceTempSwap = 0;
        }
        float tempChange = delta * (towardsTemperature - temperature)/20;

        //prevent bad;
        if (Mathf.Abs(tempChange) < 1f) {
            temperature += tempChange;
        }
    }

    public float CalculateTimeMultiplier(int plants) {
        int intervalSize = 10;
        float exponentialCoefficient = 0.125f;
        float exponentialConstant = 0.875f;

        int multAt10 = 100000;
        int multAt20 = 2000000;
        int linearScaling = (multAt20 - multAt10)/intervalSize;

        float sexticCoefficient = 2.77f;

        if (plants < 0) {
            throw new ArgumentException("Number of plants must be non-negative");
        } else if (plants <= 1) {
            //With a single plant, or no plants, time works as expected (1 second in real time is 1 second in game)
            return 1;
        } else if (plants < intervalSize) {
            //As you get more plants, we see a higher rise in time taken
            //Exponential, since linear increases are harder to scale, and because the jumps feel better
            //Low coefficient means the first few plants have noticeable, but not absurd values
            //f(2) ~ 2.2, f(3) ~ 9.2
            //f(10) is close to, but not equal to 1000000, so it is excluded
            return exponentialConstant + exponentialCoefficient *(plants * plants) * Mathf.Exp(plants - 1);
        } else if (plants <= 2 * intervalSize) {
            //At this high amount, we want the time to 'feel' stable while still increasing
            //Linear function means it feels like a slower relative increase, despite the overall higher slope
            //Key points: f(10)=100000 (a little over 1 second/dayk), f(20)=2000000 (a little under a month per second)
            return multAt10 + linearScaling * (plants - intervalSize);
        } else {
            //Sextic(?) function so you achieve the optimal 1 year/second by f(30)
            //Added to lienar function for continuity
            return sexticCoefficient * Mathf.Pow((plants - 2 * intervalSize), 7) + multAt20 + linearScaling * (plants - 2 * intervalSize);
        }
    }
}