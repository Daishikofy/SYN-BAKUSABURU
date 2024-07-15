using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TOWER
{
    public class TOW_Pathfind : MonoBehaviour
    {
        public List<Tilemap> obstacles;
        private TOW_DynamicGrid _grid;

        private void Awake()
        {
            _grid = new TOW_DynamicGrid(obstacles);
        }

        public List<Vector2> ShortestPath(Vector2 initialPosition, Vector2 targetPosition)
        {
            Debug.Log("Start Pathfind");
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();
            List<Vector2> path = new List<Vector2>();

            Node startNode = new Node(new Vector2Int((int) initialPosition.x, (int) initialPosition.y));

            Vector2Int target = new Vector2Int((int) targetPosition.x, (int) targetPosition.y);

            Node currentNode = startNode;
            openNodes.Add(startNode);

            int SECURITY = 0;
            while (openNodes.Count != 0)
            {
                if (SECURITY++ > 1000)
                {
                    Debug.LogWarning("[PATHFIND] Took over 1000 iterations, canceled for overtime.");
                    return path;
                }

                currentNode = openNodes[0];
                openNodes.RemoveAt(0);

                if (currentNode.Position == target)
                {
                    path.Add(currentNode.Position);
                    Vector2Int startPosition = startNode.Position;
                    while (currentNode.Position != startPosition)
                    {
                        currentNode = currentNode.Parent;
                        path.Add(currentNode.Position);
                    }

                    path.Reverse();
                    return path;
                }
                
                
                List<Node> neighbours = GetNeighbours(currentNode, target, _grid);

                foreach (Node neighbour in neighbours)
                {
                    if (!closedNodes.Contains(neighbour))
                    {
                        bool inOpenNode = false;
                        foreach (Node openNode in openNodes)
                        {
                            if (openNode == neighbour)
                            {
                                if (openNode.H >= neighbour.H)
                                {
                                    openNode.G = neighbour.G;
                                    openNode.Parent = neighbour.Parent;
                                }
                                inOpenNode = true;
                            }
                        }

                        if (!inOpenNode)
                        {
                            int index = InsertAtIndex(openNodes, neighbour);
                            if (index < openNodes.Count)
                            {
                                openNodes.Insert(index, neighbour);
                            }
                            else
                            {
                                openNodes.Add(neighbour);
                            }
                        }
                    }
                    closedNodes.Add(currentNode);
                }
            }
            return path;
        }

        private int CompareHeuristic(Node A, Node B)
        {
            if (A.H < B.H)
            {
                return 1;
            }

            if (A.H == B.H)
            {
                return 0;
            }

            return -1;
        }

        private List<Node> GetNeighbours(Node node, Vector2Int targetPosition, TOW_DynamicGrid grid)
        {
            Vector2Int[] adjacentPositions =
            {
                node.Position + Vector2Int.up,
                node.Position + Vector2Int.right,
                node.Position + Vector2Int.down,
                node.Position + Vector2Int.left,
                node.Position + Vector2Int.up + Vector2Int.right,
                node.Position + Vector2Int.up + Vector2Int.left,
                node.Position + Vector2Int.down + Vector2Int.right,
                node.Position + Vector2Int.down + Vector2Int.left,
            };

            List<Node> neighbours = new List<Node>(8);

            foreach (Vector2Int position in adjacentPositions)
            {
                int cellValue = grid.GetCellValue(position);
                if (cellValue < Int32.MaxValue)
                {
                    int distance = (int) Vector2Int.Distance(targetPosition, position);
                    neighbours.Add(new Node(node, position, distance, cellValue));
                }
            }

            return neighbours;
        }

        private int InsertAtIndex(List<Node> list, Node node)
        {
            int startIndex = 0;
            int endIndex = list.Count - 1;
            int midIndex;
            while (startIndex <= endIndex)
            {
                midIndex = startIndex + (endIndex - startIndex) / 2;
                int comparision = CompareHeuristic(node, list[midIndex]);
                if (comparision > 0)
                {
                    endIndex = midIndex - 1;
                }
                else if (comparision < 0)
                {
                    startIndex = midIndex + 1;
                }
                else
                {
                    return midIndex;
                }
            }

            return startIndex;
        }
        
        private class Node
        {
            public readonly Vector2Int Position;

            public int F, G;
            public int H => F + G;
            public Node Parent;

            public Node(Vector2Int position)
            {
                Position = position;
            }

            public Node(Node parent, Vector2Int position, int distance, int value)
            {
                Parent = parent;
                Position = position;
                F = distance;
                G = parent.G + 1 + value;
            }

            public static bool operator ==(Node a, Node b)
            {
                return a.Position == b.Position;
            }

            public override bool Equals(object obj)
            {
                return this == (Node) obj;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator !=(Node a, Node b)
            {
                return !(a.Position == b.Position);
            }
        }
    }
}