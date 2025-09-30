using System;
using UnityEngine;
using UnityEngine.UI;

public class TextDemo : MonoBehaviour
{
    private int text;
    public Text Text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeValue();
        }
    }


    public void ChangeValue()
    {
        Text.text += $"\n{text++}";
        Text.transform.position += new Vector3(0, 100);
    }
}