using UnityEngine;

public class EnemytoPlayerZone : MonoBehaviour
{
    public EnemytoPlayer enemy; // 需要追逐的敌人对象
    public AudioClip chaseSound; // 追逐开始时的音效
    private AudioSource audioSource; // 音频播放器

    private void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.StartChase(); // 开始追逐

            // 播放音效
            PlayChaseSound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.StopChase(); // 当玩家离开触发器时，停止追逐
        }
    }

    private void PlayChaseSound()
    {
        if (chaseSound != null && audioSource != null)
        {
            audioSource.clip = chaseSound;
            audioSource.Play();
        }
    }
}

