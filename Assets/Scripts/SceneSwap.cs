using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            LoadNextScene();
        }
#else
        if (Input.touchCount > 0)
        {
            LoadNextScene();
        }
#endif
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }
}
