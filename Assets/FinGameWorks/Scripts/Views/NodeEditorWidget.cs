using System.Collections.Generic;
using System.Linq;
using FinGameWorks.Scripts.Datas;
using FinGameWorks.Scripts.Datas.Singletons;
using FinGameWorks.Scripts.Datas.Templates.NodeData;
using UniRx;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using UnityEngine;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace FinGameWorks.Scripts.Views
{
    public class NodeEditorWidget : StatefulWidget
    {
        public readonly NodeGraphData NodeGraphData;

        public NodeEditorWidget(Key key = null) : base(key)
        {
            NodeGraphData = NodeGraphInstanceManager.Instance.GetNewNodeGraphDataInstance(key);
        }
        
        public override State createState()
        {
            return new NodeEditorWidgetState();
        }
    }

    public class NodeEditorWidgetState : State<NodeEditorWidget>
    {
        private Vector2 scrollViewOffset = Vector2.zero;
        private readonly Vector2 scrollContentSize = new Vector2(2400,2400);
        private Vector2 scrollViewPortSize = Vector2.zero; 
        private readonly GlobalKey outerContainerStackKey = GlobalKey.key();
        private readonly GlobalKey innerContainerStackKey = GlobalKey.key();
        private readonly GlobalKey<NodeEditorScrollBarWidgetState> scrollBarXKey =
            GlobalKey<NodeEditorScrollBarWidgetState>.key();
        private readonly GlobalKey<NodeEditorScrollBarWidgetState> scrollBarYKey =
            GlobalKey<NodeEditorScrollBarWidgetState>.key();

        public override void initState()
        {
            base.initState();
            widget.NodeGraphData.allNodes.ObserveAdd().Subscribe(addEvent =>
            {
                setState(() => { });
            });
            widget.NodeGraphData.allNodes.ObserveRemove().Subscribe(addEvent =>
            {
                setState(() => { });
            });
        }

        public override Widget build(BuildContext context)
        {
            return new Container
            (
                color: Colors.grey.shade200,
                child: new GestureDetector
                (
                    child: new Stack // used to contain scrollbar
                    (    
                        outerContainerStackKey,
                        children:
                        new List<Widget>
                        {
                            new Positioned // used to apply offset
                            (
                                left: scrollViewOffset.x,
                                top: scrollViewOffset.y,
                                child: new Stack // used to contain graph and painter
                                (
                                    innerContainerStackKey,
                                    Alignment.topLeft,
                                    children: new List<Widget>
                                    {
                                        new Stack
                                        (
                                            children:widget.NodeGraphData.allNodes.Select(data => new NodeWidgetCanvasWrapper(data,innerContainerStackKey) as Widget).ToList()
                                        ),
                                        new CustomPaint
                                        (
                                            foregroundPainter: new NodePortConnectionCustomPainter(widget.NodeGraphData),
                                            size:new Size(scrollContentSize.x,scrollContentSize.y),
                                            isComplex:true,
                                            willChange:true
                                        )
                                    }
                                ),
                                width: scrollContentSize.x,
                                height: scrollContentSize.y
                            ),
                            new NodeEditorScrollBarWidget(NodeEditorScrollBarDirection.X,scrollViewPortSize,scrollViewOffset,scrollContentSize,scrollBarXKey),
                            new NodeEditorScrollBarWidget(NodeEditorScrollBarDirection.Y,scrollViewPortSize,scrollViewOffset,scrollContentSize,scrollBarYKey),
                        },
                        alignment: Alignment.topLeft
                    ),
                    onPanEnd: details => { },
                    onPanStart: details => { },
                    onPanUpdate: details =>
                    {
                        Vector2 newScrollViewPortSize = new Vector2(MediaQuery.of(outerContainerStackKey.currentContext).size.width,MediaQuery.of(outerContainerStackKey.currentContext).size.height);
                        Vector2 newOffset = scrollViewOffset + details.delta.toVector();
                        newOffset.x = newOffset.x > 0 ? 0 : newOffset.x;
                        newOffset.x =
                            newOffset.x < scrollViewPortSize.x - scrollContentSize.x
                                ? scrollViewPortSize.x - scrollContentSize.x
                                : newOffset.x;  
                        newOffset.y = newOffset.y > 0 ? 0 : newOffset.y;
                        newOffset.y =
                            newOffset.y < scrollViewPortSize.y - scrollContentSize.y
                                ? scrollViewPortSize.y - scrollContentSize.y
                                : newOffset.y;  
                        setState(() =>
                        {
                            scrollViewOffset = newOffset;
                            scrollViewPortSize = newScrollViewPortSize;
                        });
                        scrollBarXKey.currentState.updateState(newScrollViewPortSize, newOffset, scrollContentSize);
                        scrollBarYKey.currentState.updateState(newScrollViewPortSize, newOffset, scrollContentSize);
                    },
                    onTapDown: details => { },
                    onLongPress: () =>
                    {
                        
                    },
                    behavior: HitTestBehavior.translucent
                ),
                padding: EdgeInsets.zero
            );
        }
    }
}