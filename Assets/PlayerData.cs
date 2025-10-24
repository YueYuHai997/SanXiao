using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class PlayerData
{
    public float MaxHeal;
    public float CurrentHeal;
    
    public float MaxEnergy;
    public float CurrentEnergy;
    
    public int MaxtLevel;
    public int CurrentLevel;
    
    public string NikeName;
    public string Description;
    public string ImagePath;
    
    public IconType FirstIconType;
    public IconType SecondIconType;

    // 局外升级相关 需要做数据持久化的内容
    // 用户自定义规则
    public int customRule_Level;
    public Rule magicalArtifact;
    public int customRule_X;
    public int customRule_Y;
    public int customRule_Count;
    
}

[Play("Maike")]
public class MaiKe : PlayerData
{
    public MaiKe()
    {
        MaxHeal = 100;
        CurrentHeal = 100;

        MaxEnergy = 100;
        CurrentEnergy = 0;

        MaxtLevel = 10;
        CurrentLevel = 1;

        NikeName = "麦克";
        ImagePath = " ";

        FirstIconType = IconType.Attack;
        SecondIconType = IconType.Defend;
    }
}

[Play("Mercy")]
public class Mercy : PlayerData
{
    public Mercy()
    {
        MaxHeal = 100;
        CurrentHeal = 100;

        MaxEnergy = 100;
        CurrentEnergy = 0;

        MaxtLevel = 10;
        CurrentLevel = 1;

        NikeName = "天使";
        ImagePath = " ";
        
        FirstIconType = IconType.Buffer;
        SecondIconType = IconType.Skill;
    }
}


public class PlayFactor<T>
{
    private readonly IDictionary<string, Type> products;

    public PlayFactor()
    {
        products = Assembly.GetExecutingAssembly().ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetCustomAttributes<PlayAttribute>(), (t, a) => new { t, a.Value })
            .ToDictionary(ta => ta.Value.ToLower(), ta => ta.t);
    }

    public List<T> GetAllPlayer()
    {
        List<T> res = new List<T>();

        foreach (var item in products)
        {
            res.Add((T)Activator.CreateInstance(item.Value));
        }
        return res;
    }

    public T GetPlayer(string Value)
    {
        if (products.TryGetValue(Value.ToLower(), out Type productType))
        {
            return (T)Activator.CreateInstance(productType);
        }

        throw new ArgumentException($"{Value} 未找到");
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)] //特性约束，约束特性作用域
public class PlayAttribute : Attribute
{
    public string Value { get; }

    public PlayAttribute(string value)
    {
        Value = value;
    }
}