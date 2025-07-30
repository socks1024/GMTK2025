using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Tools.Utils
{
    public static class StringUtils
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return GenerateRandomString(length, chars);
        }

        public static string GenerateRandomString(int length, string chars)
        {
            StringBuilder sb = new StringBuilder();
            System.Random random = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length); // 随机选择一个字符
                sb.Append(chars[index]); // 将字符添加到 StringBuilder
            }

            return sb.ToString();
        }

        public static string GetNumberStringWithSign(double number)
        {
            if (number < 0)
            {
                return $"-{Math.Abs(number)}";
            }
            else if (number > 0)
            {
                return $"+{number}";
            }
            else
            {
                return $"{number}";
            }
        }

        public static string GetColoredRichTextString(string text, Color color)
        {
            return $"<color={ColorUtils.Color2Hex(color)}>{text}</color>";
        }
    }
}