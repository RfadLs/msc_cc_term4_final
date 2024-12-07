using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAInoChase : MonoBehaviour
{
    public float moveSpeed; // 敌人的移动速度
    public float attackRange; // 攻击范围
    public float attackCooldown; // 攻击冷却时间
    public GameObject bulletPrefab; // 子弹的预制体
    public Transform[] firePoints; // 使用数组来存储多个发射点

    public float bulletSpeed; // 子弹速度
    private GameObject player; // 玩家对象
    private float attackCooldownTimer; // 攻击冷却计时器
    public int health = 5; // 敌人的初始血量为 5
    public float minAngle = -45f; // 最小角度
    public float maxAngle = 45f;  // 最大角度
    private float noiseOffset; // 噪声偏移


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
        noiseOffset = Random.Range(0f, 10f); // 初始化噪声偏移
    }

    void Update()
    {
        // 检测玩家是否在攻击范围内
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (attackCooldownTimer <= 0)
            {
                Shoot();
                attackCooldownTimer = attackCooldown; // 重置攻击冷却
            }
        }

        // 更新攻击冷却计时器
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        int firePointCount = firePoints.Length;
        for (int i = 0; i < firePointCount; i++)
        {
            Transform firePoint = firePoints[i]; // 获取每个发射点
            // 使用 Perlin Noise 生成平滑变化的角度
            float noiseValue = Mathf.PerlinNoise(Time.time + noiseOffset + i, 0f);
            float noiseAngle = Mathf.Lerp(minAngle, maxAngle, noiseValue);
            Vector2 direction = Quaternion.Euler(0, 0, noiseAngle) * Vector2.left; // 旋转方向

            // 实例化子弹
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = (player.transform.position - firePoint.position).normalized; // 指向玩家
            rb.velocity = shootDirection * bulletSpeed; // 设置子弹速度

            // 设置子弹速度为计算出的方向
            rb.velocity = direction * bulletSpeed;


        }
    }

    // 添加死亡函数
    private void Die()
    {
        // 可以在这里添加死亡动画、音效等
        Destroy(gameObject); // 销毁敌人对象
    }
}

