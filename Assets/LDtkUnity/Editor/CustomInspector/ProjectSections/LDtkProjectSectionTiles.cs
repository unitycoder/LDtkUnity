﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace LDtkUnity.Editor
{
    public class LDtkProjectSectionTiles : LDtkProjectSectionDrawer<TilesetDefinition>
    {
        public LDtkProjectSectionTiles(SerializedObject serializedObject) : base(serializedObject)
        {
        }

        protected override string PropertyName => LDtkProject.TILE_COLLECTIONS;
        protected override string GuiText => "Tiles";
        protected override string GuiTooltip => "Tile Collections store tilemap tiles based on a texture's sliced sprites. " +
                                                "Generate the collections in the Tilesets section and then assign them here. " +
                                                "If the texture was only used for entity visuals in the LDtk editor, then it's not required to assign the field.";
        protected override Texture2D GuiImage => LDtkIconLoader.LoadAutoLayerIcon();

        protected override void GetDrawers(TilesetDefinition[] defs, List<LDtkContentDrawer<TilesetDefinition>> drawers)
        {
            for (int i = 0; i < defs.Length; i++)
            {
                TilesetDefinition definition = defs[i];
                SerializedProperty tileCollection = ArrayProp.GetArrayElementAtIndex(i);
                LDtkDrawerTileCollection drawer = new LDtkDrawerTileCollection(definition, tileCollection, definition.Identifier);
                drawers.Add(drawer);
            }
        }
    }
}