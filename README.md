# maze-solver
This Unity game takes in an ASCII maze, finds a path from the start to the end and animates a character through that path.
The maze must have the start be on the top left and the end be on the bottom right.
A space character is considered a walkable space, and anything else is a wall.
The mazes must be the same format as the example maze, and the maze loader will remove the double horizontal path spacing.
If needed, mazes can be loaded raw with a toggle, without any modification.

## Example Maze
+--+--+--+--+--+--+--+--+--+--+
                  |        |  |
+--+--+  +--+--+  +  +--+  +  +
|     |  |  |     |  |     |  |
+  +  +  +  +  +--+--+  +--+  +
|  |  |  |  |        |     |  |
+  +--+  +  +--+--+  +  +  +  +
|           |     |     |     |
+--+--+  +--+  +  +--+--+--+  +
|     |     |  |  |        |  |
+  +  +  +  +  +  +  +--+  +  +
|  |     |  |  |     |     |  |
+  +--+--+  +  +--+--+  +--+  +
|        |     |  |     |     |
+--+--+  +--+--+  +  +--+--+--+
|           |  |     |        |
+  +--+--+  +  +--+--+  +--+  +
|        |     |     |  |     |
+--+--+  +  +--+  +  +  +  +--+
|        |        |     |      
+--+--+--+--+--+--+--+--+--+--+