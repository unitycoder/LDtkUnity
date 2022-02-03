﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDtkUnity.Editor
{
    internal class LDtkWorldBuilder
    {
        private readonly LDtkProjectImporter _importer;
        private readonly LdtkJson _json;
        private readonly World _world;

        private GameObject _worldObject;

        public LDtkWorldBuilder(LDtkProjectImporter importer, LdtkJson json, World world)
        {
            _importer = importer;
            _json = json;
            _world = world;
        }
        
        public GameObject BuildWorld()
        {
            CreateWorldObject();
            
            foreach (Level lvl in _world.Levels)
            {
                LDtkLevelBuilder levelBuilder = new LDtkLevelBuilder(_importer, _json, lvl);
                
                GameObject levelObj = levelBuilder.BuildLevel();
                levelObj.transform.SetParent(_worldObject.transform);
            }

            return _worldObject;
        }
        
        private void CreateWorldObject()
        {
            _worldObject = new GameObject(_world.Identifier);
            
            if (_importer.DeparentInRuntime)
            {
                _worldObject.AddComponent<LDtkDetachChildren>();
            }
        }
    }
}