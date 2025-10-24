public abstract class Rule
{
    public string RuleName;
    public IconType iconType;
    public IconType[][] pattern;
    public bool AllDir;
    
}

/// <summary>
/// 神圣之剑
/// </summary>
public class SacredSword : Rule
{
    /*    A
     *   AAA
     *    A
     *    A
     */

    public SacredSword()
    {
        RuleName = "神圣之剑";
        iconType = IconType.Attack;
        AllDir = false;

        // 不知道为什么 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { IconType.None, IconType.None, iconType, IconType.None },
            new IconType[] { iconType, iconType, iconType, iconType },
            new IconType[] { IconType.None, IconType.None, iconType, IconType.None },
        };
    }
}


/// <summary>
/// 两面包夹
/// </summary>
public class TwoBread : Rule
{
    /*
     *   AAA
     *   000
     *   AAA
     */

    public TwoBread()
    {
        RuleName = "两面包夹";
        iconType = IconType.Attack;
        AllDir = false;

        //  声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { iconType, IconType.None, iconType, },
            new IconType[] { iconType, IconType.None, iconType, },
            new IconType[] { iconType, IconType.None, iconType, },
        };
    }
}

/// <summary>
/// 组合防御进攻
/// </summary>
public class CombinedDefenseOffense : Rule
{
    /*
     *   DDD
     *   AAA
     *   000
     */

    public CombinedDefenseOffense()
    {
        RuleName = "组合防御进攻";
        iconType = IconType.Attack;
        AllDir = false;

        //  声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { IconType.None, iconType, IconType.Defend, },
            new IconType[] { IconType.None, iconType, IconType.Defend, },
            new IconType[] { IconType.None, iconType, IconType.Defend, },
        };
    }
}


/// <summary>
/// 完美防御
/// </summary>
public class PerfectDefense : Rule
{
    /* 111
     * 101
     * 111
     */
    public PerfectDefense()
    {
        RuleName = "完美防御";
        iconType = IconType.Defend;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { iconType, iconType, iconType },
            new IconType[] { iconType, IconType.None, iconType },
            new IconType[] { iconType, iconType, iconType },
        };
    }
}

/// <summary>
/// 绝对防御
/// </summary>
public class AbsoluteDefense : Rule
{
    /* DDDDD
     */
    public AbsoluteDefense()
    {
        RuleName = "绝对防御";
        iconType = IconType.Defend;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[5][]
        {
            new IconType[] { iconType },
            new IconType[] { iconType },
            new IconType[] { iconType },
            new IconType[] { iconType },
            new IconType[] { iconType },
        };
    }
}

/// <summary>
/// 舍命进攻
/// </summary>
public class SacrificeAttack : Rule
{
    /* A
     * A
     * A
     * A
     * A
     */
    public SacrificeAttack()
    {
        RuleName = "舍命进攻";
        iconType = IconType.Defend;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[1][]
        {
            new IconType[] { iconType, iconType, iconType, iconType, iconType }
        };
    }
}

/// <summary>
/// 闪电战
/// </summary>
public class LightningAttack : Rule
{
    /* 
     * AAA
     * A0A
     * A0A
     * A0A
     */
    public LightningAttack()
    {
        RuleName = "闪电战";
        iconType = IconType.Attack;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { iconType, iconType, iconType, iconType, iconType },
            new IconType[] { iconType, IconType.None, IconType.None, IconType.None, IconType.None },
            new IconType[] { iconType, iconType, iconType, iconType, iconType }
        };
    }
}

/// <summary>
/// 弓箭 
/// </summary>
public class Sagittarius : Rule
{
    /* 010
     * 111
     * 010
     */
    public Sagittarius()
    {
        RuleName = "弓箭";
        iconType = IconType.Skill;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { IconType.None, iconType, IconType.None,  },
            new IconType[] { iconType,      iconType, iconType,       },
            new IconType[] { IconType.None, iconType, IconType.None,  }
        };
    }
}
/// <summary>
/// 白羊座、金牛座、双子座、巨蟹座、狮子座、处女座、天秤座、天蝎座、射手座、摩羯座、水瓶座、双鱼座
/// </summary>
public class Constellation : Rule
{
    /* 11111
     * 10101
     * 11111
     * 00100
     * 00100
     */
    
    /* 01010
     * 10001
     * 11111
     * 10001
     * 11111
     */
    
    /* 11111
     * 01010
     * 01010
     * 01010
     * 11111
     */
    
    /* 01111
     * 10100
     * 01100
     * 01010
     * 11110
     */
    
    /* 01111
     * 10100
     * 01100
     * 01010
     * 11110
     */
    
    
    public Constellation()
    {
        RuleName = "白羊座";
        iconType = IconType.Skill;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[3][]
        {
            new IconType[] { IconType.None, iconType, IconType.None,  },
            new IconType[] { iconType,      iconType, iconType,       },
            new IconType[] { IconType.None, iconType, IconType.None,  }
        };
    }
}