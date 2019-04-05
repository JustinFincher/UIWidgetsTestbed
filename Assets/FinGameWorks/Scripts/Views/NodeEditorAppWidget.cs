using System;
using System.Collections.Generic;
using FinGameWorks.Scripts.Datas.Singletons;
using UniRx;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace FinGameWorks.Scripts.Views
{
    public class NodeEditorAppWidget : StatefulWidget
    {
        public override State createState()
        {
            return new NodeEditorAppWidgetState();
        }
    }

    class NodeEditorAppWidgetState : State<NodeEditorAppWidget>
    {
        private readonly GlobalKey<NodeEditorWidgetState> nodeEditorWidgetKey = GlobalKey<NodeEditorWidgetState>.key();
        private IDisposable frameCounterEnabledDisposable;
        
        public override void initState()
        {
            base.initState();
            frameCounterEnabledDisposable = NodeEditorSettingsManager.Instance.frameCounterEnabled.Subscribe(b =>
            {
                setState();
            });
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold
            (
                backgroundColor: Colors.white,
                appBar: new AppBar
                (
                    title: new Text("Node Editor (UIWidgets Based)"),
                    centerTitle: false,
                    actions:new List<Widget>
                    {
                        new IconButton
                        (
                            icon:new Icon(Icons.brightness_4,color:Colors.white),
                            onPressed:() =>
                            {
                                
                            }
                        ),
                        new IconButton
                        (
                            icon:new Icon(Icons.settings,color:Colors.white),
                            onPressed: () =>
                            {
                                BottomSheetUtils.showModalBottomSheet<object>
                                (
                                    context,
                                    buildContext => new NodeEditorSettingsWidget()
                                );
                            }
                        )
                    }
                ),
                body: new Stack
                (
                    children:new List<Widget>
                    {
                        new NodeEditorWidget(nodeEditorWidgetKey),
                        new Visibility
                        (
                            maintainInteractivity:false,
                            maintainAnimation:false,
                            child:PerformanceOverlay.allEnabled(),
                            visible:NodeEditorSettingsManager.Instance.frameCounterEnabled.Value
                        )
                    }
                ),
                floatingActionButton: new FloatingActionButton
                (
                    onPressed: () =>
                    {
                        BottomSheetUtils.showModalBottomSheet<object>
                        (
                            context,
                            buildContext => new NodeDataClassesListWidget().WithNodeEditorKey(nodeEditorWidgetKey)
                        );
                    },
                    tooltip: "Add",
                    child: new Icon(Icons.add)),
                floatingActionButtonLocation: FloatingActionButtonLocation.endFloat
            );
        }

        public override void dispose()
        {
            base.dispose();
            frameCounterEnabledDisposable.Dispose();
        }
    }
}