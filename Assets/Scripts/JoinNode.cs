using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts
{
    public class JoinNode : MonoBehaviour
    {
        public NodeDir Dir;
        public Transform _joinedTo;

        public bool Connected()
        {
            return _joinedTo != null;
        }


        public void Join(Transform tile)
        {
            _joinedTo = tile;
            transform.parent.GetComponent<Tile>().joinedTo.Add(tile);
        }

        public Vector3 GetDir()
        {
            switch (Dir)
            {
                case NodeDir.E:
                    return Vector3.right;
                case NodeDir.W:
                    return Vector3.left;
                case NodeDir.N:
                    return Vector3.forward;
                case NodeDir.S:
                    return Vector3.back;
            }
            return new Vector3();
        }

        public bool ConnectedTo(Transform tile)
        {
            return _joinedTo == tile;
        }
    }

    public enum NodeDir
    {
        N, //0
        S, // 1
        E, // 2
        W // 3
    }
}