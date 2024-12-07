using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    // 播放音效的方法
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // 播放音效
        }
    }
}
