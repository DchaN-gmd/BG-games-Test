using UnityEngine;
using UnityEngine.AI;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Point _point;

    [Header("Size")]
    [SerializeField] private int _widthOfMaze;
    [SerializeField] private int _heightOfMaze;

    [SerializeField] private GameObject _finishPoint;
    [SerializeField] private Vector3 _offsetOfFinishPoint;

    [SerializeField] private int _targetCountOfTrap;
    private int _countOfTrap = 0;

    private Vector3 _pointSize = new Vector3(1, 0, 1);
    private MazeGeneratedPoints[,] maze;
    private NavMeshSurface _navMesh;

    private int _spawnTrapChance = 10;

    public GameObject GetFinishPoint {get; private set;}

    private void Awake()
    {
        SpawnMaze();
    }

    private void SpawnMaze()
    {
        MazeGenerator _generator = new MazeGenerator();

        _generator.SetSize(_widthOfMaze, _heightOfMaze);

        maze = _generator.GanerateMaze();

        _navMesh = GetComponent<NavMeshSurface>();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                // Перевод 2-ух мерной матричной системы в 3D
                Point point = Instantiate(_point, new Vector3(x * _pointSize.x, y * _pointSize.y, y * _pointSize.z), Quaternion.identity, gameObject.transform).GetComponent<Point>();

                if(maze[x,y] != maze[0,0]) TrySetTrapZone(point);


                if (x == maze.GetLength(0) - 1 && y == maze.GetLength(1) - 1)
                {
                    GameObject finish = Instantiate(_finishPoint, point.transform.position - _offsetOfFinishPoint, Quaternion.identity);
                    GetFinishPoint = finish;
                }

                point.leftWall.SetActive(maze[x, y].wallLeft);
                point.bottomWall.SetActive(maze[x, y].wallBottom);
                point.floor.SetActive(maze[x, y].wallFloor);
            }
        }
        _navMesh.BuildNavMesh();
    }

    private void TrySetTrapZone(Point point)
    {
        if (Random.Range(0,100) < _spawnTrapChance && _countOfTrap < _targetCountOfTrap)
        {
            point.floor.GetComponent<Trap>()._isTrapZone = true;
            _countOfTrap++;
        } 
    }
}
