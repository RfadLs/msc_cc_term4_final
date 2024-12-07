using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 用于屏幕变红效果

public class Bullet : MonoBehaviour
{
    public float lifeTime = 8f; // 子弹的生命周期
    public float trackingSpeed = 10f; // 反弹速度
    private Transform player;
    private bool isReflected = false; // 是否已经被反弹
    private CameraShake cameraShake;
    private AudioSource audioSource;  // 用于播放打击音效
    public AudioClip hitSound;  // 打击音效

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraShake = Camera.main.GetComponent<CameraShake>();  // 获取相机上的 CameraShake 组件
        audioSource = GetComponent<AudioSource>();  // 获取 AudioSource 组件
        Destroy(gameObject, lifeTime); // 设置生命周期
    }

    void Update()
    {
        // 检测玩家按下 E 键，并且子弹还没有被反弹
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.position) < 2f && !isReflected)
        {
            StartCoroutine(ReflectToEnemy());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isReflected)
        {
            // 子弹未被反弹，检测与玩家的碰撞
            if (collision.gameObject.CompareTag("Player"))
            {

                // 获取玩家的 Health System
                HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.TakeDamage();  // 减少玩家的生命值
                }

                // 查找 BulletReflectCounter 脚本
                BulletReflectCounter comboManager = collision.gameObject.GetComponent<BulletReflectCounter>();
                if (comboManager != null)
                {
                    comboManager.ResetCombo();  // 重置 Combo 计数
                }

                Destroy(gameObject);  // 销毁子弹
            }
        }
        else
        {
            // 子弹已被反弹，检测与敌人的碰撞
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // 获取敌人的 EnemyAI 脚本
                EnemyAI enemyAI = collision.gameObject.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    Debug.Log("Boss hit by bullet!");
                    enemyAI.TakeDamage(1); // 对敌人造成 1 点伤害
                }

                Destroy(gameObject); // 销毁子弹
            }
        }
    }

    private IEnumerator ReflectToEnemy()
    {
        isReflected = true; // 标记子弹已经被反弹

        // 更改子弹的 Layer，避免与玩家再次碰撞
        gameObject.layer = LayerMask.NameToLayer("ReflectedBullet");

        // 忽略子弹与玩家的碰撞
        Collider2D bulletCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider, true);
        }

        // 获取敌人的位置
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy"); // 确保敌人有 "Enemy" 标签
        if (enemy != null)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = direction * trackingSpeed; // 设置反弹速度

            // 播放打击音效
            PlayHitSound();

            // 触发相机抖动
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake(0.2f, 0.2f)); // 设置持续时间和震动强度
            }

            // 等待一段时间后销毁子弹
            yield return new WaitForSeconds(1f);
            Destroy(gameObject); // 销毁子弹
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);  // 播放一次打击音效
        }
    }
}
