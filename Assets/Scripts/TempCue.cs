using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCue : MonoBehaviour
{
    
    private float temp;
    private const float PERFECT_TEMP = 68;
    private const float ERROR = PERFECT_TEMP * 0.05f;
    private const float REALLY_BAD = PERFECT_TEMP * 0.1f;

    public SpriteRenderer tablee;
    public Color blue;
    public Color red;
    public Color white;

    private void Update() {
        //Get current temp
        temp = GameController.instance.temperature;

        if (temp - PERFECT_TEMP < -ERROR) {
            if (temp-PERFECT_TEMP < -REALLY_BAD) {
                tablee.color = blue;
            } else {
                tablee.color = new Color(blue.r, blue.g, blue.b, ((-(temp - PERFECT_TEMP)) / (REALLY_BAD))-.5f);
            }
        } else if (temp - PERFECT_TEMP > ERROR) {
            if (temp - PERFECT_TEMP > REALLY_BAD) {
                tablee.color = red;
            } else {
                tablee.color = new Color(red.r, red.g, red.b, ((temp - PERFECT_TEMP) / (REALLY_BAD))-.5f);
            }
        } else {
            tablee.color = white;
        }
    }
}
