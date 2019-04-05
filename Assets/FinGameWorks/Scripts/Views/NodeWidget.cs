using System;
using System.Collections.Generic;
using System.Linq;
using FinGameWorks.Scripts.Datas;
using FinGameWorks.Scripts.Datas.Templates.NodePortData;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using FontStyle = Unity.UIWidgets.ui.FontStyle;

namespace FinGameWorks.Scripts.Views
{
    public enum NodeMoreMenuItem {
        Delete
    }
    
    public class NodeWidget : StatefulWidget
    {
        public readonly BaseNodeData nodeData;
        public readonly GlobalKey parentContainerStackKey;

        public NodeWidget(BaseNodeData nodeData, GlobalKey parentContainerStackKey = null, Key key = null) : base(key)
        {
            this.nodeData = nodeData;
            this.parentContainerStackKey = parentContainerStackKey;
        }

        public override State createState()
        {
            return new NodeWidgetState();
        }
    }

    public class NodeWidgetState : State<NodeWidget>
    {
        public override Widget build(BuildContext context)
        {
            return new FittedBox
            (
                fit: BoxFit.contain,
                child: new Container
                    (
                        decoration:new BoxDecoration
                        (
                            color:Colors.white,
                            borderRadius: BorderRadius.circular(8),
                            border: Border.all
                            (
                                widget.nodeData.isSelected.Value ? Colors.orange : Colors.transparent,
                                widget.nodeData.isSelected.Value ? 4 : 0
                            )
                        ),
                        padding: EdgeInsets.fromLTRB(8,0,8,8),
                        alignment: Alignment.topLeft,
                        child: new IntrinsicWidth
                            (
                            child:new Column
                                (
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: new List<Widget>
                                    {
                                        new IntrinsicHeight
                                        (
                                            child:new Row
                                            (
                                                children:new List<Widget>
                                                {
                                                    new Expanded
                                                        (
                                                        child:new Text
                                                        (
                                                            widget.nodeData.title.Value,
                                                            textAlign: TextAlign.left,
                                                            style: new TextStyle
                                                            (
                                                                fontSize: 26,
                                                                fontWeight: FontWeight.w100,
                                                                fontStyle: FontStyle.italic
                                                            )
                                                        )
                                                    ),
                                                    new Visibility
                                                    (
                                                        visible:!widget.nodeData.isMock.Value,
                                                        maintainInteractivity:false,
                                                        child:new PopupMenuButton<NodeMoreMenuItem>
                                                        (
                                                            icon:new Icon
                                                            (
                                                                Icons.more_horiz,
                                                                size:18,
                                                                color:Theme.of(context).indicatorColor
                                                            ),
                                                            padding:EdgeInsets.zero,
                                                            itemBuilder: item => new List<PopupMenuEntry<NodeMoreMenuItem>>
                                                            {
                                                                new PopupMenuItem<NodeMoreMenuItem>
                                                                (
                                                                    value:NodeMoreMenuItem.Delete,
                                                                    child:new Text("Delete")
                                                                )
                                                            }
                                                        )
                                                    )
                                                }
                                            )
                                        ),
                                        new Row
                                        (
                                            mainAxisAlignment:MainAxisAlignment.start,
                                            crossAxisAlignment:CrossAxisAlignment.start,
                                            children: new List<Widget>
                                            {
                                                new Expanded
                                                (
                                                    child: new ClipRRect
                                                    (
                                                        borderRadius: BorderRadius.circular(4),
                                                        child: new Container
                                                        (
                                                            padding: EdgeInsets.all(4),
                                                            color: Colors.pink.shade50,
                                                            child: new Column
                                                            (
                                                                mainAxisAlignment:MainAxisAlignment.start,
                                                                crossAxisAlignment:CrossAxisAlignment.start,
                                                                children: widget.nodeData.inDataPorts.Select
                                                                (
                                                                    data => new NodePortWidget(data,NodePortDirectionRelativeToNode.In,widget.parentContainerStackKey).withSubscribeNodePosition(widget.nodeData.reactivePosition) as Widget
                                                                ).ToList()
                                                            )
                                                        )
                                                    )
                                                ),
                                                new Container(width: 4),
                                                new Expanded
                                                (
                                                    child: new ClipRRect
                                                    (
                                                        borderRadius: BorderRadius.circular(4),
                                                        child: new Container
                                                        (
                                                            padding: EdgeInsets.all(4),
                                                            color: Colors.teal.shade50,
                                                            child: new Column
                                                            (
                                                                mainAxisAlignment:MainAxisAlignment.start,
                                                                crossAxisAlignment:CrossAxisAlignment.end,
                                                                children: widget.nodeData.outDataPorts.Select
                                                                (
                                                                    data => new NodePortWidget(data,NodePortDirectionRelativeToNode.Out,widget.parentContainerStackKey).withSubscribeNodePosition(widget.nodeData.reactivePosition) as Widget
                                                                ).ToList()
                                                            )
                                                        )
                                                    )
                                                )
                                            }
                                        )
                                    }
                                )
                            )
                    )
            );
        }
    }
}