using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Tools.Utils
{
    public static class ColorUtils
    {
        public static Color SetAlpha(this Color self, float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            return new Color(self.r, self.g, self.b, alpha);
        }

        public static Color SetTransparent(this Color self)
        {
            return self.SetAlpha(0);
        }

        public static Color ResetTransparent(this Color self)
        {
            return self.SetAlpha(1);
        }

        public static Color Hex2Color(string hex)
        {
            if (hex.Length != 7 || !hex.StartsWith("#"))
            {
                Debug.LogError("颜色格式解析错误");
                return Color.clear;
            }

            var red   = int.Parse(hex[1..3], NumberStyles.HexNumber);
            var green = int.Parse(hex[3..5], NumberStyles.HexNumber);
            var blue  = int.Parse(hex[5..7], NumberStyles.HexNumber);
            return new Color(red / 255f, green / 255f, blue / 255f);
        }

        public static string Color2Hex(Color color)
        {
            return $"#{Mathf.RoundToInt(color.r * 255f):X2}{Mathf.RoundToInt(color.g * 255f):X2}{Mathf.RoundToInt(color.b * 255f):X2}";
        }
    }
}