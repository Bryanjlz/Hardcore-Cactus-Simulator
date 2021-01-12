using UnityEngine;

public class ThermostatButton: MonoBehaviour {

    bool incre;
    bool decre;

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
                incre = true;
                
                break;
            case ButtonType.DECREMENT:
                decre = true;
                DecrementTemperature();
                break;
            case ButtonType.CACTUS:
                BuyCactus();
                break;
        }
    }

    public void OnMouseUp() {
        incre = false;
        decre = false;
    }

    private void Update() {
        if (incre) {
            IncrementTemperature();
        }
        if (decre) {
            DecrementTemperature();
        }
    }

    public void IncrementTemperature() {
        GameController.instance.temperature += Time.deltaTime * 25;
    }

    public void DecrementTemperature() {
        GameController.instance.temperature -= Time.deltaTime * 25;
    }

    public void BuyCactus() {
        GameController.instance.timeMultiplier = GameController.instance.CalculateTimeMultiplier(++GameController.instance.plants);
        CactiController.instance.AddCactus();
    }
}