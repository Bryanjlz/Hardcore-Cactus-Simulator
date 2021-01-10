using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene: MonoBehaviour {

    public string sceneName;

    public void OnMouseDown() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


}