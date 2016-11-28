using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tile
    {
        private GameObject _object;
        private List<JoinNode> _joinNodes;

        public Tile(GameObject gameObject, List<JoinNode> nodes)
        {
            _object = gameObject;
            _joinNodes = nodes;
        }
    }

    public class JoinNode
    {
        private NodeDir dir;


        public JoinNode(Transform transform)
        {
        }
    }

    public enum NodeDir
    {
        N,
        S,
        E,
        W
    }
}