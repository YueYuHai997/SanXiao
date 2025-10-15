using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class RollingNumbers : MonoBehaviour
{
    /// <summary> 滚动数字精灵图 </summary>
    public Sprite RollingNumbersSprite;

    /// <summary> 数字数量 </summary>
    public int NumberSize = 11;

    /// <summary> 横向间距 </summary>
    public float NumberHorizontallyInterval = 0;

    /// <summary> 预制体 </summary>
    public RollingItem RollingItemPrefab;

    /// <summary> 初始化位数 </summary>
    public int InitialBitCount = 4;

    //纵向间距
    private float Step;

    //横向间距
    private float InterVal;

    //位数集合
    private List<RollingItem> RollingItems = new();

    //当前数字
    private int num = 0;

    private void Awake()
    {
        Step = (RollingNumbersSprite.bounds.size.y * 100) / NumberSize;
        InterVal = RollingItemPrefab.GetComponent<RectTransform>().sizeDelta.x + NumberHorizontallyInterval;
        for (int i = 0; i < InitialBitCount; i++)
        {
            var op = GameObject.Instantiate(RollingItemPrefab, this.transform);
            op.Init(Step, RollingNumbersSprite);
            RollingItems.Add(op);
        }

        JudgeInteval();
    }

    /// <summary>
    /// 添加位数
    /// </summary>
    private void AddNumber()
    {
        var op = GameObject.Instantiate(RollingItemPrefab, this.transform);
        op.Init(Step, RollingNumbersSprite);
        RollingItems.Add(op);
        JudgeInteval();
    }

    /// <summary>
    /// 调整位置
    /// </summary>
    private void JudgeInteval()
    {
        for (int i = 0; i < RollingItems.Count; i++)
        {
            RollingItems[i].transform.localPosition = new Vector2((RollingItems.Count - i) * InterVal, 0);
        }
    }


    private List<int> nums = new();

    /// <summary>
    /// 设置数字
    /// </summary>
    /// <param name="_num"></param>
    public void SetNumber(int _num)
    {
        num = _num;
        nums.Clear();
        while (num >= 1)
        {
            nums.Add(num % 10);
            num /= 10;
        }

        while (nums.Count > RollingItems.Count)
        {
            AddNumber();
        }

        for (int i = 0; i < nums.Count; i++)
        {
            RollingItems[i].SetNumberAnimation(nums[i]);
        }
    }

    /// <summary>
    /// 获取数字
    /// </summary>
    /// <returns></returns>
    public int GetNumber()
    {
        return num;
    }
    
    
}