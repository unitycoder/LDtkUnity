﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LDtkUnity.Editor
{
    public class LDtkProjectSectionLevels : LDtkProjectSectionDrawer<Level>
    {
        protected override string PropertyName => LDtkProject.LEVEL;
        protected override string GuiText => "Levels";
        protected override string GuiTooltip => "The levels. Hit the button at the bottom of this dropdown to automatically assign them.";
        protected override Texture2D GuiImage => LDtkIconLoader.LoadWorldIcon();
        
        public LDtkProjectSectionLevels(SerializedObject serializedObject) : base(serializedObject)
        {
        }

        protected override void GetDrawers(Level[] defs, List<LDtkContentDrawer<Level>> drawers)
        {
            for (int i = 0; i < defs.Length; i++)
            {
                Level level = defs[i];
                SerializedProperty levelObj = ArrayProp.GetArrayElementAtIndex(i);
                LDtkDrawerLevel drawer = new LDtkDrawerLevel(level, levelObj, level.Identifier);
                
                drawers.Add(drawer);
            }
        }

        protected override void DrawDropdownContent(Level[] datas)
        {
            base.DrawDropdownContent(datas);
            AutoAssetLinkerLevels linkerLevels = new AutoAssetLinkerLevels();
            linkerLevels.DrawButton(ArrayProp, datas, Project.ProjectJson);
        }
        
    }
}