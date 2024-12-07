using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveLoadPanel : MonoBehaviour
{
    public TextMeshProUGUI[] saveSlotTexts; // 存档点文本数组
    private int selectedSaveSlotIndex = 0; // 当前选择的存档槽索引

    void Start()
    {
        LoadSaveSlots(); // 加载存档点数据
    }

    void Update()
    {
        HandleSaveLoadInput();
    }

    void HandleSaveLoadInput()
    {
        // 检测上下方向键来改变存档槽选择
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
            // 这里可以自定义按钮逻辑，比如保存或加载
            SaveGame(selectedSaveSlotIndex); // 或者 LoadGame(selectedSaveSlotIndex)
        }
    }

    void HighlightSaveSlot(int index)
    {
        // 更新存档槽的高亮显示
        for (int i = 0; i < saveSlotTexts.Length; i++)
        {
            saveSlotTexts[i].color = (i == index) ? Color.yellow : Color.white;
        }
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
}
