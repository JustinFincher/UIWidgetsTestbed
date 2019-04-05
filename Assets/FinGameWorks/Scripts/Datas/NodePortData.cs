using System;
using System.Collections.Generic;
using FinGameWorks.Scripts.Views;
using UniRx;
using UnityEngine;

namespace FinGameWorks.Scripts.Datas
{
    public enum NodePortDirectionRelativeToNode
    {
        In,
        Out
    }

    public interface NodePortDataInterface
    {
        List<Type> AcceptedTypes();
        string DefaultTitle();
        void Init();
    }

    [Serializable]
    public abstract class NodePortData : NodePortDataInterface
    {
        public ReactiveProperty<Vector2> reactivePosition = new ReactiveProperty<Vector2>(new Vector2(0,0));
        public ReactiveProperty<Vector2> reactiveSize = new ReactiveProperty<Vector2>(new Vector2(0,0));
        public Vector2 portCenter => new Vector2(reactivePosition.Value.x + reactiveSize.Value.x / 2, reactivePosition.Value.y + reactiveSize.Value.y / 2);
        public List<NodePortConnection> PortConnections;

        public WeakReference<NodeData> nodeData = new WeakReference<NodeData>(null);
        
        protected NodePortData()
        {
            Init();
        }

        public List<Type> AcceptedTypes()
        {
            return new List<Type>();
        }

        public virtual string DefaultTitle()
        {
            return "";
        }

        public string title;

        public void Init()
        {
            title = DefaultTitle();
        }
    }
    
    public class BaseNodePortData : NodePortData, ICloneable
    {
        public override string DefaultTitle()
        {
            return "Node Port";
        }

        public object Clone()
        {
            BaseNodePortData res =
                (BaseNodePortData) GetType().GetConstructor(new Type[] { })
                    .Invoke(new object[] { }) as BaseNodePortData;
            return res;
        }
    }
}