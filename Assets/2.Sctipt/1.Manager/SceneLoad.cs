using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneLoad : MonoBehaviour  //Data Field
{
    [SerializeField]
    private string sceneName = string.Empty;
}

public partial class SceneLoad : MonoBehaviour  //Function Field
{
    public void Active()
    {
        StartCoroutine(SceneLoadStart());
    }

    IEnumerator SceneLoadStart()
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        yield return asyncOperation;
    }

    public void Active(string _sceneName)
    {
        StartCoroutine(SceneLoadStart(_sceneName));
    }

    IEnumerator SceneLoadStart(string _sceneName)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName);
        yield return asyncOperation;
    }
}