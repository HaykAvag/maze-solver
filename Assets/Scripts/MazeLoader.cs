using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Gameplay))]
public class MazeLoader : MonoBehaviour
{
    [SerializeField] private TMP_InputField mazeInput;
    [SerializeField] private TMP_Text errorMessage;
    [SerializeField] private Toggle rawLevel;

    private Gameplay gameplay;

    private void Awake()
    {
        gameplay = GetComponent<Gameplay>();
    }

    private int[,] ParseMaze(string text)
    {
        // Remove \r from \r\n carriage return.
        text = text.Replace("\r", "");
        string[] maze = text.Split('\n');

        if (!rawLevel.isOn)
        {
            /*
                Because the maze is uneven(for example 2 spaces under 2 hyphens indicates one cell),
                delete one column of characters from each cell.
            */
            for (int y = 0; y < maze.Length; y++)
            {
                for (int x = 1; x < maze[y].Length; x += 2)
                {
                    maze[y] = maze[y].Remove(x, 1);
                }
            }
        }

        int[,] parsedMaze = new int[maze.Length, maze[0].Length];

        for (int y = 0; y < maze.Length; y++)
        {
            if (maze[y].Length != maze[0].Length)
            {
                throw new Exception("Inconsistent Row Width.");
            }

            for (int x = 0; x < maze[y].Length; x++)
            {
                parsedMaze[y, x] = maze[y][x] == ' ' ? 0 : 1;
            }
        }

        return parsedMaze;
    }

    public void LoadMaze()
    {
        int[,] parsedMaze;

        try
        {
            parsedMaze = ParseMaze(mazeInput.text);
        }
        catch (Exception e)
        {
            errorMessage.text = e.Message;
            return;
        }

        int height = parsedMaze.GetLength(0);
        int width = parsedMaze.GetLength(1);
        int start = parsedMaze[1, 0];
        int end = parsedMaze[height - 2, width - 1];

        if (start == 1 || end == 1)
        {
            errorMessage.text = "Start or End Not Found";
            return;
        }

        List<Node> path = MazeSolver.SolveMaze(parsedMaze);

        if (!path.Any())
        {
            errorMessage.text = "Maze Impossible to Solve";
            return;
        }

        gameplay.Setup(path, parsedMaze);
    }
}
