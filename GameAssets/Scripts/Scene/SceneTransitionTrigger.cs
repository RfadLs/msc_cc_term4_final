using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            SceneTransitionManager.instance.NextLevel();
        }
    }
}
