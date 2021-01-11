using UnityEngine;
using TMPro;

public class EndText: MonoBehaviour {

    public TMPro.TMP_Text text;

    public void Start() {
        text.text += GameController.score.ToString();
    }
}