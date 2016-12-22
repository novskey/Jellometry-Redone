using UnityEngine;
using System.Collections.Generic;

[RequireComponent( typeof(MeshFilter) )]
[RequireComponent( typeof(MeshRenderer) )]
public class Decal : MonoBehaviour {

	public Material Material;
	public Sprite Sprite;

	public float MaxAngle = 90.0f;
	public float PushDistance = 0.009f;
	public LayerMask AffectedLayers = -1;
	private GameObject[] _affectedObjects;

	private Matrix4x4 _oldMatrix;
	private Vector3 _oldScale;

	void OnDrawGizmosSelected() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube( Vector3.zero, Vector3.one );
	}

	public Bounds GetBounds() {
		Vector3 size = transform.lossyScale;
		Vector3 min = -size/2f;
		Vector3 max =  size/2f;

		Vector3[] vts = new Vector3[] {
			new Vector3(min.x, min.y, min.z),
			new Vector3(max.x, min.y, min.z),
			new Vector3(min.x, max.y, min.z),
			new Vector3(max.x, max.y, min.z),

			new Vector3(min.x, min.y, max.z),
			new Vector3(max.x, min.y, max.z),
			new Vector3(min.x, max.y, max.z),
			new Vector3(max.x, max.y, max.z),
		};

		for(int i=0; i<8; i++) {

			vts[i] = transform.TransformDirection( vts[i] );
		}

		min = max = vts[0];
		foreach(Vector3 v in vts) {
			min = Vector3.Min(min, v);
			max = Vector3.Max(max, v);
		}

		return new Bounds(transform.position, max-min);
	}

	// Update is called once per frame
	void Update() {
		// Only rebuild mesh when scaling
		//bool hasChanged = oldMatrix != transform.localToWorldMatrix;
		bool hasChanged = _oldScale != transform.localScale;
		//oldMatrix = transform.localToWorldMatrix;
		_oldScale = transform.localScale;
		
		
		if(hasChanged) {
			BuildDecal( this );
		}

	}

	public void BuildDecal(Decal decal) {
		MeshFilter filter = decal.GetComponent<MeshFilter>();
		if(filter == null) filter = decal.gameObject.AddComponent<MeshFilter>();
		if(decal.GetComponent<Renderer>() == null) decal.gameObject.AddComponent<MeshRenderer>();
		decal.Material = decal.GetComponent<Renderer>().material;
		
		if(decal.Material == null || decal.Sprite == null) {
			filter.mesh = null;
			return;
		}
		
		_affectedObjects = GetAffectedObjects(decal.GetBounds(), decal.AffectedLayers);
		foreach(GameObject go in _affectedObjects) {
			DecalBuilder.BuildDecalForObject( decal, go );
		}
		DecalBuilder.Push( decal.PushDistance );
		
		Mesh mesh = DecalBuilder.CreateMesh();
		if(mesh != null) {
			mesh.name = "DecalMesh";
			filter.mesh = mesh;
		}
	}
		
	private static bool IsLayerContains(LayerMask mask, int layer) {
		//Debug.Log("Mask value is " + mask.value);
		//Debug.Log("Layer value is " + (layer >> 2));
		if (mask.value >= 0)
			return ((mask.value >> 2) & layer) != 0;
		else
			return true;
	}

	private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers) {
		MeshRenderer[] renderers = (MeshRenderer[]) GameObject.FindObjectsOfType<MeshRenderer>();
		List<GameObject> objects = new List<GameObject>();
		foreach(Renderer r in renderers) {
			if( !r.enabled ) continue;
            /*
            if (r.gameObject.name == "bonnet") {
                Debug.Log("bonnet layer is " + r.gameObject.layer);
                //int test = (affectedLayers.value >> 2);
                Debug.Log("affected layer is " + (affectedLayers.value >> 2));

                Debug.Log("Mask test: " + (affectedLayers.value & r.gameObject.layer >> 2));
                //Debug.Log("Mask test: " + (r.gameObject.layer & (affectedLayers.value >> 2)));
            }
            */
			if( !IsLayerContains(affectedLayers, r.gameObject.layer) || r.gameObject.tag == "Player" || r.gameObject.tag == "Enemy") continue;
			if( r.GetComponent<Decal>() != null ) continue;
			
			if( bounds.Intersects(r.bounds) ) {
				objects.Add(r.gameObject);
			}
		}
		return objects.ToArray();
	}
}