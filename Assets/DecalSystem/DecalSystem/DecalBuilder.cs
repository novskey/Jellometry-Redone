﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.DecalSystem.DecalSystem
{
    public class DecalBuilder {

        private static List<Vector3> _bufVertices = new List<Vector3>();
        private static List<Vector3> _bufNormals = new List<Vector3>();
        private static List<Vector2> _bufTexCoords = new List<Vector2>();
        private static List<int> _bufIndices = new List<int>();


        public static void BuildDecalForObject(Decal decal, GameObject affectedObject) {
            Mesh affectedMesh = affectedObject.GetComponent<MeshFilter>().sharedMesh;
            if(affectedMesh == null) return;

            float maxAngle = decal.MaxAngle;
	
            Plane right = new Plane( Vector3.right, Vector3.right/2f );
            Plane left = new Plane( -Vector3.right, -Vector3.right/2f );

            Plane top = new Plane( Vector3.up, Vector3.up/2f );
            Plane bottom = new Plane( -Vector3.up, -Vector3.up/2f );

            Plane front = new Plane( Vector3.forward, Vector3.forward/2f );
            Plane back = new Plane( -Vector3.forward, -Vector3.forward/2f );

            Vector3[] vertices = affectedMesh.vertices;
            int[] triangles = affectedMesh.triangles;
            int startVertexCount = _bufVertices.Count;

            Matrix4x4 matrix = decal.transform.worldToLocalMatrix * affectedObject.transform.localToWorldMatrix;

            for(int i=0; i<triangles.Length; i+=3) {
                int i1 = triangles[i];
                int i2 = triangles[i+1];
                int i3 = triangles[i+2];
			
                Vector3 v1 = matrix.MultiplyPoint( vertices[i1] );
                Vector3 v2 = matrix.MultiplyPoint( vertices[i2] );
                Vector3 v3 = matrix.MultiplyPoint( vertices[i3] );

                Vector3 side1 = v2 - v1;
                Vector3 side2 = v3 - v1;
                Vector3 normal = Vector3.Cross(side1, side2).normalized;

                if( Vector3.Angle(-Vector3.forward, normal) >= maxAngle ) continue;


                DecalPolygon poly = new DecalPolygon( v1, v2, v3 );

                poly = DecalPolygon.ClipPolygon(poly, right);
                if(poly == null) continue;
                poly = DecalPolygon.ClipPolygon(poly, left);
                if(poly == null) continue;

                poly = DecalPolygon.ClipPolygon(poly, top);
                if(poly == null) continue;
                poly = DecalPolygon.ClipPolygon(poly, bottom);
                if(poly == null) continue;

                poly = DecalPolygon.ClipPolygon(poly, front);
                if(poly == null) continue;
                poly = DecalPolygon.ClipPolygon(poly, back);
                if(poly == null) continue;

                AddPolygon( poly, normal );
            }

            if (decal.Sprite)
                GenerateTexCoords (startVertexCount, decal.Sprite);
            else {
                int textureWidth = decal.Material.mainTexture.width;
                int textureHeight = decal.Material.mainTexture.height;
                Texture2D textureData = new Texture2D(textureWidth,textureHeight);
                Sprite.Create(textureData, new Rect(0,0,textureWidth,textureHeight), new Vector2(textureWidth/2,textureHeight/2));
                GenerateTexCoords (startVertexCount, decal.Sprite);
            }
        }

        private static void AddPolygon(DecalPolygon poly, Vector3 normal) {
            int ind1 = AddVertex( poly.Vertices[0], normal );
            for(int i=1; i<poly.Vertices.Count-1; i++) {
                int ind2 = AddVertex( poly.Vertices[i], normal );
                int ind3 = AddVertex( poly.Vertices[i+1], normal );

                _bufIndices.Add( ind1 );
                _bufIndices.Add( ind2 );
                _bufIndices.Add( ind3 );
            }
        }

        private static int AddVertex(Vector3 vertex, Vector3 normal) {
            int index = FindVertex(vertex);
            if(index == -1) {
                _bufVertices.Add( vertex );
                _bufNormals.Add( normal );
                index = _bufVertices.Count-1;
            } else {
                Vector3 t = _bufNormals[ index ] + normal;
                _bufNormals[ index ] = t.normalized;
            }
            return (int) index;
        }

        private static int FindVertex(Vector3 vertex) {
            for(int i=0; i<_bufVertices.Count; i++) {
                if( Vector3.Distance(_bufVertices[i], vertex) < 0.01f ) {
                    return i;
                }
            }
            return -1;
        }

        private static void GenerateTexCoords(int start, Sprite sprite) {
            Rect rect = sprite.rect;
            rect.x /= sprite.texture.width;
            rect.y /= sprite.texture.height;
            rect.width /= sprite.texture.width;
            rect.height /= sprite.texture.height;
		
            for(int i=start; i<_bufVertices.Count; i++) {
                Vector3 vertex = _bufVertices[i];
			
                Vector2 uv = new Vector2(vertex.x+0.5f, vertex.y+0.5f);
                uv.x = Mathf.Lerp( rect.xMin, rect.xMax, uv.x );
                uv.y = Mathf.Lerp( rect.yMin, rect.yMax, uv.y );
			
                _bufTexCoords.Add( uv );
            }
        }

        public static void Push(float distance) {
            for(int i=0; i<_bufVertices.Count; i++) {
                Vector3 normal = _bufNormals[i];
                _bufVertices[i] += normal * distance;
            }
        }


        public static Mesh CreateMesh() {
            if(_bufIndices.Count == 0) {
                return null;
            }
            Mesh mesh = new Mesh();

            mesh.vertices = _bufVertices.ToArray();
            mesh.normals = _bufNormals.ToArray();
            mesh.uv = _bufTexCoords.ToArray();
            mesh.uv2 = _bufTexCoords.ToArray();
            mesh.triangles = _bufIndices.ToArray();

            _bufVertices.Clear();
            _bufNormals.Clear();
            _bufTexCoords.Clear();
            _bufIndices.Clear();

            return mesh;
        }

    }
}
