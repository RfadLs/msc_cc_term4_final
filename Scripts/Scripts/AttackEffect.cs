using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject effectPrefab; // 粒子特效的Prefab
    public Transform effectSpawnPoint; // 特效生成的位置
    public float effectDuration = 2f; // 特效持续时间

    // 调用以触发特效
    public void TriggerEffect()
    {
        if (effectPrefab != null && effectSpawnPoint != null)
        {
            GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
            Destroy(effectInstance, effectDuration); // 自动销毁特效
        }
    }
}
