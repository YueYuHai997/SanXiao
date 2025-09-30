using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using DG.Tweening;

public class IconItem : MonoBehaviour
{
    public Color IconColor;

    //public Text text;
    public int x;
    public int y;

    public IconType iconType;


    public void Fall(int j)
    {
        y = j;
        this.transform.position = new Vector2(x * 110, y * 110);
        //text.text = $"x:{x}\ny:{y}";
    }


    public void SetDestroy()
    {
        // text.color = Color.red;;
    }

    public void SetResume()
    {
        //  text.color = Color.black;;
    }

    public void SetICon(IconType type)
    {
        this.GetComponent<Image>().sprite = Helper.GetColor(type);
        iconType = type;
    }

    private Vector3 P0;
    private Vector3 P1;
    private Vector3 P2;
    private Vector3 P3;

    public void Move(Vector3 Tag, Action action)
    {
        P0 = this.transform.position;
        P1 = P0 + Random.insideUnitSphere * 500f;
        P2 = Tag + Random.insideUnitSphere * 500f;
        P3 = Tag;

        var currentValue = 0f;
        DOTween.To(
            () => currentValue, // 读取当前值的回调
            x => currentValue = x, // 更新值的回调
            1f, // 目标值
            1f // 动画持续时间（秒）
        ).OnUpdate(() =>
        {
            MoveBZR3(currentValue);
        }).OnComplete(() =>
        {
            action?.Invoke();
            Destroy(this.gameObject);
        });
    }

    private void MoveBZR3(float t)
    {
        this.transform.position = P0 * Mathf.Pow(1 - t, 3) +
                                  3 * P1 * t * Mathf.Pow(1 - t, 2) +
                                  3 * P2 * t * t * (1 - t) +
                                  P3 * Mathf.Pow(t, 3);
    }

    public void SetXY(int _x, int _y)
    {
        x = _x;
        y = _y;
        this.transform.position = new Vector2(x * 110, y * 110);

        //text.text = $"x:{x}\ny:{y}";
    }


}