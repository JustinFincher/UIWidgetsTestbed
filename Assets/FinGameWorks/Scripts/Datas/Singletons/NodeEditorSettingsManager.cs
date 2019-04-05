using UniRx;
using UnityEngine;

namespace FinGameWorks.Scripts.Datas.Singletons
{
    [ExecuteInEditMode]
    public class NodeEditorSettingsManager : Singleton<NodeEditorSettingsManager>
    {
        public ReactiveProperty<bool> frameCounterEnabled = new ReactiveProperty<bool>(false);
    }
    
}