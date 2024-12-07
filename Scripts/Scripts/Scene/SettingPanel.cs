using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour

{
    public GameObject settingsPanel; // 引用设置面板

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // 显示设置面板
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // 隐藏设置面板
    }
}
