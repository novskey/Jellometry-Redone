using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour
    {
        public bool placed;
        public List<Transform> _joinNodes;
        public List<Transform> joinedTo;

        public void SetNodes(List<Transform> nodes)
        {
            _joinNodes = nodes;
        }

        public int Nodes()
        {
            return _joinNodes.Count;
        }

        public Transform[] GetNodes()
        {
            return _joinNodes.ToArray();
        }

        public Transform GetNode(int index)
        {
            return _joinNodes[index];
        }

        public Transform GetRandomNode()
        {
            Debug.Log("Getting random node from: " + Nodes());
            System.Random random = new System.Random();

            int chosen = random.Next(Nodes());
            Debug.Log("Chose: " + chosen);
            return GetNode(chosen);
        }

        public void Remove(Transform node)
        {
            Debug.Log("Removing node " + node.gameObject + " from " + gameObject);
            Debug.Log("Count after remove: " + _joinNodes.Count);
            _joinNodes.Remove(node);

            if (_joinNodes.Count == 0) placed = true;
        }

        public bool AllJoined()
        {
            foreach (Transform joinNode in _joinNodes)
            {
                if (joinNode.GetComponent<JoinNode>().Connected() == false) return false;
            }

            return true;
        }

        public bool JoinedTo(Tile initialTile)
        {
            foreach (Transform trans in joinedTo)
            {
                if (trans == initialTile.transform) return true;
            }
            return false;
        }
    }

}