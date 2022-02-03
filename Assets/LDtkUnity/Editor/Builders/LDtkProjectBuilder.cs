﻿using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LDtkUnity.Editor
{
    internal class LDtkProjectBuilder
    {
        private readonly LDtkProjectImporter _importer;
        private readonly LdtkJson _json;
        private readonly World[] _worlds;

        public GameObject RootObject { get; private set; } = null;
        
        
        public LDtkProjectBuilder(LDtkProjectImporter importer, LdtkJson json)
        {
            _importer = importer;
            _json = json;
            _worlds = GetWorlds();
        }

        public void BuildProject()
        {
            if (!TryCanBuildProject())
            {
                return;
            }
            
            LDtkUidBank.CacheUidData(_json);
            BuildProcess();
            LDtkUidBank.ReleaseDefinitions();
        }

        private bool TryCanBuildProject()
        {
            if (_importer == null)
            {
                Debug.LogError("LDtk: Project was null, not building project.");
                return false;
            }

            if (_importer.JsonFile == null)
            {
                Debug.LogError("LDtk: Project File was null, not building project.", _importer);
                return false;
            }

            if (_json == null)
            {
                Debug.LogError("LDtk: ProjectJson was null, not building project.", _importer);
                return false;
            }

            if (_worlds.IsNullOrEmpty())
            {
                Debug.LogError("LDtk: No levels specified, not building project.", _importer);
                return false;
            }

            return true;
        }

        private void BuildProcess()
        {
            Stopwatch levelBuildTimer = Stopwatch.StartNew();

            LDtkPostProcessorCache.Initialize();
            CreateRootObject();
            BuildWorlds();
            InvokeCustomPostProcessing();

            levelBuildTimer.Stop();
            if (LDtkPrefs.LogBuildTimes && _worlds.Length > 1)
            {
                double ms = levelBuildTimer.ElapsedMilliseconds;
                Debug.Log($"LDtk: Built all worlds and levels in {ms}ms ({ms / 1000}s)");
            }
        }
        
        private void BuildWorlds()
        {
            foreach (World world in _worlds)
            {
                LDtkWorldBuilder worldBuilder = new LDtkWorldBuilder(_importer, _json, world);
                GameObject worldObj = worldBuilder.BuildWorld();
                
                worldObj.transform.SetParent(RootObject.transform);
            }
        }
        
        // How worlds are formulated: https://github.com/deepnight/ldtk/wiki/%5B0.10.0%5D-Multi-worlds
        private World[] GetWorlds()
        {
            if (_json.Worlds.IsNullOrEmpty())
            {
                return new World[] { World.FromJsonRoot(_json) };
            }

            return _json.Worlds;
        }

        private void InvokeCustomPostProcessing()
        {
            LDtkPostProcessorCache.AddPostProcessAction(() =>
            {
                LDtkPostProcessorInvoker.PostProcessProject(RootObject);
            });
            
            LDtkPostProcessorCache.PostProcess();
        }

        private void CreateRootObject()
        {
            RootObject = new GameObject(_importer.AssetName);

            LDtkComponentProject component = RootObject.AddComponent<LDtkComponentProject>();
            component.SetJson(_importer.JsonFile);


            if (_importer.DeparentInRuntime)
            {
                RootObject.AddComponent<LDtkDetachChildren>();
            }
        }
    }
}