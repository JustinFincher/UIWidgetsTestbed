using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace FinGameWorks.Scripts.Editor.Modifier
{
    public static class UiWidgetsDefineExportChecker
    {
        public static void ModifyDefine(string define,bool isAdd)
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            List<string> allDefines = definesString.Split ( ';' ).ToList ();

            if (isAdd)
            {
                if (!allDefines.Contains(define))
                {
                    allDefines.Add(define);
                }
            }
            else
            {
                if (allDefines.Contains(define))
                {
                    allDefines.Remove(define);
                }
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup 
            (
                EditorUserBuildSettings.selectedBuildTargetGroup,string.Join ( ";", allDefines.ToArray() ) 
            );
        }
    }

    public class UiWidgetsDefineExportCheckerPreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("Removing UIWidgets_DEBUG");
            UiWidgetsDefineExportChecker.ModifyDefine("UIWidgets_DEBUG", false);
        }
    }

    public class UiWidgetsDefineExportCheckerPostBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; }
        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log("Adding UIWidgets_DEBUG");
            UiWidgetsDefineExportChecker.ModifyDefine("UIWidgets_DEBUG", true);
        }
    }
}