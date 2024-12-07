using UnityEngine;

public class QuitGame1 : MonoBehaviour
{
    public void QuitGame()
    {
        // 在编辑器中会打印一条信息，方便调试
        Debug.Log("Quit Game!");
        // 退出游戏（仅在构建后的游戏中生效）
        Application.Quit();
    }
}


