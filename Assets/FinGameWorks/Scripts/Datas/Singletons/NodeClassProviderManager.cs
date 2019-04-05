using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FinGameWorks.Scripts.Datas.Singletons
{
    [ExecuteInEditMode]
    public class NodeClassProviderManager : Singleton<NodeClassProviderManager>
    {
        [SerializeField]
        public List<BaseNodeData> nodeDataTypes = new List<BaseNodeData>();

        private void Awake()
        {
            Refresh();
        }

        private void Refresh()
        {
            nodeDataTypes = typeof(BaseNodeData).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BaseNodeData)))
                .Select(type => type.GetConstructor(new Type[]{}).Invoke(new object[]{}) as BaseNodeData).ToList();
            nodeDataTypes.ForEach(nodeData => nodeData.isMock.Value = true);
        }

#if UNITY_EDITOR
        void OnEnable()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
        }

        void OnDisable()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
        }

        public void OnBeforeAssemblyReload()
        {
        }

        public void OnAfterAssemblyReload()
        {
            Refresh();
        }
#endif    
    }
}