using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class RollingItem : MonoBehaviour
{
    
    private float NumberStep;
    private int number;
    private float FatherWidth;
    private float bias;
    public Image NumberImage;

    private void Awake()
    {
        FatherWidth = this.GetComponent<RectTransform>().sizeDelta.y;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Init(float numberStep,Sprite sprite)
    {
        NumberStep = numberStep;
        bias = (sprite.bounds.size.y * 100 - FatherWidth) / 2;
        NumberImage.sprite = sprite;
        NumberImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(sprite.bounds.size.x * 100, sprite.bounds.size.y * 100);
        
        SetNumber(0);
    }
    
    public void SetNumber(int num)
    {
        number = num;
        float y = number * NumberStep - bias;
        NumberImage.transform.localPosition = new Vector2(0, y);
    }

    public void SetNumberAnimation(int num)
    {
        number = num;
        int Tempernum = number;
        float y = number * NumberStep - bias;
        NumberImage.transform.DOLocalMoveY(y, .15f).SetEase(Ease.OutBack);

        /*.OnComplete(() =>
    {
        if (Tempernum == 10)
        {
            number = 0;
            SetNumber(number);
        }
    });*/
    }
}