using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int health = 3; // 角色的初始生命值
    [System.Serializable]
    public class Heart
    {
        public Image image; // 心形的 UI 图片
        public Sprite fullHeart; // 完整的心形图片
        public Sprite emptyHeart; // 空的心形图片
    }

    public Heart[] hearts; // 存储所有心形

    // 调用这个方法来减少角色的生命值
    public void TakeDamage()
    {
        if (health > 0)
        {
            health--; // 生命值减少
            UpdateHearts(); // 更新心形 UI
        }

        if (health <= 0)
        {
            Die(); // 角色死亡
        }
    }

    // 更新心形 UI 显示
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].image.sprite = hearts[i].fullHeart; // 显示完整的心
            }
            else
            {
                hearts[i].image.sprite = hearts[i].emptyHeart; // 显示空心
            }
        }
    }

    // 角色死亡的方法
    private void Die()
    {
        Invoke("LoadScene", 2f);
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
