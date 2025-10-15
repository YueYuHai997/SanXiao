using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;

public enum IconType
{
    None,
    Attack,
    Defend,
    Skill,
    Buffer,
}

//Attack 3、4、5、6
//Defend 3、4、5、6
//Skill  3、4、5、6
//Buffer 3、4、5、6

public class Manager : MonoBehaviour
{
    public IconItem Prefab;

    [Range(1, 10)] public int Resolution_X;

    [Range(1, 10)] public int Resolution_Y;

    private IconItem[][] Icons;

    private HashSet<IconItem> DestroyIcons = new();
    private Dictionary<IconItem, int> FallIcons = new();

    private IconItem Switch_1;
    private IconItem Switch_2;

    public Image AttackRange;
    public Image DefendRange;
    public Image SkillRange;
    public Image BufferRange;

    public Text AttackText;
    public Text DefendText;
    public Text SkillText;
    public Text BufferText;

    public int AttackValue;
    public int DefendValue;
    public int SkillValue;
    public int BufferValue;
    
    public RollingNumbers attackRollingNumbers;
    public RollingNumbers defendRollingNumbers;
    public RollingNumbers skillRollingNumbers;
    public RollingNumbers bufferRollingNumbers;


    private Pool<IconItem> IconItems;

    private Rule _rule;
    private MatchingRule Rules;
    
    private void Awake()
    {
        Icons = new IconItem[Resolution_X][];
        IconItems = new Pool<IconItem>(Prefab, this.transform, Resolution_X * Resolution_Y);
        
        for (int i = 0; i < Resolution_X; i++)
        {
            Icons[i] = new IconItem[Resolution_Y]; // 为每一行初始化第二维数组
            for (int j = 0; j < Resolution_Y; j++)
            {
                int Ti, Tj;
                Ti = i;
                Tj = j;
                IconItems.GetObject((item) =>
                {
                    item.SetICon(RandomColor());
                    item.SetXY(Ti, Tj);
                    Icons[Ti][Tj] = item;
                });
                
                /*
                var item = GameObject.Instantiate<IconItem>(Prefab, this.transform);
                item.SetICon(RandomColor());
                item.SetXY(i, j);
                Icons[i][j] = item;
                */
                
            }
        }
        
        _rule = new PerfectDefense();
        Rules = new MatchingRule(_rule);
    }


    public async void DestroyIcon()
    {
        int DelayTime = 100;
        foreach (var item in DestroyIcons)
        {
            var destroyItem = item;
            destroyItem.Move(GetRange(destroyItem.iconType), () =>
            {
                attackRollingNumbers.SetNumber(AttackValue++);
                //AttackText.text = AttackValue.ToString();
                IconItems.DestObject(destroyItem);
                Icons[destroyItem.x][destroyItem.y] = null;
            });
            await Task.Delay(DelayTime);
            DelayTime = DelayTime > 10 ? DelayTime - 5 : DelayTime;
        }

        DestroyIcons.Clear();
    }


    public Vector3 GetRange(IconType _type) => _type switch
    {
        IconType.Attack => AttackRange.transform.position,
        IconType.Defend => DefendRange.transform.position,
        IconType.Skill => SkillRange.transform.position,
        IconType.Buffer => BufferRange.transform.position,
    };

    public void FallIcon()
    {
        for (int i = 0; i < Icons.Length; i++)
        {
            for (int j = 0; j < Icons[i].Length; j++)
            {
                if (Icons[i][j])
                {
                    int minj = j;
                    int k = j;
                    while (k >= 0)
                    {
                        if (!Icons[i][k])
                        {
                            minj = k;
                        }
                        k--;
                    }
                    if (minj != j)
                    {
                        Icons[i][j].Fall(minj);
                        Icons[i][minj] = Icons[i][j];
                        Icons[i][j] = null;
                    }
                }
            }
        }
    }


    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            /*
            Jugde();
            Debug.Log("Exec");
            DestroyIcon();
            */

            var items = Rules.Judge(Icons);
            Debug.Log(items is null);
        
