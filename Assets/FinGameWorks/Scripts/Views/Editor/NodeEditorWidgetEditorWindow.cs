using Unity.UIWidgets.editor;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEditor;
using UnityEngine;

namespace FinGameWorks.Scripts.Views.Editor
{
    public class NodeEditorWidgetEditorWindow : UIWidgetsEditorWindow
    {
        
        [MenuItem("FinGameWorks/NodeEditor")]
        public static void gallery() {
            EditorWindow.GetWindow<NodeEditorWidgetEditorWindow>();
        }
        
        protected override Widget createWidget()
        {
            return new NodeEditorApp();
        }
    }
}