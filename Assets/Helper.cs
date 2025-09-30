using System.IO;
using UnityEngine;

public class Helper
{
    public static Sprite GetColor(IconType _type) => _type switch
    {
        IconType.Attack => LoadImageSync("Attack.png"),
        IconType.Defend => LoadImageSync("Defend.png"),
        IconType.Skill => LoadImageSync("Skill.png"),
        IconType.Buffer => LoadImageSync("Buffer.png"),
    };


    private static Sprite LoadImageSync(string imageFileName)
    {
        // 拼接完整路径
        string fullPath = Path.Combine(Application.streamingAssetsPath, imageFileName);

        if (!File.Exists(fullPath))
        {
            return null;
        }

        // 读取图片字节
        byte[] imageData = File.ReadAllBytes(fullPath);

        // 转换为Texture2D
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(imageData)) // 自动识别PNG/JPG等格式
        {
            // 转换为Sprite并显示
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            return sprite;
        }
        else
        {
            GameObject.Destroy(texture);
            return null;
        }
    }
}