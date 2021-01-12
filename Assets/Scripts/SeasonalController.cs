using UnityEngine;
using System;

public class SeasonalController: MonoBehaviour {
    
    public float temp;

    public float GetAmbientTemperature(DateTime time) {
        float amplitude = 10;
        float phaseShift = -2 * Mathf.PI/3;
        float average = 20;
        float periodScale = 2*Mathf.PI/365;
    
        //Domain of x will be [0, 366]
        float x = time.DayOfYear;
        temp = amplitude * Mathf.Sin(periodScale * x + phaseShift) + average;
        return amplitude * Mathf.Sin(periodScale * x + phaseShift) + average;
    }
}