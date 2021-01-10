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
        GameController.instance.temperature++;
    }

    public void DecrementTemperature() {
        GameController.instance.temperature--;
    }

    public void BuyCactus() {
        GameController.instance.timeMultiplier = GameController.instance.CalculateTimeMultiplier(++GameController.instance.plants);
    }
}