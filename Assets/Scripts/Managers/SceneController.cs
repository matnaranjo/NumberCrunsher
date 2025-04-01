using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(WaitScene(sceneName));
    }

    IEnumerator WaitScene(string sceneName)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName);
    }
}
