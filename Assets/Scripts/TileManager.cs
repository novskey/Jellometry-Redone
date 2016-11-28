using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    private List<GameObject> _tileObjects;

    public List<Tile> _tiles = new List<Tile>();

    public Vector3 StartPoint;

    public bool done;

    // Use this for initialization
	void Start ()
	{
	    _tileObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));

	    PlaceTiles();

	}

    private void PlaceTiles()
    {
        foreach (GameObject tile in _tileObjects)
        {
            Debug.Log(tile);
            List<Transform> joinNodes = new List<Transform>();
            for (int i = 0; i <= 3; i++)
            {
                if (tile.transform.GetChild(i).transform.childCount != 0)
                {
                    joinNodes.Add(tile.transform.GetChild(i));
                }
            }

            tile.GetComponent<Tile>().SetNodes(joinNodes);

            _tiles.Add(tile.GetComponent<Tile>());
        }

        int j = 0;
        do
        {
            if (j >= _tiles.Count - 1) break;

            Debug.Log(j + ", " + _tiles.Count);
            Tile initialTile = _tiles[j];

            if (initialTile.placed) continue;

            Debug.Log("inital tile: " + initialTile.gameObject);
            do
            {

                Debug.Log(initialTile);

                List<Tile> possibleTiles = PossibleTiles(initialTile);

                if (possibleTiles.Count == 0) break;

                JoinNode startNode = initialTile.GetRandomNode().GetComponent<JoinNode>();

                Debug.Log("possible tiles: " + possibleTiles.Count);
                Debug.Log(Random.Range(0,possibleTiles.Count));
                Tile endTile = possibleTiles.Count == 1 ? possibleTiles[0] : possibleTiles[Random.Range(0,possibleTiles.Count)];

                Debug.Log("target tile: " + endTile.gameObject);

                JoinNode endNode = endTile.GetRandomNode().GetComponent<JoinNode>();

                Join(startNode, endNode);

                initialTile.Remove(startNode.transform);
                endTile.Remove(endNode.transform);

                if (initialTile.placed || PossibleTiles(initialTile).Count == 0) _tiles.Remove(initialTile);
                if (endTile.placed || PossibleTiles(endTile).Count == 0) _tiles.Remove(endTile);

            } while (initialTile.placed == false || PossibleTiles(initialTile).Count > 0);

            j++;
        } while (_tiles.Count > 0);
    }

    private List<Tile> PossibleTiles(Tile initialTile)
    {
        List<Tile> possibleTiles = new List<Tile>();

        if (initialTile.Nodes() == 0) return possibleTiles;

        foreach (Tile tile in _tiles)
        {
            if (tile == initialTile) continue;
            if (tile.placed) continue;
            if (tile.JoinedTo(initialTile)) continue;
            if (tile.joinedTo.Count > 0 && initialTile.joinedTo.Count > 0) continue;
            Debug.Log("Adding " + tile);
            possibleTiles.Add(tile);
        }
        return possibleTiles;
    }


    private void Join(JoinNode startNode, JoinNode endNode)
    {
        Debug.Log("Joining nodes: " + startNode +","+endNode);
        Debug.Log("");
        Transform startTile = startNode.transform.parent;

        Transform endTile = endNode.transform.parent;

        Vector3 startDir = startNode.GetDir();
        Vector3 endDir = endNode.GetDir();

        Vector3 opp = new Vector3(0,0,0);
        Vector3 right = new Vector3(-1,0,1);
        Vector3 left = new Vector3(-1,0,-1);

        Vector3 diff = startDir - endDir;
        Debug.Log(startDir + ", " + endDir + ", " + diff);

        if (diff == opp)
        {
            Debug.Log("Opp");
            endTile.Rotate(0,180,0);
        }else if (diff == right)
        {
            Debug.Log("right");
            endTile.Rotate(0,90,0);
        }else if (diff == left)
        {
            Debug.Log("left");
            endTile.Rotate(0,90,0);
        }else if (diff == new Vector3(1, 0, 1))
        {
            endTile.Rotate(0,90,0);
        }else if (startDir == new Vector3(0, 0, -1) && endDir == new Vector3(-1, 0, 0))
        {
            endTile.Rotate(0,90,0);
        }else if (diff == new Vector3(1, 0, -1))
        {
            endTile.Rotate(0,-90,0);
        }

        Vector3 startNodePos = startNode.transform.position;

        Vector3 endNodePos = endNode.transform.position;

        startNodePos = startNode.transform.position;

        endNodePos = endNode.transform.position;

        Vector3 vector3 = startNodePos - endNodePos;

        Debug.Log("Translating " + endTile.gameObject + " by: " + vector3);
        endTile.Translate(vector3,startTile);

        if (startDir == Vector3.forward)
        {
            endTile.Translate(0,0,5,startTile);
        }else if (startDir == Vector3.back)
        {
            endTile.Translate(0,0,-5,startTile);
        }else if (startDir == Vector3.left)
        {
            endTile.Translate(-5,0,0,startTile);
        }else if (startDir == Vector3.right)
        {
            endTile.Translate(5,0,0,startTile);
        }

        startNode.Join(endTile);
        endNode.Join(startTile);
    }


    void Update () {
    // Update is called once per frame

	    
	}
}
