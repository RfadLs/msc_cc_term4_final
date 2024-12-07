using UnityEngine;
using TMPro;
using UnityEngine.UI; // 导入 UI 命名空间

public class ItemClue : MonoBehaviour
{
    public GameObject cluePanel; // 用于显示线索的面板
    public TextMeshProUGUI clueNameText; // 显示线索名称的文本
    public TextMeshProUGUI clueDescriptionText; // 显示线索描述的文本
    public Image clueImage; // 显示线索图像

    public string clueName; // 线索的名称
    public string clueDescription; // 线索的描述
    public Sprite clueSprite; // 线索的图像

    public GameObject magnifyingGlassIcon; // 放大镜图标
    public AudioSource audioSource; // 用于播放音效的 AudioSource 组件
    public AudioClip clueSound; // 按下 F 键时的音效

    private bool isPlayerInRange = false; // 玩家是否在范围内
    private bool isCluePanelActive = false; // 线索面板是否处于激活状态

    void Start()
    {
        // 隐藏放大镜图标和线索面板
        if (magnifyingGlassIcon != null)
            magnifyingGlassIcon.SetActive(false);

        if (cluePanel != null)
            cluePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (magnifyingGlassIcon != null)
                magnifyingGlassIcon.SetActive(true); // 显示放大镜图标
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (magnifyingGlassIcon != null)
                magnifyingGlassIcon.SetActive(false); // 隐藏放大镜图标

            if (cluePanel != null)
                cluePanel.SetActive(false); // 隐藏线索面板

            isCluePanelActive = false; // 重置线索面板状态
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isCluePanelActive)
            {
                HideClue(); // 隐藏线索面板
            }
            else
            {
                ShowClue(); // 显示线索面板并播放音效
            }
        }
    }

    void ShowClue()
    {
        if (cluePanel != null)
        {
            cluePanel.SetActive(true); // 显示线索面板
            clueNameText.text = clueName; // 设置线索名称
            clueDescriptionText.text = clueDescription; // 设置线索描述
            clueImage.sprite = clueSprite; // 设置线索图像
            isCluePanelActive = true; // 更新面板状态
            PlaySound(); // 仅在打开面板时播放音效
        }
    }

    void HideClue()
    {
        if (cluePanel != null)
        {
            cluePanel.SetActive(false); // 隐藏线索面板
            isCluePanelActive = false; // 更新面板状态
        }
    }

    void PlaySound()
    {
        if (audioSource != null && clueSound != null)
        {
            audioSource.PlayOneShot(clueSound); // 播放音效
        }
    }
}
