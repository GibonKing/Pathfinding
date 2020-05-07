using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public LevelManager levelManager;
    public AStar aStar;
    public DepthFirst dFirst;
    public BreadthFirst bFirst;

    public class Node
    {
        public int posX, posY, parentX, parentY;
        public float h, g, f;
        public Node()
        {
            posX = -1;
            posY = -1;
            parentX = -1;
            parentY = -1;
            h = 0;
            g = 0;
            f = 0;
        }
        public Node(int x, int y)
        {
            posX = x;
            posY = y;
            parentX = -1;
            parentY = -1;
            h = 0;
            g = 0;
            f = 0;
        }

        public bool Equals(Node node)
        {
            if (this.posX == node.posX && this.posY == node.posY)
                return true;
            else
                return false;
        }
    }

    public void RemoveNode(ref List<Node> nodes, ref Node node) {
	    for (int i = 0; i < nodes.Count; i++) {
    		if (nodes[i].Equals(node)) {
                nodes.Remove(nodes[i]);
                return;
            }
    	}
    }

    Node MakeNode(int x, int y)
    {
        if (levelManager.currentLevel.layout[levelManager.currentLevel.sizeY - y, x] != 0 && levelManager.currentLevel.layout[levelManager.currentLevel.sizeY - y, x] != 2) //Not empty or wall space
        {
            return new Node(x, y);
        }
        else
        {
            return new Node();
        }
    }
    
    public List<Node> GetAdjacent(Node node)
    {
        List<Node> nodes = new List<Node>();    
        nodes.Add(MakeNode(node.posX, node.posY - 1));    //Bottom
        nodes.Add(MakeNode(node.posX - 1, node.posY));    //Left
        nodes.Add(MakeNode(node.posX + 1, node.posY));    //Right
        nodes.Add(MakeNode(node.posX, node.posY + 1));    //Top    

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].posX == -1)
            {
                Node passNode = nodes[i];
                RemoveNode(ref nodes, ref passNode);
                i--;
            }
            else
            {
                nodes[i].parentX = node.posX;
                nodes[i].parentY = node.posY;
            }
        }    
        return nodes;
    }

    public bool InList(ref List<Node> nodes, ref Node node)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Equals(node))
            {
                return true;
            }
        }
        return false;
    }
    
    public Node GetNodeInList(ref List<Node> nodes, ref Node Node)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Equals(Node))
            {
                return nodes[i];
            }
        }
        return null;
    }

    public void GenerateRoutes()
    {
        aStar.FindRoute();
        dFirst.FindRoute();
        bFirst.FindRoute();
    }

    public void DrawRoute(ref GameObject marker, ref List<PathfindingManager.Node> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Instantiate(marker, new Vector2(nodes[i].posX, nodes[i].posY), Quaternion.identity);
        }
    }
}
