using System;
using System.Collections.Generic;
using System.Linq;
using FinGameWorks.Scripts.Datas;
using FinGameWorks.Scripts.Datas.Singletons;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public class NodeDataClassesListWidget : StatefulWidget
    {
        public Key nodeEditorKey;
        public NodeDataClassesListWidget WithNodeEditorKey(Key key)
        {
            nodeEditorKey = key;
            return this;
        }
    
        public override State createState()
        {
            return new NodeDataClassesListWidgetState();
        }
    }

    public class NodeDataClassesListWidgetState : State<NodeDataClassesListWidget>
    {
        private readonly List<BaseNodeData> types;
        public NodeDataClassesListWidgetState()
        {
            if (NodeClassProviderManager.Instance == null || NodeClassProviderManager.Instance.nodeDataTypes == null)
            {
                Debug.LogWarning("NodeClassProviderManager.Instance.nodeDataTypes == null");
                return;
            }
            types = NodeClassProviderManager.Instance.nodeDataTypes;
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold
            (
                primary: false,
                body: new Container
                (
                    padding:EdgeInsets.all(8),
                    child:new Wrap
                    (
                        spacing:8,
                        runSpacing:8,
                        children:types.Select
                        (
                            type => new InkWell
                            (
                                child:new NodeWidget(type),
                                onTap:() =>
                                {
                                    Debug.Log("Selected Type " + type.GetType());
                                    NodeGraphInstanceManager.Instance.GetNewNodeGraphDataInstance(widget.nodeEditorKey).AddNodeWithType(type.GetType());
                                }
                            ) as Widget
                        ).ToList()
                    )
                ),
                appBar: new AppBar
                (
                    title: new Text("Add A Node")
                )
            );
            
        }
    }
}