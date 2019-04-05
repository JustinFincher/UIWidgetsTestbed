using System;
using System.Collections.Generic;
using FinGameWorks.Scripts.Views;
using UniRx;
using UnityEngine;

namespace FinGameWorks.Scripts.Datas
{
    public interface NodeDataInterface
    {
        List<NodePortData> DefaultInDataPorts();
        List<NodePortData> DefaultOutDataPorts();
        string DefaultTitle();
        void Init();
    }

    [Serializable]
    public abstract class NodeData : NodeDataInterface
    {
        protected NodeData()
        {
            Init();
        }

        public virtual List<NodePortData> DefaultInDataPorts()
        {
            return new List<NodePortData>();
        }

        public virtual List<NodePortData> DefaultOutDataPorts()
        {
            return new List<NodePortData>();
        }

        public virtual string DefaultTitle()
        {
            return "";
        }

        public ReactiveProperty<Vector2> reactivePosition = new ReactiveProperty<Vector2>(new Vector2(0,0));
        public ReactiveProperty<string> title = new ReactiveProperty<string>();

        public List<NodePortData> inDataPorts;
        public List<NodePortData> outDataPorts;
        public ReactiveProperty<bool> isSelected = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> isMock = new ReactiveProperty<bool>(false);
        
        public void Init()
        {
            title.Value = DefaultTitle();
            inDataPorts = DefaultInDataPorts();
            inDataPorts.ForEach(data => data.nodeData.SetTarget(this));
            outDataPorts = DefaultOutDataPorts();
            outDataPorts.ForEach(data => data.nodeData.SetTarget(this));
        }
    }
    
    [Serializable]
    public class BaseNodeData : NodeData, ICloneable
    {
        public override string DefaultTitle()
        {
            return "Base";
        }

        public override List<NodePortData> DefaultInDataPorts()
        {
            return new List<NodePortData>
            {
                new BaseNodePortData()
            };
        }

        public override List<NodePortData> DefaultOutDataPorts()
        {
            return new List<NodePortData>
            {
                new BaseNodePortData()
            };
        }

        public object Clone()
        {
            BaseNodeData clone = GetType().GetConstructor(new Type[] { }).Invoke(new object[] { }) as BaseNodeData;
            clone.reactivePosition.Value = reactivePosition.Value;
            return clone;
        }
    }
    
}