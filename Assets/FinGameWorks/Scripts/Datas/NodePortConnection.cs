using System;

namespace FinGameWorks.Scripts.Datas
{
    [Serializable]
    public class NodePortConnection
    {
        public NodePortData inPortRelativeToConnection;
        public NodePortData outPortRelativeToConnection;
    }
}