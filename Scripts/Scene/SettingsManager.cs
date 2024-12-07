using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider; // 引用滑动条

    void Start()
    {
        // 设置滑动条的默认值
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // 更改全局音量
        AudioListener.volume = value;
    }
}
