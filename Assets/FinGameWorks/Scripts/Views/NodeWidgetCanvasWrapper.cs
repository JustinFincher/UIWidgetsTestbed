using FinGameWorks.Scripts.Datas;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public class NodeWidgetCanvasWrapper : StatefulWidget
    {
        private readonly BaseNodeData nodeData;
        private readonly GlobalKey parentContainerStackKey;
        
        public NodeWidgetCanvasWrapper(BaseNodeData nodeData, GlobalKey parentContainerStackKey, Key key = null) : base(key)
        {
            this.nodeData = nodeData;
            this.parentContainerStackKey = parentContainerStackKey;
        }

        public override State createState()
        {
            return new NodeWidgetCanvasWrapperState(nodeData);
        }

        public class NodeWidgetCanvasWrapperState : State<NodeWidgetCanvasWrapper>
        {
            private readonly BaseNodeData nodeData;
            private Column contentColumn;
            
            public NodeWidgetCanvasWrapperState(BaseNodeData nodeData, Key key = null)
            {
                this.nodeData = nodeData ?? new BaseNodeData();
            }

            public override Widget build(BuildContext context)
            {
                return new Positioned
                (
                    left:nodeData.reactivePosition.Value.x, 
                    top:nodeData.reactivePosition.Value.y,
                    child: new GestureDetector
                    (
                        child: new NodeWidget(nodeData,widget.parentContainerStackKey),
                        onTapDown: details =>
                        {
                            setState(() => { 
                                nodeData.isSelected.Value = true;
                            });
                        },
                        onTap: () => {
                            setState(() => { 
                                nodeData.isSelected.Value = true;
                            });  
                        },
                        onPanStart: details => { },
                        onPanUpdate: details =>
                        {
                            setState(() =>
                            {
                                Vector2 newPosition = nodeData.reactivePosition.Value + details.delta.toVector();
                                newPosition.x = newPosition.x < 0 ? 0 : newPosition.x;
                                newPosition.y = newPosition.y < 0 ? 0 : newPosition.y;
                                nodeData.reactivePosition.Value = newPosition;
                            });
                        },
                        onPanEnd: details => { }
                    )
                );
            }
        }
        
    }
}