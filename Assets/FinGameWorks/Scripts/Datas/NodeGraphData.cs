using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FinGameWorks.Scripts.Datas
{
    [Serializable]
    public class NodeGraphData
    {
        [SerializeField]
        public ReactiveCollection<BaseNodeData> singleNodes = new ReactiveCollection<BaseNodeData>();

        [SerializeField]
        public ReactiveCollection<BaseNodeData> allNodes = new ReactiveCollection<BaseNodeData>();


        public NodeGraphData()
        {
            
        }

        public void AddNodeInstance(BaseNodeData nodeData)
        {
            singleNodes.Add(nodeData);
            allNodes.Add(nodeData);
        }

        public void AddNodeWithType(Type type)
        {
            BaseNodeData node = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as BaseNodeData;
            AddNodeInstance(node);
        }

        public void ConnectNodePort(BaseNodePortData connectionInPort, BaseNodePortData connectionOutPort)
        {
            
        }
    }
}