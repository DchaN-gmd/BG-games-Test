using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Point _point;

    [Header("Size")]
    [SerializeField] private int _widthOfMaze;
    [SerializeField] private int _heightOfMaze;

    [SerializeField] private GameObject _finishPoint;
    [SerializeField] private Vector3 _offsetOfFinishPoint;

    private Vector3 _pointSize = new Vector3(1, 0, 1);
    private MazeGeneratedPoint[,] maze;

    private void Start()
    {
        MazeGenerator _generator = new MazeGenerator();

        _generator.SetSize(_widthOfMaze, _heightOfMaze);

        maze = _generator.GanerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                // Перевод 2-ух мерной матричной системы в 3D
                Point point = Instantiate(_point, new Vector3(x * _pointSize.x, y * _pointSize.y, y * _pointSize.z), Quaternion.identity).GetComponent<Point>();
                if (x == maze.GetLength(0) - 1 && y == maze.GetLength(1) - 1)
                {
                    Instantiate(_finishPoint, point.transform.position - _offsetOfFinishPoint, Quaternion.identity);
                    point.isFinishPoint = true;
                }

                point.leftWall.SetActive(maze[x, y].wallLeft);
                point.bottomWall.SetActive(maze[x, y].wallBottom);
                point.floor.SetActive(maze[x, y].wallFloor);
            }
        }
    }

    private void SearchRoute()
    {
        int countStep = 0;

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {

            }
        }
    }
}
