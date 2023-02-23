using UnityEngine;

public class WelcomeScene : MonoBehaviour
{
    [SerializeField]
    SceneSwap sceneSwaper;

    public SceneSwap GetSceneSwaper 
    { 
        get { return sceneSwaper;  }
        private set { sceneSwaper = value; }
    }

    private void Awake()
    {
        GetSceneSwaper = Instantiate(sceneSwaper).GetComponent<SceneSwap>();
        GetSceneSwaper.name = "SceneSwaper";
    }
}
