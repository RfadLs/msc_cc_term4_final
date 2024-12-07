using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed; // 敌人的移动速度
    public float attackRange; // 攻击范围
    public float attackCooldown; // 攻击冷却时间
    public GameObject bulletPrefab; // 子弹的预制体
    public Transform firePoint; // 子弹发射位置
    public float bulletSpeed; // 子弹速度
    private GameObject player; // 玩家对象
    private float attackCooldownTimer; // 攻击冷却计时器
    public int health = 5; // 敌人的初始血量为 5

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // 检测玩家是否在攻击范围内
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            ChasePlayer();
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

    private void ChasePlayer()
    {
        // 计算方向
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        // 实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = (player.transform.position - firePoint.position).normalized; // 指向玩家
        rb.velocity = shootDirection * bulletSpeed; // 设置子弹速度
    }
    // 添加受伤函数
    public void TakeDamage(int damage)
    {
        health -= damage; // 减少血量

        if (health <= 0)
        {
            Die(); // 血量为 0，调用死亡函数
        }
    }

    // 添加死亡函数
    private void Die()
    {
        // 可以在这里添加死亡动画、音效等
        Destroy(gameObject); // 销毁敌人对象
    }
}