using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MazeSolver
{
    private static Node[,] GetNodeGrid(int[,] parsedMaze, int width, int height)
    {
        Node[,] grid = new Node[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (parsedMaze[y, x] == 0)
                {
                    grid[y, x] = new Node { Position = new Vector2(x, y) };
                }
            }
        }

        return grid;
    }

    private static Node[] GetWalkableNeighbors(Node currentNode, Node[,] grid, int width, int height)
    {
        Node[] walkableNeighbors = new Node[4];
        int x = (int)currentNode.Position.x;
        int y = (int)currentNode.Position.y;

        if (y + 1 != height)
            walkableNeighbors[0] = grid[y + 1, x]; // Up
        if (y - 1 > -1)
            walkableNeighbors[1] = grid[y - 1, x]; // Down
        if (x + 1 != width)
            walkableNeighbors[2] = grid[y, x + 1]; // Right
        if (x - 1 > -1)
            walkableNeighbors[3] = grid[y, x - 1]; // Left

        return walkableNeighbors;
    }

    // Uses A* to find a path.
    public static List<Node> SolveMaze(int[,] parsedMaze)
    {
        int height = parsedMaze.GetLength(0);
        int width = parsedMaze.GetLength(1);

        Node[,] grid = GetNodeGrid(parsedMaze, width, height);

        Node startNode = grid[1, 0];
        Node goalNode = grid[height - 2, width - 1];

        startNode.G = 0;
        startNode.H = Vector2.Distance(startNode.Position, goalNode.Position);
        startNode.F = startNode.H;

        List<Node> openList = new() { startNode }; // Set of nodes to be evaluated.
        List<Node> closedList = new(); // Set of nodes already evaluated.
        List<Node> path = new();

        while (openList.Any())
        {
            Node currentNode = openList[0];

            // Find next best node to explore.
            foreach (Node node in openList)
            {
                if (node.F < currentNode.F || node.F == currentNode.F && node.H < currentNode.H)
                {
                    currentNode = node;
                }
            }

            // Stop if goal reached.
            if (currentNode == goalNode)
            {
                Node node = goalNode;

                while (node != null)
                {
                    path.Insert(0, node);
                    node = node.Parent;
                }

                break;
            }

            closedList.Add(currentNode);
            openList.Remove(currentNode);

            foreach (Node neighbor in GetWalkableNeighbors(currentNode, grid, width, height))
            {
                if (neighbor == null || closedList.Contains(neighbor))
                    continue;

                if (!openList.Contains(neighbor))
                {
                    neighbor.G = currentNode.G + 1;
                    neighbor.H = Vector2.Distance(neighbor.Position, goalNode.Position);
                    neighbor.F = neighbor.G + neighbor.H;
                    neighbor.Parent = currentNode;

                    openList.Add(neighbor);
                }
            }
        }

        return path;
    }
}
