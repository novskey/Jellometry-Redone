using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class TileManager : MonoBehaviour
{
    private List<GameObject> _tileObjects;

    private List<Tile> _tiles;

	// Use this for initialization
	void Start ()
	{
	    _tileObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Join Node"));

	    PlaceTiles();
	}

    private void PlaceTiles()
    {

        foreach (GameObject tile in _tileObjects)
        {
            List<JoinNode> joinNodes = new List<JoinNode>();
            for (int i = 0; i < 3; i++)
            {
                if (tile.transform.GetChild(i).transform.childCount != 0)
                {
                    joinNodes.Add(new JoinNode(tile.transform.GetChild(i));
                }
            }

            _tiles.Add(new Tile(tile, joinNodes));
        }
    }

	void Update () {
    // Update is called once per frame

	   
	    
	}
}
