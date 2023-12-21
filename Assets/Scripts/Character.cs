using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    int targetPositionIndex = 1;
    Vector2[] walkPositions;
    bool canWalk = false;

    public void Setup(List<Node> path)
    {
        walkPositions = new Vector2[path.Count];

        for (int i = 0; i < path.Count; i++)
        {
            Vector2 position = path[i].Position;
            walkPositions[i] = new Vector2(position.x, -position.y);
        }

        canWalk = true;
    }

    void Update()
    {
        if (!canWalk) return;

        Vector2 targetPosition = walkPositions[targetPositionIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            targetPositionIndex++;

            if (targetPositionIndex == walkPositions.Length)
            {
                canWalk = false;
            }
        }
    }
}