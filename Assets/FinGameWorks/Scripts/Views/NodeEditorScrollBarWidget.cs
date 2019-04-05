using System;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Views
{
    public enum NodeEditorScrollBarDirection
    {
        X,
        Y
    }

    public class NodeEditorScrollBarWidget : StatefulWidget
    {
        public NodeEditorScrollBarDirection direction;
        public Vector2 scrollViewSize;
        public Vector2 scrollViewOffset;
        public Vector2 scrollContentSize;

        public NodeEditorScrollBarWidget(NodeEditorScrollBarDirection direction, Vector2 scrollViewSize, Vector2 scrollViewOffset, Vector2 scrollContentSize, Key key = null) : base(key)
        {
            this.direction = direction;
            this.scrollViewSize = scrollViewSize;
            this.scrollViewOffset = scrollViewOffset;
            this.scrollContentSize = scrollContentSize;
        }

        public override State createState()
        {
            return new NodeEditorScrollBarWidgetState();
        }
    }

    public class NodeEditorScrollBarWidgetState : State<NodeEditorScrollBarWidget>
    {
        public static float scrollBarSize = 10;

        public override Widget build(BuildContext context)
        {
            ClipRRect child = new ClipRRect
            (
                borderRadius: BorderRadius.circular(scrollBarSize / 2),
                child: new Container(padding: EdgeInsets.fromLTRB(2,2,2,2), color: Theme.of(context).indicatorColor)
            );            
            float width = widget.direction == NodeEditorScrollBarDirection.X
                ? widget.scrollViewSize.x * Math.Min(widget.scrollViewSize.x / widget.scrollContentSize.x,1)
                : scrollBarSize;
            float height = widget.direction == NodeEditorScrollBarDirection.X
                ? scrollBarSize
                : widget.scrollViewSize.y * Math.Min(widget.scrollViewSize.y / widget.scrollContentSize.y,1);
            
            return widget.direction == NodeEditorScrollBarDirection.X ?
                new Positioned
                (
                    child: child,
                    width: width,
                    bottom: 2,
                    height: height,
                    left: (widget.scrollViewSize.x - width) * widget.scrollViewOffset.x / (widget.scrollViewSize.x - widget.scrollContentSize.x)
                ):
                new Positioned
                (
                    child: child,
                    width: width,
                    top: (widget.scrollViewSize.y - height) * widget.scrollViewOffset.y / (widget.scrollViewSize.y - widget.scrollContentSize.y),
                    height: height,
                    right: 2
                );
        }

        public void updateState(Vector2 scrollViewSize, Vector2 scrollViewOffset, Vector2 scrollContentSize)
        {
            setState(() =>
            {
                widget.scrollViewSize = scrollViewSize;
                widget.scrollViewOffset = scrollViewOffset;
                widget.scrollContentSize = scrollContentSize;
            });
        }
    }
}