using System.Collections.Generic;
using UnityEngine;
public class MazeGeneratedPoint
{
    public int X;
    public int Y;

    public bool wallLeft = true;
    public bool wallBottom = true;
    public bool wallFloor = true;

    public bool isVisited = false;
}

public class MazeGenerator
{
    public int Width;
    public int Height;

    public void SetSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public MazeGeneratedPoint[,] GanerateMaze()
    {
        MazeGeneratedPoint[,] maze = new MazeGeneratedPoint[Width, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratedPoint { X = x, Y = y };
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].wallLeft = false;
            maze[x, Height - 1].wallFloor = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].wallBottom = false;
            maze[Width - 1, y].wallFloor = false;
        }

        RemoveWallsWithBackTracker(maze);

        return maze;
    }

    private void RemoveWallsWithBackTracker(MazeGeneratedPoint[,] maze)
    {
        MazeGeneratedPoint targetPoint = maze[0, 0];
        targetPoint.isVisited = true;

        Stack<MazeGeneratedPoint> stack = new Stack<MazeGeneratedPoint>();

        do
        {
            List<MazeGeneratedPoint> unvisiteNeighbours = new List<MazeGeneratedPoint>();

            int x = targetPoint.X;
            int y = targetPoint.Y;

            if (x > 0 && !maze[x - 1, y].isVisited)
            {
                unvisiteNeighbours.Add(maze[x - 1, y]);
            }
            if (y > 0 && !maze[x, y - 1].isVisited)
            {
                unvisiteNeighbours.Add(maze[x , y - 1]);
            }
            if (x < Width - 2 && !maze[x + 1, y].isVisited)
            {
                unvisiteNeighbours.Add(maze[x + 1, y]);
            }
            if (y < Height - 2 && !maze[x, y + 1].isVisited)
            {
                unvisiteNeighbours.Add(maze[x, y + 1]);
            }

            if (unvisiteNeighbours.Count > 0)
            {
                MazeGeneratedPoint chosen = unvisiteNeighbours[Random.Range(0, unvisiteNeighbours.Count)];
                RemoveWall(targetPoint, chosen);

                chosen.isVisited = true;
                targetPoint = chosen;
                stack.Push(chosen);
            }
            else 
            {
                targetPoint = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratedPoint firstPoint, MazeGeneratedPoint secondPoint)
    {
        if(firstPoint.X == secondPoint.X)
        {
            if (firstPoint.Y > secondPoint.Y) firstPoint.wallBottom = false;
            else secondPoint.wallBottom = false; 
        }
        
        if(firstPoint.Y == secondPoint.Y)
        {
            if (firstPoint.X > secondPoint.X) firstPoint.wallLeft = false;
            else secondPoint.wallLeft = false;
        }
    }

    
}