            for (int i = 0; i < items.Count; i++)
            {
                items[i].HeightLight();
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < Icons.Length; i++)
            {
                for (int j = 0; j < Icons[0].Length; j++)
                {
                    Icons[i][j].HeightResume();
                }
            }
            //FallIcon();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Resume();
        }


        if (Input.GetMouseButtonDown(0))
        {
            var a = GetClickedImage();
            Debug.Log($"{a?.x}:{a?.y}");

            if (Switch_1)
            {
                Switch_2 = a;

                Icons[Switch_1.x][Switch_1.y] = Switch_2;
                Icons[Switch_2.x][Switch_2.y] = Switch_1;

                int x = Switch_2.x;
                int y = Switch_2.y;

                Switch_2.SetXY(Switch_1.x, Switch_1.y);
                Switch_1.SetXY(x, y);

                Switch_2 = null;
                Switch_1 = null;
            }
            else
            {
                Switch_1 = a;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Switch_1 = null;
        }
    }

    public void Resume()
    {
        for (int i = 0; i < Icons.Length; i++)
        {
            for (int j = 0; j < Icons[i].Length; j++)
            {
                if (Icons[i][j])
                {
                    /*
                    item.SetICon(RandomColor());
                    Icons[i][j].SetResume();
                    */
                }
                else
                {
                    int Ti, Tj;
                    Ti = i;
                    Tj = j;
                    IconItems.GetObject((item) =>
                    {
                        item.SetICon(RandomColor());
                        item.SetXY(Ti, Tj);
                        Icons[Ti][Tj] = item;
                    });
                    
                    /*
                    var item = GameObject.Instantiate<IconItem>(Prefab, this.transform);
                    item.SetICon(RandomColor());
                    item.SetXY(i, j);
                    Icons[i][j] = item;
                    */
                }
            }
        }
    }


    /// <summary>
    /// 获取鼠标点击位置的Image组件
    /// </summary>
    /// <returns>点击到的Image，没有则返回null</returns>
    private IconItem GetClickedImage()
    {
        // 检查是否点击在UI上
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 创建射线
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            // 射线检测
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerEventData, results);

            // 遍历检测结果
            foreach (RaycastResult result in results)
            {
                // 如果检测到Image组件
                if (result.gameObject.TryGetComponent<IconItem>(out IconItem item))
                {
                    return item;
                }
            }
        }

        return null;
    }


    private IconType RandomColor()
    {
        float r = Random.Range(0, 1f);
        if (r < 0.25f)
        {
            return IconType.Defend;
        }
        else if (r < 0.5f)
        {
            return IconType.Skill;
        }
        else if (r < 0.75f)
        {
            return IconType.Buffer;
        }
        else if (r < 1f)
        {
            return IconType.Attack;
        }

        return IconType.Buffer;
    }


    public void Jugde()
    {
        //判断Y / 列
        for (int i = 0; i < Resolution_X; i++)
        {
            var X_First = Icons[i][0].iconType;
            var XCount = 1;
            for (int j = 1; j < Resolution_Y; j++)
            {
                if (X_First == Icons[i][j].iconType)
                {
                    XCount++;
                }
                else
                {
                    if (XCount >= 3)
                    {
                        DesTroyItemX(i, j - 1, XCount);
                    }

                    X_First = Icons[i][j].iconType;
                    XCount = 1;
                }
            }

            if (XCount >= 3)
            {
                DesTroyItemX(i, Resolution_Y - 1, XCount);
            }
        }

        //判断X / 行
        for (int i = 0; i < Resolution_Y; i++)
        {
            var Y_First = Icons[0][i].iconType;
            var YCount = 1;
            for (int j = 1; j < Resolution_X; j++)
            {
                if (Y_First == Icons[j][i].iconType)
                {
                    YCount++;
                }
                else
                {
                    if (YCount >= 3)
                    {
                        DesTroyItemY(j - 1, i, YCount);
                    }

                    Y_First = Icons[j][i].iconType;
                    YCount = 1;
                }
            }

            if (YCount >= 3)
            {
                DesTroyItemY(Resolution_X - 1, i, YCount);
            }
        }

        //倾斜 向左上
        for (int k = 0; k < Resolution_X - 2; k++)
        {
            int j = 0;
            int i = k;
            var Up_First = Icons[i++][j++].iconType;
            var UpCount = 1;

            while (i < Resolution_X && j < Resolution_Y)
            {
                if (Up_First == Icons[i][j].iconType)
                {
                    UpCount++;
                }
                else
                {
                    if (UpCount >= 3)
                    {
                        DesTroyItemUp(i, j, UpCount);
                    }

                    Up_First = Icons[i][j].iconType;
                    UpCount = 1;
                }

                i++;
                j++;
            }

            if (UpCount >= 3)
            {
                DesTroyItemUp(i, j, UpCount);
            }
        }

        for (int k = 1; k < Resolution_Y - 2; k++)
        {
            int j = k;
            int i = 0;
            var Up_First = Icons[i++][j++].iconType;
            var UpCount = 1;

            while (i < Resolution_X && j < Resolution_Y)
            {
                if (Up_First == Icons[i][j].iconType)
                {
                    UpCount++;
                }
                else
                {
                    if (UpCount >= 3)
                    {
                        DesTroyItemUp(i, j, UpCount);
                    }

                    Up_First = Icons[i][j].iconType;
                    UpCount = 1;
                }

                i++;
                j++;
            }

            if (UpCount >= 3)
            {
                DesTroyItemUp(i, j, UpCount);
            }
        }


        //倾斜 向右下
        for (int k = 0; k < Resolution_X - 2; k++)
        {
            int i = k;
            int j = Resolution_Y - 1;
            var Up_First = Icons[i++][j--].iconType;
            var UpCount = 1;

            while (i < Resolution_X && j >= 0)
            {
                if (Up_First == Icons[i][j].iconType)
                {
                    UpCount++;
                }
                else
                {
                    if (UpCount >= 3)
                    {
                        DesTroyItemDown(i, j, UpCount);
                    }

                    Up_First = Icons[i][j].iconType;
                    UpCount = 1;
                }

                i++;
                j--;
            }

            if (UpCount >= 3)
            {
                DesTroyItemDown(i, j, UpCount);
            }
        }

        for (int k = 2; k < Resolution_Y - 1; k++)
        {
            int j = k;
            int i = 0;
            var Up_First = Icons[i++][j--].iconType;
            var UpCount = 1;

            while (i < Resolution_X && j >= 0)
            {
                if (Up_First == Icons[i][j].iconType)
                {
                    UpCount++;
                }
                else
                {
                    if (UpCount >= 3)
                    {
                        DesTroyItemDown(i, j, UpCount);
                    }

                    Up_First = Icons[i][j].iconType;
                    UpCount = 1;
                }

                i++;
                j--;
            }

            if (UpCount >= 3)
            {
                DesTroyItemDown(i, j, UpCount);
            }
        }
    }

    private void DesTroyItemX(int x, int y, int num)
    {
//        Debug.Log($"{x}、{y}、{num}");
        for (int i = 0; i < num; i++)
        {
            Icons[x][y - i].SetDestroy();
            DestroyIcons.Add(Icons[x][y - i]);
        }
    }

    private void DesTroyItemY(int x, int y, int num)
    {
        //  Debug.Log($"{x}、{y}、{num}");
        for (int i = 0; i < num; i++)
        {
            Icons[x - i][y].SetDestroy();
            DestroyIcons.Add(Icons[x - i][y]);
        }
    }

    private void DesTroyItemUp(int x, int y, int num)
    {
        //   Debug.Log($"{x}、{y}、{num}");
        for (int i = 1; i <= num; i++)
        {
            Icons[x - i][y - i].SetDestroy();
            DestroyIcons.Add(Icons[x - i][y - i]);
        }
    }

    private void DesTroyItemDown(int x, int y, int num)
    {
        //   Debug.Log($"{x}、{y}、{num}");
        for (int i = 1; i <= num; i++)
        {
            Icons[x - i][y + i].SetDestroy();
            DestroyIcons.Add(Icons[x - i][y + i]);
        }
    }
}