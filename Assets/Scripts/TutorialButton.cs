using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialButton: MonoBehaviour {
    public string actuallyUsefulishHint;
    public GameObject panel;
    public TMPro.TMP_Text panelText;
    private string internalText;
    private Coroutine routine;

    public void Start() {
        internalText = panelText.text;
    }

    public void EnablePanel() {
        panel.SetActive(true);
        routine = StartCoroutine(LolText());
    }

    public void DisablePanel() {
        panel.SetActive(false);
        panelText.text = internalText;
        StopCoroutine(routine);
    }

    IEnumerator LolText() {
        yield return new WaitForSecondsRealtime(10);
        panelText.text += "\n\n\n" + actuallyUsefulishHint;

        yield return null;
    }
}