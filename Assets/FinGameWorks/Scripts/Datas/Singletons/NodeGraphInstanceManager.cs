using System.Collections.Generic;
using FinGameWorks.Scripts.Datas.Templates.NodeData;
using FinGameWorks.Scripts.Views;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace FinGameWorks.Scripts.Datas.Singletons
{
    [ExecuteInEditMode]
    public class NodeGraphInstanceManager : Singleton<NodeGraphInstanceManager>
    {
        public Dictionary<Key, NodeGraphData> editorDataDict = new Dictionary<Key, NodeGraphData>();
        public NodeGraphData GetNewNodeGraphDataInstance(Key key = null)
        {
            if (key == null)
            {
                NodeGraphData graphData = new NodeGraphData();
            
                graphData.allNodes.Add(new AddNodeData());
                graphData.allNodes.Add(new MinusNodeData());
                return graphData;
            }else if (!editorDataDict.ContainsKey(key) || editorDataDict[key] == null)
            {
                NodeGraphData graphData = new NodeGraphData();
            
                graphData.allNodes.Add(new AddNodeData());
                graphData.allNodes.Add(new MinusNodeData());
                editorDataDict.Remove(key);
                editorDataDict.Add(key,graphData);
                return graphData;
            }
            else
            {
                return editorDataDict[key];
            }
        }

    }
}