using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed; // 水平移动速度
    public float dashSpeed; // Dash 速度
    public float dashDuration; // Dash 持续时间
    public float dashCooldown; // Dash 冷却时间

    public ParticleSystem dust; // 粒子效果
    private bool isDashing; // 是否在冲刺中
    private float dashCooldownTimer; // 冲刺冷却计时器

    private bool facingRight = true; // 标记角色是否朝向右边

    // 引用 Animator
    private Animator animator;

    // 音效相关
    private AudioSource audioSource; // 引用 AudioSource
    public AudioClip walkSound; // 行走音效文件

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }


        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkSound; // 将行走音效赋值到 AudioSource
    }

    void Update()
    {
        // 获取水平输入
        float xInput = Input.GetAxis("Horizontal");

        // 水平移动逻辑
        float movementSpeed = Mathf.Abs(xInput * speed); // 计算水平速度
        body.velocity = new Vector2(xInput * speed, body.velocity.y); // 更新X轴速度，保持Y轴速度

        // 播放行走音效
        HandleWalkSound(movementSpeed);
        // 将 Speed 参数传递给 Animator
        animator.SetFloat("Speed", movementSpeed);

        // 检测角色水平翻转
        if (xInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (xInput < 0 && facingRight)
        {
            Flip();
        }

        // 冲刺输入检测
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            CreateDust();
            StartCoroutine(Dash());
        }

        // 如果在冲刺中，优先播放冲刺动画
        if (isDashing)
        {
            animator.SetBool("IsDashing", true);
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }

        // 更新 Dash 冷却计时器
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    public void SetControlEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

    // 播放和停止行走音效
    private void HandleWalkSound(float movementSpeed)
    {
        if (movementSpeed > 0.01f && !audioSource.isPlaying) // 当角色移动且没有播放音效时，播放音效
        {
            audioSource.Play();
        }
        else if (movementSpeed <= 0.01f && audioSource.isPlaying) // 当角色停止移动时停止音效
        {
            audioSource.Stop();
        }
    }

    private IEnumerator Dash()
    {
        float originalSpeed = speed; // 记录原始速度
        speed = dashSpeed; // 设置为 Dash 速度

        // 设置动画参数 IsDashing
        animator.SetBool("IsDashing", true);

        // Dash 持续时间
        yield return new WaitForSeconds(dashDuration);

        // 还原速度
        speed = originalSpeed;

        // 设置动画参数 IsDashing 为 false
        animator.SetBool("IsDashing", false);

        // 设置冷却时间
        dashCooldownTimer = dashCooldown;
    }

    // 水平翻转角色
    private void Flip()
    {
        facingRight = !facingRight; // 翻转角色的方向标志

        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // 反转 x 轴的缩放
        transform.localScale = theScale;
    }

    void CreateDust()
    {
        dust.Play();
    }
}
