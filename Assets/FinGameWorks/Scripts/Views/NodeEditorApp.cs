using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public class NodeEditorApp : StatefulWidget
    {
        public override State createState()
        {
            return new NodeEditorAppState();
        }
    }

    public enum NodeEditorAppTheme
    {
        Day,
        Night
    }

    public class NodeEditorAppState : State<NodeEditorApp>
    {
        private NodeEditorAppTheme appTheme = NodeEditorAppTheme.Day;
        
        public override Widget build(BuildContext context)
        {
            Application.targetFrameRate = 120;
            NodeEditorAppConfig.LoadFonts();
            return new MaterialApp
            (
                title: "Node Editor",
                home: new NodeEditorAppWidget(),
                theme: appTheme == NodeEditorAppTheme.Day ? NodeEditorAppConfig.DayTheme() : NodeEditorAppConfig.NightTheme()
            );
        }
    }
}