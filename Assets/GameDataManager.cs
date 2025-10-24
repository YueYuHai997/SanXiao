using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static readonly Lazy<GameDataManager> _instance = new(() => new GameDataManager());
    public static GameDataManager Instance = _instance.Value;
    
    /// <summary> X轴 分布 </summary>
    public int ResolutionX;
    
    /// <summary> Y轴 分布 </summary>
    public int ResolutionY;
    
    /// <summary> 遗物列表 </summary>
    public List<HolyArticle> HolyArticles;

    /// <summary> 金币 </summary>
    public int GoldCoin;
    
    /// <summary> 当前进度 </summary>
    public int Level;
    
    /// <summary> 人物属性 </summary>
    public PlayerData PlayerData;
    

}