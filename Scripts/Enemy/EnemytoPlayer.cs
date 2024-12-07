using UnityEngine;
using UnityEngine.SceneManagement; // 用于重新加载场景

public class EnemytoPlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // 敌人的移动速度
    private bool isChasing = false; // 是否开始追逐
    private Transform player; // 玩家 Transform

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 找到玩家对象
    }

    void Update()
    {
        if (isChasing)
        {
            // 仅在 X 轴上移动敌人
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StartChase()
    {
        isChasing = true; // 开始追逐
    }

    public void StopChase()
    {
        isChasing = false; // 停止追逐
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 玩家与敌人碰撞时重新加载当前场景
            Debug.Log("Player triggered by enemy. Reloading scene...");
            ReloadScene();
        }
    }


    private void ReloadScene()
    {
        Invoke("LoadScene", 3f);
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
