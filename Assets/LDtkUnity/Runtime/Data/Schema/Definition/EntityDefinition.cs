﻿using System.Runtime.Serialization;

namespace LDtkUnity
{
    public partial class EntityDefinition
    {
        /// <summary>
        /// Base entity color
        /// </summary>
        [DataMember(Name = "color")]
        public string Color { get; set; }

        /// <summary>
        /// Array of field definitions
        /// </summary>
        [DataMember(Name = "fieldDefs")]
        public FieldDefinition[] FieldDefs { get; set; }

        [DataMember(Name = "fillOpacity")]
        public double FillOpacity { get; set; }

        /// <summary>
        /// Pixel height
        /// </summary>
        [DataMember(Name = "height")]
        public long Height { get; set; }

        [DataMember(Name = "hollow")]
        public bool Hollow { get; set; }

        /// <summary>
        /// User defined unique identifier
        /// </summary>
        [DataMember(Name = "identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Only applies to entities resizable on both X/Y. If TRUE, the entity instance width/height
        /// will keep the same aspect ratio as the definition.
        /// </summary>
        [DataMember(Name = "keepAspectRatio")]
        public bool KeepAspectRatio { get; set; }

        /// <summary>
        /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
        /// </summary>
        [DataMember(Name = "limitBehavior")]
        public LimitBehavior LimitBehavior { get; set; }

        /// <summary>
        /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
        /// values: `PerLayer`, `PerLevel`, `PerWorld`
        /// </summary>
        [DataMember(Name = "limitScope")]
        public LimitScope LimitScope { get; set; }

        [DataMember(Name = "lineOpacity")]
        public double LineOpacity { get; set; }

        /// <summary>
        /// Max instances count
        /// </summary>
        [DataMember(Name = "maxCount")]
        public long MaxCount { get; set; }

        /// <summary>
        /// An array of 4 dimensions for the up/right/down/left borders (in this order) when using
        /// 9-slice mode for `tileRenderMode`.<br/>  If the tileRenderMode is not NineSlice, then
        /// this array is empty.<br/>  See: https://en.wikipedia.org/wiki/9-slice_scaling
        /// </summary>
        [DataMember(Name = "nineSliceBorders")]
        public long[] NineSliceBorders { get; set; }

        /// <summary>
        /// Pivot X coordinate (from 0 to 1.0)
        /// </summary>
        [DataMember(Name = "pivotX")]
        public double PivotX { get; set; }

        /// <summary>
        /// Pivot Y coordinate (from 0 to 1.0)
        /// </summary>
        [DataMember(Name = "pivotY")]
        public double PivotY { get; set; }

        /// <summary>
        /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
        /// </summary>
        [DataMember(Name = "renderMode")]
        public RenderMode RenderMode { get; set; }

        /// <summary>
        /// If TRUE, the entity instances will be resizable horizontally
        /// </summary>
        [DataMember(Name = "resizableX")]
        public bool ResizableX { get; set; }

        /// <summary>
        /// If TRUE, the entity instances will be resizable vertically
        /// </summary>
        [DataMember(Name = "resizableY")]
        public bool ResizableY { get; set; }

        /// <summary>
        /// Display entity name in editor
        /// </summary>
        [DataMember(Name = "showName")]
        public bool ShowName { get; set; }

        /// <summary>
        /// An array of strings that classifies this entity
        /// </summary>
        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// **WARNING**: this deprecated value will be *removed* completely on version 1.2.0+
        /// Replaced by: `tileRect`
        /// </summary>
        [DataMember(Name = "tileId")]
        public long? TileId { get; set; }

        [DataMember(Name = "tileOpacity")]
        public double TileOpacity { get; set; }

        /// <summary>
        /// An object representing a rectangle from an existing Tileset
        /// </summary>
        [DataMember(Name = "tileRect")]
        public TilesetRectangle TileRect { get; set; }

        /// <summary>
        /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
        /// values: `Cover`, `FitInside`, `Repeat`, `Stretch`, `FullSizeCropped`,
        /// `FullSizeUncropped`, `NineSlice`
        /// </summary>
        [DataMember(Name = "tileRenderMode")]
        public TileRenderMode TileRenderMode { get; set; }

        /// <summary>
        /// Tileset ID used for optional tile display
        /// </summary>
        [DataMember(Name = "tilesetId")]
        public long? TilesetId { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [DataMember(Name = "uid")]
        public long Uid { get; set; }

        /// <summary>
        /// Pixel width
        /// </summary>
        [DataMember(Name = "width")]
        public long Width { get; set; }
    }
}