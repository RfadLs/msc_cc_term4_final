using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家角色的Transform
    public Vector3 offset; // 相机与玩家的偏移量
    public float smoothSpeed = 0.125f; // 平滑跟随的速度
    public float minX = -10f; // 摄像机左边界
    public float maxX = 10f;  // 摄像机右边界
    private float initialY; // 初始Y轴位置


    void Start()
    {
        // 记录相机的初始 Y 轴位置
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
        // 计算期望的相机位置，仅在 X 轴上跟随玩家，Y 轴保持不变
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, initialY, player.position.z + offset.z);


        // 限制相机的X轴位置
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition = new Vector3(clampedX, desiredPosition.y, desiredPosition.z);

        // 平滑地移动相机
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 更新相机位置
        transform.position = smoothedPosition;
    }
}
