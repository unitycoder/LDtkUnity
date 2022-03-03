﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace LDtkUnity
{
    [Serializable]
    internal class LDtkFieldElement
    {
        public const string PROPERTY_TYPE = nameof(_type);
        public const string PROPERTY_INT = nameof(_int);
        public const string PROPERTY_FLOAT = nameof(_float);
        public const string PROPERTY_BOOL = nameof(_bool);
        public const string PROPERTY_STRING = nameof(_string);
        public const string PROPERTY_COLOR = nameof(_color);
        public const string PROPERTY_VECTOR2 = nameof(_vector2);
        public const string PROPERTY_SPRITE = nameof(_sprite);

        [SerializeField] private LDtkFieldType _type;
        
        [SerializeField] private int _int = 0;
        [SerializeField] private float _float = 0;
        [SerializeField] private bool _bool = false;
        [SerializeField] private string _string = string.Empty;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Vector2 _vector2 = Vector2.zero;
        [SerializeField] private Sprite _sprite = null;

        public LDtkFieldType Type => _type;
        
        public LDtkFieldElement(object obj, FieldInstance instance)
        {
            _type = GetTypeForInstance(instance);
            switch (_type)
            {
                case LDtkFieldType.Int:
                    _int = Convert.ToInt32(obj);
                    break;
                
                case LDtkFieldType.Float:
                    _float = (float)obj;
                    break;
                
                case LDtkFieldType.Bool:
                    _bool = (bool)obj;
                    break;
                
                case LDtkFieldType.String:
                case LDtkFieldType.Multiline:
                case LDtkFieldType.FilePath:
                case LDtkFieldType.Enum:
                case LDtkFieldType.EntityRef:
                    _string = (string)obj;
                    break;
                
                case LDtkFieldType.Color:
                    _color = (Color)obj;
                    break;
                case LDtkFieldType.Point:
                    _vector2 = (Vector2)obj;
                    break;
                
                case LDtkFieldType.Tile:
                    _sprite = (Sprite)obj;
                    break;
            }
        }

        private LDtkFieldType GetTypeForInstance(FieldInstance instance)
        {
            if (instance.IsInt) return LDtkFieldType.Int;
            if (instance.IsFloat) return LDtkFieldType.Float;
            if (instance.IsBool) return LDtkFieldType.Bool;
            if (instance.IsString) return LDtkFieldType.String;
            if (instance.IsMultilines) return LDtkFieldType.Multiline;
            if (instance.IsFilePath) return LDtkFieldType.FilePath;
            if (instance.IsColor) return LDtkFieldType.Color;
            if (instance.IsEnum) return LDtkFieldType.Enum;
            if (instance.IsPoint) return LDtkFieldType.Point;
            if (instance.IsEntityRef) return LDtkFieldType.EntityRef;
            if (instance.IsTile) return LDtkFieldType.Tile;
            return LDtkFieldType.None;
        }
        
        public int GetIntValue() => GetData(_int, LDtkFieldType.Int);
        public float GetFloatValue() => GetData(_float, LDtkFieldType.Float);
        public bool GetBoolValue() => GetData(_bool, LDtkFieldType.Bool);
        public string GetStringValue() => GetData(_string, LDtkFieldType.String);
        public string GetMultilineValue() => GetData(_string, LDtkFieldType.Multiline);
        public string GetFilePathValue() => GetData(_string, LDtkFieldType.FilePath);
        public Color GetColorValue() => GetData(_color, LDtkFieldType.Color);
        public TEnum GetEnumValue<TEnum>() where TEnum : struct
        {
            if (string.IsNullOrEmpty(_string))
            {
                return default;
            }
            
            // For enums, we do a runtime process in order to work around the fact that enums need to compile 
            string data = GetData(_string, LDtkFieldType.Enum);
            if (data == default)
            {
                return default;
            }

            Type type = typeof(TEnum);
            if (!type.IsEnum)
            {
                Debug.LogError($"LDtk: Input type {type.Name} is not an enum");
                return default;
            }

            if (Enum.IsDefined(type, _string))
            {
                return (TEnum)Enum.Parse(type, _string);
            }
            
            Array values = Enum.GetValues(typeof(TEnum));
            List<string> stringValues = new List<string>();
            foreach (object value in values)
            {
                string stringValue = Convert.ToString(value);
                stringValues.Add(stringValue);
            }
            string joined = string.Join("\", \"", stringValues);

            Debug.LogError($"LDtk: C# enum \"{type.Name}\" does not define enum value \"{_string}\". Possible values are \"{joined}\"");
            return default;
        }
        public Vector2 GetPointValue() => GetData(_vector2, LDtkFieldType.Point);
        public string GetEntityRefValue() => GetData(_string, LDtkFieldType.EntityRef);
        public Sprite GetTileValue() => GetData(_sprite, LDtkFieldType.Tile);

        /// <summary>
        /// This pass helps protects against getting the wrong type for a certain field identifier
        /// </summary>
        private T GetData<T>(T data, LDtkFieldType type)
        {
            if (_type == type)
            {
                return data;
            }
            
            //an exception to the matching rule, multilines implementation is an advanced setting option in LDtk
            if (_type == LDtkFieldType.String && type == LDtkFieldType.Multiline)
            {
                return data;
            }
            
            Debug.LogError($"LDtk: Trying to get improper type \"{type}\" instead of \"{_type}\"");
            return default;
        }

        public string GetValueAsString()
        {
            switch (_type)
            {
                case LDtkFieldType.Int:
                    return _int.ToString();

                case LDtkFieldType.Float:
                    return _float.ToString(CultureInfo.CurrentCulture);

                case LDtkFieldType.Bool:
                    return _bool.ToString();

                case LDtkFieldType.String:
                case LDtkFieldType.Multiline:
                case LDtkFieldType.FilePath:
                case LDtkFieldType.Enum:
                case LDtkFieldType.EntityRef:
                    return _string;

                case LDtkFieldType.Color:
                    return _color.ToString();
                
                case LDtkFieldType.Point:
                    return _vector2.ToString();

                case LDtkFieldType.Tile:
                    return _sprite.ToString();
            }

            return "";
        }
    }
}