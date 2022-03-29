﻿using UnityEditor;
using UnityEngine;

namespace LDtkUnity.Editor
{
    internal static class LDtkHandleDrawerUtil
    {
        //todo make this better soon, where it draws a single poly line instead of many single poly lines to look nicer
        //Original code from: https://github.com/deepnight/ldtk/blob/51819b99e0aa83e20d56500569657b03bd3e54c1/src/electron.renderer/display/FieldInstanceRender.hx#L21
        public static void RenderRefLink(Vector3 from, Vector3 to, int gridSize)
        {
            Color color = Handles.color;
            float width = LDtkPrefs.FieldEntityRefThickness;
            float fx = from.x;
            float fy = from.y;
            float tx = to.x;
            float ty = to.y;

            float a = Mathf.Atan2(ty - fy, tx - fx);
            float len = Vector2.Distance((Vector2)from, (Vector2)to);
            float dashLen = Mathf.Min((float)5/gridSize, len * 0.05f);
            int count = Mathf.CeilToInt(len / dashLen);
            dashLen = len / count;

            int sign = -1;
            float off = 2.5f / gridSize;
            float x = fx;
            float y = fy;
            float z = from.z;
    
            for (int n = 0; n < count; n++)
            {
                float r = (float)n/(count-1); //ratio
                float startRatio = Mathf.Min(r/0.05f, 1);

                Vector3 subStart = new Vector3(x, y, z);
                x = fx + Mathf.Cos(a) * (n * dashLen) + Mathf.Cos(a + Mathf.PI / 2) * sign * off * (1 - r) * startRatio;
                y = fy + Mathf.Sin(a) * (n * dashLen) + Mathf.Sin(a + Mathf.PI / 2) * sign * off * (1 - r) * startRatio;
                z = Vector3.Lerp(from, to, r).z;
                Vector3 subEnd = new Vector3(x, y, z);
        
                Color subColor = color;
                subColor.a = (0.15f + 0.85f * (1 - r)) * color.a;
        
                DrawLine(subStart, subEnd, subColor, width);
                sign = -sign;
            }
    
            //final line
            Vector3 lastStart = new Vector3(x, y, z);
            Color lastColor = color;
            lastColor.a = 0.15f * color.a;
            DrawLine(lastStart, to, lastColor, width);
        
            Handles.color = color;
        }
    
        //Code from: https://github.com/deepnight/ldtk/blob/04d4a04406f4cfdc23297b5a860d04eaa6731144/src/electron.renderer/display/FieldInstanceRender.hx#L79
        public static void RenderSimpleLink(Vector3 from, Vector3 to, float gridSize)
        {
            Color baseColor = Handles.color;
            float width = LDtkPrefs.FieldPointsThickness;
            float fx = from.x;
            float fy = from.y;
            float tx = to.x;
            float ty = to.y;

            float a = Mathf.Atan2(ty-fy, tx-fx);
            float len = Vector2.Distance((Vector2)from, (Vector2)to);
            float dashLen = 10f / gridSize;
            int count = Mathf.CeilToInt(len / dashLen);
        
            if (count <= 1)
            {
                Color subColor = baseColor;
                subColor.a = 0.4f;
                DrawLine(from, to, subColor, width);
                return;
            }
        
            dashLen = len / count;

            int sign = -1;
            float off = 0.7f / gridSize;
            float x = fx;
            float y = fy;
            float z = from.z;

            for (int n = 0; n < count; n++)
            {
                float ratio = (float)n / (count - 1);

                Vector3 subStart = new Vector3(x, y, z);
                x = fx + Mathf.Cos(a) * (n * dashLen) + Mathf.Cos(a + Mathf.PI / 2) * sign * off;
                y = fy + Mathf.Sin(a) * (n * dashLen) + Mathf.Sin(a + Mathf.PI / 2) * sign * off;
                z = Vector3.Lerp(from, to, ratio).z;
                Vector3 subEnd = new Vector3(x, y, z);
        
                Color subColor = baseColor;
                subColor.a = 0.4f + 0.6f * (1 - ratio);
        
                DrawLine(subStart, subEnd, subColor, width);

                sign = -sign;

            }

            //final line
            Vector3 lastStart = new Vector3(x, y, z);
            DrawLine(lastStart, to, baseColor, width);

            Handles.color = baseColor;
        }

        private static void DrawLine(Vector3 from, Vector3 to, Color color, float width)
        {
#if UNITY_EDITOR
            Vector3[] points = new[] { from, to };
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawAAPolyLine(width, points);
#endif
        }
    
        private static void DrawLines(Vector3[] points, Color color, float width)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawAAPolyLine(width, points);
#endif
        }
    }
}