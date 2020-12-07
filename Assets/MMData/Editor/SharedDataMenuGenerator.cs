﻿using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MildMania.SharedDataSystem
{
    public class SharedDataMenuGenerator
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            CreateSharedDataMenuCreationScript();
            AssetDatabase.Refresh();
        }

        private static void CreateSharedDataMenuCreationScript()
        {
            string directoryPath = "Assets/MMData/AutoGenerated/";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = directoryPath + "SharedDataCreator.cs";

            if (File.Exists(filePath))
                File.Delete(filePath);

            StreamWriter sw = new StreamWriter(filePath);

            sw.WriteLine("using MildMania.SharedDataSystem;");
            sw.WriteLine("using UnityEditor;\n");
            sw.WriteLine("public class SharedDataCreator");
            sw.WriteLine("{");

            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            Type baseType = typeof(SharedData);

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsAbstract)
                        continue;

                    if (baseType.IsAssignableFrom(type))
                    {
                        string typeName = type.Name;
                        string methodName = "MenuItem_" + typeName;
                        string menuPath = "Assets/Shared Data/Create " + typeName;

                        sw.WriteLine("\t[MenuItem(\"" + menuPath + "\")]");
                        sw.WriteLine("\tpublic static void MenuItem_" + methodName + "()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tScriptableObjectUtility.CreateAsset<" + typeName + ">();");
                        sw.WriteLine("\t}");
                    }
                }
            }

            sw.WriteLine("}");
            sw.Close();
        }
    }
}