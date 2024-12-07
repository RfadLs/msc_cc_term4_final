using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    [SerializeField] private Animator CrossFadeAnimator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        if (CrossFadeAnimator != null)
        {

            StartCoroutine(LoadLevel());
        }
    }

    private IEnumerator LoadLevel()
    {

        CrossFadeAnimator.SetTrigger("Start"); // 触发开始动画

        // 等待动画完成（与动画时长一致）
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        CrossFadeAnimator.SetTrigger("End"); // 触发结束动画
    }
}
