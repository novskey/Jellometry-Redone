using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Gamestrap.UI.Effects
{
    [AddComponentMenu("UI/Gamestrap UI/Directional Gradient")]
    public class RadialGradientEffect : GamestrapEffect
    {
        public Vector2 CenterPosition;
        public float Radius;
        public Color CenterColor = Color.white;

        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + (Vector3)CenterPosition, 2f);
        }

        public override void ModifyVerticesWrapper(List<UIVertex> vertexList)
        {
            if (!IsActive() || vertexList.Count < 4)
            {
                return;
            }

            if (Radius == 0)
            {
                Radius = 1;
            }
            for (int i = 0; i < vertexList.Count; i++)
            {
                UIVertex v = vertexList[i];

                v.color *= Color.Lerp(CenterColor, Color.white, Mathf.Clamp01(((Vector2)v.position - CenterPosition).magnitude / Radius));
                vertexList[i] = v;
            }
        }

    }
}