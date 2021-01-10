using UnityEngine;

public class ThermostatButton: MonoBehaviour {

    public enum ButtonType {
        INCREMENT,
        DECREMENT,
        CACTUS,
    }

    public Thermostat thermostat;
    public ButtonType type;

    public void OnMouseDown() {
        print("A");
        switch (type) {
            case ButtonType.INCREMENT:
                IncrementTemperature();
                break;
            case ButtonType.DECREMENT:
                DecrementTemperature();
                break;
            case ButtonType.CACTUS:
                BuyCactus();
                break;
        }
    }

    public void IncrementTemperature() {
        thermostat.temperature++;
        thermostat.temperatureText.text = thermostat.temperature.ToString();
    }

    public void DecrementTemperature() {
        thermostat.temperature--;
        thermostat.temperatureText.text = thermostat.temperature.ToString();
    }

    public void BuyCactus() {

    }
}