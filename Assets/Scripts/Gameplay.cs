using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject grass;
    [SerializeField] private Character character;
    [SerializeField] private GameObject userInterface;

    private void DrawMaze(int[,] parsedMaze)
    {
        for (int y = 0; y < parsedMaze.GetLength(0); y++)
        {
            for (int x = 0; x < parsedMaze.GetLength(1); x++)
            {
                if (parsedMaze[y, x] == 0)
                {
                    Instantiate(grass, new Vector2(x, -y), Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(wall, new Vector2(x, -y), Quaternion.identity, transform);
                }
            }
        }
    }

    private void FocusCamera(int width, int height)
    {
        Camera.main.transform.position = new Vector3(width / 2f, -height / 2f, -10f);

        float mapHalfHeight = height / 2f;
        float mapHalfWidth = width / 2f;
        float aspectRatio = Screen.width / (float)Screen.height;

        Camera.main.orthographicSize = Mathf.Max(mapHalfWidth / aspectRatio, mapHalfHeight);
    }

    public void Setup(List<Node> path, int[,] parsedMaze)
    {
        int height = parsedMaze.GetLength(0);
        int width = parsedMaze.GetLength(1);

        userInterface.SetActive(false);
        FocusCamera(width, height);
        character.Setup(path);
        DrawMaze(parsedMaze);
    }
}
