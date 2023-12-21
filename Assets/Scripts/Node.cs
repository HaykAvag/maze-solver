using UnityEngine;

public class Node
{
    public Node Parent;
    public Vector2 Position;
    // G cost + H cost
    public float F;
    // Distance from starting node.
    public float G;
    // Distance from end node.
    public float H;
}