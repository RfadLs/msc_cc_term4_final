using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void TriggerSceneTransition()
    {
        SceneTransitionManager.instance.NextLevel();
    }
}
