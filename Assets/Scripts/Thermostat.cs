using UnityEngine;
using System;
using TMPro;

public class Thermostat: MonoBehaviour {

    //Children GameObjects
    public TMPro.TMP_Text dateText;
    public TMPro.TMP_Text temperatureText;

    public void Start() {
        dateText.text = new DateTime(2000, 1, 1, 0, 0, 0).ToString("yyyy-MM-dd\nHH:mm:ss");
    }

    public void Update() {
        dateText.text = GameController.instance.time.ToString("yyyy-MM-dd\nHH:mm:ss");
        temperatureText.text = GameController.instance.temperature.ToString();
    }
}