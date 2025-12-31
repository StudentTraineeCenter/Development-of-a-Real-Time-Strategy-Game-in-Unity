using System.Collections.Generic;
using UnityEngine;

public static class FormationUtility
{
    public static List<Vector2> GetGridFormation(Vector2 target, int count, float spacing)
    {
        var result = new List<Vector2>(count);

        int cols = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = Mathf.CeilToInt(count / (float)cols);

        float width = (cols - 1) * spacing;
        float height = (rows - 1) * spacing;
        Vector2 topLeft = target + new Vector2(-width / 2f, height / 2f);

        int i = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (i >= count) break;

                Vector2 pos = topLeft + new Vector2(c * spacing, -r * spacing);
                result.Add(pos);
                i++;
            }
        }

        return result;
    }
}
