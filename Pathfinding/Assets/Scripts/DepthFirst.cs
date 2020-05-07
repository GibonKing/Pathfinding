using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DepthFirst : MonoBehaviour
{
    public LevelManager lManager;
    public PathfindingManager pManager;
    public GameObject marker;

    public void FindRoute()
    {
        List<PathfindingManager.Node> visited = new List<PathfindingManager.Node>();
        List<PathfindingManager.Node> toVisit = new List<PathfindingManager.Node>();
        List<PathfindingManager.Node> finalNodes = new List<PathfindingManager.Node>();

        // Add the start and target node
        PathfindingManager.Node start = new PathfindingManager.Node(lManager.startX, lManager.startY);
        toVisit.Add(start);
        PathfindingManager.Node target = new PathfindingManager.Node(lManager.endX, lManager.endY);

        while (toVisit.Count > 0)
        {
            PathfindingManager.Node currentNode = toVisit.Last();
            pManager.RemoveNode(ref toVisit, ref currentNode);
            visited.Add(currentNode);

            if (currentNode.Equals(target))
            {
                target = pManager.GetNodeInList(ref visited, ref target);
                break;
            }

            // Generate children
            List<PathfindingManager.Node> children = pManager.GetAdjacent(currentNode);

            for (int i = 0; i < children.Count; i++)
            {
                PathfindingManager.Node currentChild = children[i];
                if (pManager.InList(ref visited, ref currentChild) || pManager.InList(ref toVisit, ref currentChild))
                {
                    continue;
                }
                else
                {
                    toVisit.Add(children[i]);
                }
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
                target = pManager.GetNodeInList(ref visited, ref parent);
            }
        }

        pManager.DrawRoute(ref marker, ref finalNodes);
    }
}
