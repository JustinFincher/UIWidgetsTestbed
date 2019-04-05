using FinGameWorks.Scripts.Datas;
using UniRx;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Canvas = Unity.UIWidgets.ui.Canvas;

namespace FinGameWorks.Scripts.Views
{
    public enum NodePortConnectionLineStatus
    {
        Fail,
        Normal,
        Connected,
    }
    public class NodePortConnectionCustomPainter : AbstractCustomPainter
    {
        private readonly NodeGraphData NodeGraphData;

        public NodePortConnectionCustomPainter(NodeGraphData nodeGraphData, Listenable repaint = null) : base(repaint)
        {
            NodeGraphData = nodeGraphData;
        }

        public NodePortConnectionCustomPainter WithSubscribeRedraw()
        {
            // if blah blah
            // then
            return this;
        }

        public override void paint(Canvas canvas, Size size)
        {
            if (NodeGraphData == null || NodeGraphData.allNodes.Count <= 0)
            {
                return;
            }

            Vector2 inPortPos = NodeGraphData.allNodes[0].outDataPorts[0].portCenter;
            Vector2 outPortPos = NodeGraphData.allNodes[1].inDataPorts[0].portCenter;

            Paint bezierPaint = new Paint {style = PaintingStyle.stroke, color = Colors.yellow, strokeWidth = 4};

            float controlPointOffsetX = Mathf.Abs((inPortPos - outPortPos).x) / 2;

            Path bezierPath = new Path();
            bezierPath.moveTo(inPortPos.x,inPortPos.y);
            bezierPath.bezierTo(inPortPos.x + controlPointOffsetX,inPortPos.y,outPortPos.x - controlPointOffsetX,outPortPos.y,outPortPos.x,outPortPos.y);
            
           canvas.drawPath(bezierPath, bezierPaint);
        }

        public override bool shouldRepaint(CustomPainter oldDelegate)
        {
            return true;
            NodePortConnectionCustomPainter oldPainter = oldDelegate as NodePortConnectionCustomPainter;
            if (oldPainter == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}