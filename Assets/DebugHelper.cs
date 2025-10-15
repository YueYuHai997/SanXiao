using UnityEngine;
using Sirenix.OdinInspector; // 引入Odin命名空间

[ExecuteInEditMode]
public class DebugHelper : MonoBehaviour
{
    public Manager _Manager;
    
    [Header("游戏速度控制")]
    [Range(0f, 5f)] public float gameSpeed = 1f;

    [Header("调试设置")]
    public bool showDebugLogs = true;

    private float _originalTimeScale;

    private void Awake()
    {
        _originalTimeScale = Time.timeScale;
    }

    private void Update()
    {
        // 同步游戏速度到时间缩放
        if (Mathf.Abs(Time.timeScale - gameSpeed) > 0.01f)
        {
            Time.timeScale = gameSpeed;
            Time.fixedDeltaTime = 0.02f * gameSpeed; // 保持物理更新与速度同步

            if (showDebugLogs)
            {
                Debug.Log($"游戏速度已调整为: {gameSpeed}x");
            }
        }
    }

    // 打印123按钮（直接显示在Inspector中）
    [Button("消除", ButtonSizes.Medium), PropertyOrder(100)]
    public void Func1()
    {
        _Manager.Jugde();
        _Manager.DestroyIcon();
    }
    // 打印123按钮（直接显示在Inspector中）
    [Button("下落", ButtonSizes.Medium), PropertyOrder(100)]
    public void Func2()
    {
        _Manager.FallIcon();
    }
    // 打印123按钮（直接显示在Inspector中）
    [Button("补充", ButtonSizes.Medium), PropertyOrder(100)]
    public void Func3()
    {
        _Manager.Resume();
    }

    // 重置游戏速度按钮
    [Button("重置游戏速度", ButtonSizes.Medium, ButtonStyle.FoldoutButton), PropertyOrder(101)]
    public void ResetGameSpeed()
    {
        gameSpeed = _originalTimeScale;
        if (showDebugLogs)
        {
            Debug.Log("游戏速度已重置为默认值");
        }
    }
    
}