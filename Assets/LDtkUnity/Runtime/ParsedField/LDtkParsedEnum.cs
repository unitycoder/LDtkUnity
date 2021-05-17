﻿using System;
using UnityEngine;

namespace LDtkUnity.Editor
{
    public sealed class LDtkParsedEnum : ILDtkValueParser
    {
        public bool TypeName(FieldInstance instance) => instance.IsEnum;

        public object ImportString(object input)
        {
            string stringInput = (string) input;
            
            //enums are able to be legally null
            if (string.IsNullOrEmpty(stringInput))
            {
                return default;
            }

            //give enum value an underscore if a space was in the LDtk definition
            stringInput = stringInput.Replace(' ', '_');
            
            //because the enum scripts are probably not compiled yet, we cannot parse the enum yet so just do a string
            return stringInput;
            
            

        }
    }
}