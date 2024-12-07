using UnityEngine;

public class ChangeMusicTrigger : MonoBehaviour
{
    public AudioClip newMusic; // 拖入你想要播放的新音乐

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 检测玩家是否进入触发器
        {
            MusicManager.instance.ChangeMusic(newMusic); // 切换音乐
        }
    }
}
