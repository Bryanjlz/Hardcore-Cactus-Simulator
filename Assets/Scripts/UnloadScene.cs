using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene: MonoBehaviour {
    
    public string sceneName;

    public void Unload() {
        SceneManager.UnloadSceneAsync(sceneName);
    }


}