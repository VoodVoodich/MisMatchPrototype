using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    string sceneNameToLoad;

    // Update is called once per frame
    public void LoadGivenScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
