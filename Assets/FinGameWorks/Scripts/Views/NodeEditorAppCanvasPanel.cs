using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public class NodeEditorAppCanvasPanel : UIWidgetsPanel
    {
        protected override void Start()
        {
            base.Start();
#if UNITY_WEBGL && !UNITY_EDITOR
            devicePixelRatioOverride = 0.5f;
#endif
            devicePixelRatioOverride = 0.0f;

        }
        
        protected override Widget createWidget()
        {
            return new NodeEditorApp();
        }
    }
}