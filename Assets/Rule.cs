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
        RuleName = "组合进攻";
        iconType = IconType.Defend;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[5][]
        {
            new IconType[] { iconType},
            new IconType[] { iconType},
            new IconType[] { iconType},
            new IconType[] { iconType},
            new IconType[] { iconType},
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
    public AbsoluteDefense()
    {
        RuleName = "组合进攻";
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
public class SacrificeAttack : Rule
{
    /* 000
     * AAA
     * A0A
     * A0A
     * A0A
     */
    public AbsoluteDefense()
    {
        RuleName = "组合进攻";
        iconType = IconType.Defend;
        AllDir = false;

        // 声明的时候需要声明成向右旋转90度的。
        pattern = new IconType[1][]
        {
            new IconType[] { iconType, iconType, iconType, iconType, iconType }
        };
    }
}
