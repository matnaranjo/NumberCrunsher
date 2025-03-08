using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Change the scene to the specified scene name
    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);  // Load the scene with the given name
    }
}
