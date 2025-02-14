﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Gamestrap.UI.Effects
{
    [AddComponentMenu("UI/Gamestrap UI/Gradient")]
    public class GradientEffect : GamestrapEffect
    {
        public Color Top = Color.white;
        public Color Bottom = Color.white;


        public override void ModifyVerticesWrapper(List<UIVertex> vertexList)
        {
            if (!IsActive() || vertexList.Count < 4)
            {
                return;
            }
#if UNITY_4_6 || UNITY_5_0 || UNITY_5_1
            if (vertexList.Count == 4)
            {
                SetVertexColor(vertexList, 0, bottom);
                SetVertexColor(vertexList, 1, top);
                SetVertexColor(vertexList, 2, top);
                SetVertexColor(vertexList, 3, bottom);
#else //This if has to be changed if you are using version 5.2.1p3 or later patches of 5.2.1 Use the bottom code for it to work.
            if (vertexList.Count == 6)
            {
                SetVertexColor(vertexList, 0, Bottom);
                SetVertexColor(vertexList, 1, Top);
                SetVertexColor(vertexList, 2, Top);
                SetVertexColor(vertexList, 3, Top);
                SetVertexColor(vertexList, 4, Bottom);
                SetVertexColor(vertexList, 5, Bottom);
#endif
            }
            else
            {
                float bottomPos = vertexList[vertexList.Count - 1].position.y;
                float topPos = vertexList[0].position.y;

                float height = topPos - bottomPos;

                for (int i = 0; i < vertexList.Count; i++)
                {
                    UIVertex v = vertexList[i];
                    v.color *= Color.Lerp(Top, Bottom, ((v.position.y) - bottomPos) / height);
                    vertexList[i] = v;
                }
            }
        }
    }
}