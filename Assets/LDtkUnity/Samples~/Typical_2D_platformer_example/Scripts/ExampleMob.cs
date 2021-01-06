﻿using LDtkUnity.Enums;
using LDtkUnity.FieldInjection;
using UnityEngine;

namespace Samples.Typical_2D_platformer_example
{
    public class ExampleMob : MonoBehaviour
    {
        [LDtkField] public Item[] loot;
        [LDtkField] public Vector2[] patrol;
    }
}