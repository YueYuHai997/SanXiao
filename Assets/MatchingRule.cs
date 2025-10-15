using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatchingRule
{
    private Rule rule;

    public MatchingRule(Rule _rule)
    {
        rule = _rule;
    }
    
    public List<IconItem> Judge(IconItem[][] icons)
    {
        // 边界检查
        if (icons == null || icons.Length == 0 || rule.pattern == null || rule.pattern.Length == 0)
            return null;

        // 生成 0/90/180/270 四种旋转形态
        var patterns = GetAllRotations(rule.pattern);

        // 使用集合去重，收集所有匹配到的元素
        var resultSet = new HashSet<IconItem>();

        for (int p = 0; p < patterns.Count; p++)
        {
            var pat = patterns[p];
            int patRows = pat.Length;
            int patCols = patRows > 0 ? pat[0].Length : 0;
            if (patRows == 0 || patCols == 0) continue;

            // 遍历Icons的所有可能起点
            for (int startRow = 0; startRow <= icons.Length - patRows; startRow++)
            {
                var row0 = icons[startRow];
                if (row0 == null || row0.Length < patCols) continue;

                for (int startCol = 0; startCol <= row0.Length - patCols; startCol++)
                {
                    var matched = MatchAt(icons, startRow, startCol, pat);
                    if (matched != null && matched.Count > 0)
                    {
                        for (int k = 0; k < matched.Count; k++)
                            resultSet.Add(matched[k]);
                    }
                }
            }
        }

        if (resultSet.Count == 0)
            return null;
        return new List<IconItem>(resultSet);
    }

    private List<IconType[][]> GetAllRotations(IconType[][] src)
    {
        var res = new List<IconType[][]>(4);
        var r0 = src;
        res.Add(r0);
        if (rule.AllDir)
        {
            var r90 = RotateCW(r0);
            var r180 = RotateCW(r90);
            var r270 = RotateCW(r180);
            
            res.Add(r90);
            res.Add(r180);
            res.Add(r270);
        }
        return res;
    }

    private IconType[][] RotateCW(IconType[][] src)
    {
        // src: rows x cols -> dst: cols x rows
        int rows = src.Length;
        int cols = src[0].Length;
        var dst = new IconType[cols][];
        for (int c = 0; c < cols; c++)
        {
            dst[c] = new IconType[rows];
        }

        for (int r = 0; r < rows; r++)
        {
            var row = src[r];
            for (int c = 0; c < cols; c++)
            {
                // 旋转 90 度顺时针: (r, c) -> (c, rows - 1 - r)
                dst[c][rows - 1 - r] = row[c];
            }
        }

        return dst;
    }

    private List<IconItem> MatchAt(IconItem[][] icons, int startRow, int startCol, IconType[][] pat)
    {
        int patRows = pat.Length;
        int patCols = pat[0].Length;
        var matched = new List<IconItem>();

        for (int i = 0; i < patRows; i++)
        {
            int r = startRow + i;
            if (r < 0 || r >= icons.Length) return null;
            var rowArr = icons[r];
            if (rowArr == null || rowArr.Length <= startCol + patCols - 1) return null;

            for (int j = 0; j < patCols; j++)
            {
                int c = startCol + j;
                var pv = pat[i][j];
                if (pv == IconType.None) continue;
                var item = rowArr[c];
                if (item == null || item.iconType != rule.iconType) return null;
                matched.Add(item);
            }
        }

        return matched;
    }
}
