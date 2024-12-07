using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // 单例模式，确保只有一个音乐管理器
    private AudioSource audioSource;

    void Awake()
    {
        // 确保这是唯一的 MusicManager 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁此对象
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // 方法：更改背景音乐
    public void ChangeMusic(AudioClip newMusic)
    {
        if (audioSource.clip == newMusic) return; // 如果当前正在播放相同的音乐，则不切换

        audioSource.Stop(); // 停止当前音乐
        audioSource.clip = newMusic; // 更换为新的音乐
        audioSource.Play(); // 播放新的音乐
    }
}
