using System.Collections;
using UnityEngine;
using TMPro;  // 使用 TextMeshPro

public class BulletReflectCounter : MonoBehaviour
{
    public TextMeshProUGUI comboText;  // 显示连击数的文本
    private int comboCount = 0;  // 当前连击数
    private float comboTimer = 0f;  // 连击倒计时器
    public float comboDuration = 2f;  // 连击持续时间（2秒）
    private bool comboActive = false;  // 用于跟踪连击状态

    private GameObject player; // 用于存储玩家对象
    private GameObject[] bullets; // 子弹数组

    private void Start()
    {
        // 查找玩家对象
        player = GameObject.FindGameObjectWithTag("Player");

        // 初始化 UI 隐藏
        comboText.gameObject.SetActive(false);  // 初始隐藏 Combo 文本
    }

    private void Update()
    {
        // 检测玩家是否按下 E 键
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReflectBullets();  // 当按下 E 键时反弹子弹
        }

        // 如果 Combo 处于激活状态，处理 Combo 计时器
        if (comboActive)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0f)
            {
                ResetCombo();  // 如果2秒没有反弹子弹，重置连击
            }
        }
    }

    // 子弹反弹逻辑
    private void ReflectBullets()
    {
        // 查找所有子弹对象（通过标签标记）
        bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        bool reflected = false;  // 用于检测是否有子弹被反弹

        foreach (GameObject bullet in bullets)
        {
            if (Vector2.Distance(player.transform.position, bullet.transform.position) < 2f)
            {
                // 计算反弹方向（子弹飞向敌人）
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                if (enemy != null)
                {
                    Vector2 direction = (enemy.transform.position - bullet.transform.position).normalized;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = direction * 10f;  // 设置反弹速度
                        reflected = true;  // 成功反弹子弹
                    }
                }
            }
        }

        // 如果成功反弹子弹，增加连击数并更新 UI
        if (reflected)
        {
            IncreaseCombo();
        }
    }

    // 增加连击数
    private void IncreaseCombo()
    {
        comboCount++;  // 增加连击数
        comboTimer = comboDuration;  // 重置连击计时器

        // 当达到 3 连击时显示 Combo 文本
        if (comboCount >= 3)
        {
            comboText.gameObject.SetActive(true);  // 显示 Combo 文本
            comboActive = true;  // 激活 Combo 状态
        }

        // 更新 UI 显示
        UpdateComboUI();
    }

    // 更新 UI 显示连击数
    private void UpdateComboUI()
    {
        if (comboCount >= 3)  // 只有连击数大于等于 3 时才显示 Combo
        {
            comboText.text = "Combo " + comboCount.ToString();
        }
    }

    // 重置连击数
    public void ResetCombo()
    {
        comboCount = 0;  // 重置连击数
        comboText.gameObject.SetActive(false);  // 隐藏 Combo 文本
        comboActive = false;  // 重置 Combo 状态
    }
}
