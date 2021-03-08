﻿using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace LDtkUnity.Editor
{
    public static class LDtkAsmRefFactory
    {
        private const string ASM_REF_TEMPLATE_PATH = LDtkEnumFactory.TEMPLATES_PATH + "AsmRefTemplate.txt";
        private const string ASM_REF_KEY = "#ASSEMBLY#";
        
        public static void CreateAssemblyDefinitionReference(string directory, AssemblyDefinitionAsset asmDef)
        {
            DeleteOldAsmRefs(directory, asmDef);
            CreateNewAsmRef(directory, asmDef);
        }

        private static void CreateNewAsmRef(string folderPath, AssemblyDefinitionAsset asmDef)
        {
            //don't make a file if none was inputted
            if (asmDef == null)
            {
                return;
            }

            string asmDefPath = AssetDatabase.GetAssetPath(asmDef);
            GUID guid = AssetDatabase.GUIDFromAssetPath(asmDefPath);

            string asmRefPath = folderPath + "/" + asmDef.name + ".asmref";
            string asmrefContents = LDtkInternalLoader.Load<TextAsset>(ASM_REF_TEMPLATE_PATH).text;

            asmrefContents = asmrefContents.Replace(ASM_REF_KEY, guid.ToString());

            LDtkAssetUtil.WriteText(asmRefPath, asmrefContents);
        }

        private static void DeleteOldAsmRefs(string folderPath, AssemblyDefinitionAsset asmDef)
        {
            string[] oldAsmDefRefGuids = AssetDatabase.FindAssets("t:AssemblyDefinitionReferenceAsset", new[] {folderPath});

            //delete any amsrefs that are not supposed to be there
            foreach (string oldGuid in oldAsmDefRefGuids)
            {
                string pathToOldAsset = AssetDatabase.GUIDToAssetPath(oldGuid);
                AssemblyDefinitionReferenceAsset asset =
                    AssetDatabase.LoadAssetAtPath<AssemblyDefinitionReferenceAsset>(pathToOldAsset);

                //only delete if the asmdef was not assigned or is not the same name

                if (asmDef != null && asset.name == asmDef.name)
                {
                    continue;
                }

                AssetDatabase.DeleteAsset(pathToOldAsset);
            }
        }
    }
}