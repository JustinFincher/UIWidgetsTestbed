using System;
using System.Collections.Generic;
using FinGameWorks.Scripts.Datas.Templates.NodePortData;

namespace FinGameWorks.Scripts.Datas.Templates.NodeData
{
    [Serializable]
    public class MinusNodeData : BaseNodeData
    {
        public override string DefaultTitle()
        {
            return "Minus";
        }
        
        public override List<Datas.NodePortData> DefaultInDataPorts()
        {
            return new List<Datas.NodePortData>
            {
                new FloatNodePortData(),
                new FloatNodePortData()
            };
        }

        public override List<Datas.NodePortData> DefaultOutDataPorts()
        {
            return new List<Datas.NodePortData>
            {
                new FloatNodePortData()
            };
        }
    }
}