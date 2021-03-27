﻿
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LDtkUnity.Editor
{
    public static class LDtkPathUtil
    {
        /// <summary>
        /// //this directory is a folder that stands aside the project asset.
        /// Folder uses the same name as the original object
        /// </summary>
        public static string SiblingDirectoryOfAsset(Object obj)
        {
            string objAssetPath = AssetDatabase.GetAssetPath(obj);
            string objAssetDirectory = Path.GetDirectoryName(objAssetPath);
            
            string directory = $"{objAssetDirectory}/{obj.name}";

            return directory.Replace('\\', '/');
        }

        public static void CreateDirectoryIfNotValidFolder(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static string AbsolutePathToAssetsPath(string absolutePath)
        {
            if (absolutePath.StartsWith(Application.dataPath)) 
            {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            }

            Debug.LogWarning("Did not convert absolute path to assets path");
            return absolutePath;
        }

        public static string AssetsPathToAbsolutePath(string assetsPath)
        {
            if (!assetsPath.Contains("Assets"))
            {
                Debug.LogError("Incorrect string format");
            }
            
            return Application.dataPath + assetsPath.Remove(0, "Assets".Length);
        }

        public static string GetDirectoryOfSelectedPath(string title)
        {
            string startFrom = Application.dataPath;
            if (AssetDatabase.Contains(Selection.activeObject))
            {
                string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                startFrom = LDtkPathUtil.AssetsPathToAbsolutePath(assetPath);
                startFrom = Path.GetDirectoryName(startFrom);
            }
            
            string directory = EditorUtility.OpenFolderPanel(title, startFrom, "");

            if (string.IsNullOrEmpty(directory))
            {
                Debug.LogError("LDtk: Did not solve a path correctly, empty path specified");
                return "";
            }

            //if the path involves a hidden unity folder (maybe symbolic link reasons), then it will break. Ensure crashes cannot happen
            directory += '/';
            if (directory.Contains("~/"))
            {
                Debug.LogError("LDtk: Chosen directory contains a '~' as the end of a folder name, which could break unity folder paths. Consider renaming the folder.");
                return "";
            }

            if (!directory.Contains(Application.dataPath))
            {
                Debug.LogError("LDtk: Chosen directory is outside the Unity project.");
                return "";
            }
            
            return directory;
        }
    }
}