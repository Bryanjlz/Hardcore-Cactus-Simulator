using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenThermostat: MonoBehaviour {

    public void OnMouseDown() {
        SceneManager.LoadScene("ThermostatScene", LoadSceneMode.Additive);
    }


}