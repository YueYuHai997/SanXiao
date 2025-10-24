using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 圣物
/// </summary>
public abstract class HolyArticle
{
    internal GameDataManager gameDataManager;
    public abstract void Activate();
}

/// <summary>
/// 
/// </summary>
public class Holy_1 : HolyArticle
{
    public Holy_1(GameDataManager _gameDataManager)
    {
        gameDataManager = _gameDataManager;
    }

    public override void Activate()
    {
        gameDataManager.ResolutionX += 1;
        gameDataManager.ResolutionY += 1;
    }
}