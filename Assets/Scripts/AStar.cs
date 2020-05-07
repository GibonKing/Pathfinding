using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public LevelManager lManager;
    public PathfindingManager pManager;
    public GameObject marker;

    PathfindingManager.Node getLowestF(ref List<PathfindingManager.Node> nodes) {
	    PathfindingManager.Node returnNode = nodes[0];
	    for (int i = 1; i < nodes.Count; i++) {
	    	if (nodes[i].f < returnNode.f)
                returnNode = nodes[i];
	    }
	    return returnNode;
    }

    float distanceBetweenNodes(ref PathfindingManager.Node node1, ref PathfindingManager.Node node2) {
	    return Mathf.Pow((node1.posX - node2.posX), 2) + Mathf.Pow((node1.posY - node2.posY), 2);
    }

    public void FindRoute()
    {
        List<PathfindingManager.Node> open = new List<PathfindingManager.Node>();
        List<PathfindingManager.Node> closed = new List<PathfindingManager.Node>();
        List<PathfindingManager.Node> finalNodes = new List<PathfindingManager.Node>();

        // Add the start and target node
        PathfindingManager.Node start = new PathfindingManager.Node(lManager.startX, lManager.startY);
        open.Add(start);
        PathfindingManager.Node target = new PathfindingManager.Node(lManager.endX, lManager.endY);

        // Loop until you find the end
        while (open.Count > 0)
        {
            // Get the current node
            PathfindingManager.Node currentNode = getLowestF(ref open);
            pManager.RemoveNode(ref open, ref currentNode);
            closed.Add(currentNode);

            if (pManager.InList(ref closed, ref target))
            {
                target = pManager.GetNodeInList(ref closed, ref target);
                break;
            }

            // Generate children
            List<PathfindingManager.Node> children = pManager.GetAdjacent(currentNode);

            for (int i = 0; i < children.Count; i++)
            {
                //Check if already checked
                PathfindingManager.Node currentChild = children[i];
                if (pManager.InList(ref closed, ref currentChild))
                    continue;

                //Create values
                children[i].g = currentNode.g + distanceBetweenNodes(ref currentNode, ref currentChild); // Uniform-Cost (Backwards Cost)
                children[i].h = distanceBetweenNodes(ref currentChild, ref target);                      // Greedy (Forward Cost)
                children[i].f = children[i].g + children[i].h;                                           // A* Combines Uniform-cost and Greedy

                //Check if child is already in open
                if (pManager.InList(ref open, ref currentChild))
                {
                    PathfindingManager.Node nodeInList = pManager.GetNodeInList(ref open, ref currentChild);
                    if (children[i].g > nodeInList.g)
                        continue;
                }

                //Add Child to Open
                open.Add(children[i]);
            }
        }

        bool end = false;
        while (!end)
        {
            finalNodes.Add(target);

            if (target.Equals(start))
                end = true;
            else
            {
                PathfindingManager.Node parent = new PathfindingManager.Node(target.parentX, target.parentY);
                target = pManager.GetNodeInList(ref closed, ref parent);
            }
        }

        pManager.DrawRoute(ref marker, ref finalNodes);
    }
}
