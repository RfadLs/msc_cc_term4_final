using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // 引用 Pause Panel
    public GameObject savePanel; // 引用 Save Panel
    public GameObject loadPanel; // 引用 Load Panel
    public Button continueButton; // 继续按钮
    public Button saveButton; // 保存按钮
    public Button loadButton; // 加载按钮
    public Button quitButton; // 退出按钮
    public TextMeshProUGUI[] saveSlotTexts; // 存档点文本数组

    private bool isPaused = false; // 是否处于暂停状态
    private int selectedButtonIndex = 0; // 当前选择的按钮索引
    private int selectedSaveSlotIndex = 0; // 当前选择的存档槽索引
    private Button[] buttons; // 存储按钮的数组

    void Start()
    {
        // 初始化按钮数组
        buttons = new Button[] { continueButton, saveButton, loadButton, quitButton };
        SetButtonSelected(0); // 默认选择第一个按钮
        LoadSaveSlots(); // 加载存档点数据
    }

    void Update()
    {
        // 检测按下 Esc 键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // 如果已暂停，则恢复游戏
            }
            else
            {
                PauseGame(); // 如果未暂停，则暂停游戏
            }
        }

        if (isPaused)
        {
            HandleKeyboardInput();
        }
    }

    void HandleKeyboardInput()
    {
        if (savePanel.activeSelf || loadPanel.activeSelf)
        {
            // 在 Save 或 Load Panel 中，检测上下方向键来改变存档槽选择
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedSaveSlotIndex = (selectedSaveSlotIndex - 1 + saveSlotTexts.Length) % saveSlotTexts.Length;
                HighlightSaveSlot(selectedSaveSlotIndex);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedSaveSlotIndex = (selectedSaveSlotIndex + 1) % saveSlotTexts.Length;
                HighlightSaveSlot(selectedSaveSlotIndex);
            }

            // 检测按下 Space 键确认保存或加载
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (savePanel.activeSelf)
                {
                    SaveGame(selectedSaveSlotIndex); // 保存游戏
                }
                else if (loadPanel.activeSelf)
                {
                    LoadGame(selectedSaveSlotIndex); // 加载游戏
                }
            }
        }
        else
        {
            // 在 Pause Panel 中，检测上下方向键来改变按钮选择
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
                SetButtonSelected(selectedButtonIndex);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
                SetButtonSelected(selectedButtonIndex);
            }

            // 检测按下 Space 键确认按钮选择
            if (Input.GetKeyDown(KeyCode.Space))
            {
                buttons[selectedButtonIndex].onClick.Invoke();
            }
        }
    }

    void SetButtonSelected(int index)
    {


        // 高亮选中的按钮
        buttons[index].Select();
    }

    void HighlightSaveSlot(int index)
    {
        // 更新存档槽的高亮显示
        for (int i = 0; i < saveSlotTexts.Length; i++)
        {
            saveSlotTexts[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); // 隐藏 Pause Panel
        savePanel.SetActive(false); // 确保 Save Panel 也隐藏
        loadPanel.SetActive(false); // 确保 Load Panel 也隐藏
        Time.timeScale = 1f; // 恢复游戏时间
        isPaused = false; // 更新暂停状态
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true); // 显示 Pause Panel
        Time.timeScale = 0f; // 暂停游戏时间
        isPaused = true; // 更新暂停状态
        SetButtonSelected(0); // 默认选择第一个按钮
    }

    public void OpenSavePanel()
    {
        savePanel.SetActive(true); // 显示 Save Panel
        pausePanel.SetActive(false); // 隐藏 Pause Panel
        selectedSaveSlotIndex = 0; // 默认选择第一个存档槽
        HighlightSaveSlot(selectedSaveSlotIndex); // 更新存档槽的高亮显示
    }

    public void OpenLoadPanel()
    {
        loadPanel.SetActive(true); // 显示 Load Panel
        pausePanel.SetActive(false); // 隐藏 Pause Panel
        selectedSaveSlotIndex = 0; // 默认选择第一个存档槽
        HighlightSaveSlot(selectedSaveSlotIndex); // 更新存档槽的高亮显示
    }

    public void CloseSaveLoadPanel()
    {
        savePanel.SetActive(false); // 隐藏 Save Panel
        loadPanel.SetActive(false); // 隐藏 Load Panel
        pausePanel.SetActive(true); // 显示 Pause Panel
    }

    public void SaveGame(int slotIndex)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string saveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        PlayerPrefs.SetString($"SaveSlot_{slotIndex}_Scene", sceneName);
        PlayerPrefs.SetString($"SaveSlot_{slotIndex}_Time", saveTime);
        PlayerPrefs.Save();
        LoadSaveSlots(); // 更新存档点显示
    }

    public void LoadGame(int slotIndex)
    {
        if (PlayerPrefs.HasKey($"SaveSlot_{slotIndex}_Scene"))
        {
            string sceneName = PlayerPrefs.GetString($"SaveSlot_{slotIndex}_Scene");
            Time.timeScale = 1f; // 恢复游戏时间
            SceneManager.LoadScene(sceneName); // 加载存档场景
        }
        else
        {
            Debug.Log("No saved game found in this slot!");
        }
    }

    public void LoadSaveSlots()
    {
        // 加载存档点数据并显示
        for (int i = 0; i < saveSlotTexts.Length; i++)
        {
            if (PlayerPrefs.HasKey($"SaveSlot_{i}_Scene"))
            {
                string sceneName = PlayerPrefs.GetString($"SaveSlot_{i}_Scene");
                string saveTime = PlayerPrefs.GetString($"SaveSlot_{i}_Time");
                saveSlotTexts[i].text = $"Slot {i + 1}: {sceneName} - {saveTime}";
            }
            else
            {
                saveSlotTexts[i].text = $"Slot {i + 1}: Empty";
            }
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

}
