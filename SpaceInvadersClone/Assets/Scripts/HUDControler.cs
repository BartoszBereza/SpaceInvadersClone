using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDControler : MonoBehaviour {
    public void ChangeScene (int sceneNumber) {
        SceneManager.LoadScene (sceneNumber);
    }

    public void ExitGame () {
        Application.Quit();
    }
}