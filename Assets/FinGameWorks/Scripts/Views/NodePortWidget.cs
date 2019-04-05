using System;
using System.Collections.Generic;
using FinGameWorks.Scripts.Datas;
using UniRx;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public class NodePortWidget : StatefulWidget
    {
        public readonly GlobalKey portKnotKey = GlobalKey.key();
        public readonly NodePortData nodePortData;
        public readonly NodePortDirectionRelativeToNode directionRelativeToNode;
        private readonly GlobalKey parentContainerStackKey;

        
        public NodePortWidget(NodePortData nodePortData, NodePortDirectionRelativeToNode directionRelativeToNode, GlobalKey parentContainerStackKey = null, Key key = null) : base(key)
        {
            this.nodePortData = nodePortData;
            this.directionRelativeToNode = directionRelativeToNode;
            this.parentContainerStackKey = parentContainerStackKey;
        }

        public override State createState()
        {
            return new NodePortWidgetState();
        }

        public NodePortWidget withSubscribeNodePosition(ReactiveProperty<Vector2> position)
        {
            position?.Subscribe(vector2 => { UpdateNodePortPosition(); });
            return this;
        }

        public void UpdateNodePortPosition()
        {
            if (parentContainerStackKey?.currentContext == null || portKnotKey.currentContext == null ||
                nodePortData == null) return;
            RenderObject renderObject = portKnotKey.currentContext.findRenderObject();
            Matrix3 matrix3 = renderObject.getTransformTo(parentContainerStackKey.currentContext.findRenderObject());
            nodePortData.reactivePosition.Value =
                new Vector2(matrix3.getTranslateX(),
                    matrix3.getTranslateY());
            nodePortData.reactiveSize.Value = new Vector2(renderObject.semanticBounds.size.width,renderObject.semanticBounds.size.height);

        }
    }

    public class NodePortWidgetState : State<NodePortWidget>
    {
        private Widget getPortKnot()
        {
            return new AspectRatio
            (
                aspectRatio: 1,
                child: new Container
                ( 
                    widget.portKnotKey,
                    margin: EdgeInsets.all(4),
                    decoration:new BoxDecoration
                    (
                        color: Colors.yellow.shade300,
                        borderRadius:BorderRadius.all(int.MaxValue),
                        border:Border.all(Colors.yellow.shade600,2.0f)
                    )
                )
            );
        }

        public override Widget build(BuildContext context)
        {
            return new Container
            (
                child:new FittedBox
                (
                    alignment:widget.directionRelativeToNode == NodePortDirectionRelativeToNode.In ? Alignment.centerLeft : Alignment.centerRight,
                    child: new IntrinsicHeight
                    (
                        child:new Row
                        (
                            crossAxisAlignment:CrossAxisAlignment.center,
                            children:widget.directionRelativeToNode == NodePortDirectionRelativeToNode.In ? 
                                new List<Widget>
                                {
                                    getPortKnot(),
                                    new Text
                                    (
                                        widget.nodePortData.title,
                                        textAlign:widget.directionRelativeToNode == NodePortDirectionRelativeToNode.In ? TextAlign.left : TextAlign.right
                                    )
                                }:
                                new List<Widget>
                                {
                                    new Text(widget.nodePortData.title),
                                    getPortKnot()
                                }
                        )
                    )
                ),
                padding:widget.directionRelativeToNode == NodePortDirectionRelativeToNode.In ? EdgeInsets.fromLTRB(0,2,4,2) : EdgeInsets.fromLTRB(4,2,0,2) 
            );
        }
    }
}