using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Point _point;

    [Header("Size")]
    [SerializeField] private int _widthOfMaze;
    [SerializeField] private int _heightOfMaze;

    [Header("Finish")]
    [SerializeField] private GameObject _finishPoint;
    [SerializeField] private Vector3 _offsetOfFinishPoint;

    [Header("TrapZone")]
    [SerializeField] private int _targetCountOfTrap;
    private int _countOfTrap = 0;

    [Header("Player Spawn Position In Matrix System")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private int _xCoordinate;
    [SerializeField] private int _yCoordinate;

    private Vector3 _pointSize = new Vector3(1, 0, 1);
    private MazeGeneratedPoints[,] maze;
    private Trap _trap;
    private NavMeshSurface _navMesh;

    private Point _spawnPlayerPoint;

    private int _spawnTrapChance = 10;

    public UnityEvent<GameObject> _finishPointSpawned;

    private void Awake()
    {
        GenerateLevel();
    }

    private void Start()
    {
        //_trap = FindObjectOfType<Trap>().GetComponent<Trap>();
        //_trap.playerDied.AddListener(OnPlayedDied);
    }

    private void GenerateLevel()
    {
        SpawnMaze();
        _navMesh.BuildNavMesh();
        SpawnPlayer(_spawnPlayerPoint);
    }
    

    private void OnDisable()
    {
        //_trap.playerDied.RemoveListener(OnPlayedDied);
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

                if (maze[x, y] != maze[0, 0]) TrySetTrapZone(point);

                if (maze[x, y] == maze[_xCoordinate, _yCoordinate]) _spawnPlayerPoint = point;


                if (x == maze.GetLength(0) - 1 && y == maze.GetLength(1) - 1)
                {
                    GameObject finish = Instantiate(_finishPoint, point.transform.position - _offsetOfFinishPoint, Quaternion.identity);
                    _finishPointSpawned?.Invoke(finish);
                }

                point.leftWall.SetActive(maze[x, y].wallLeft);
                point.bottomWall.SetActive(maze[x, y].wallBottom);
                point.floor.SetActive(maze[x, y].wallFloor);
            }
        }
    }

    private void TrySetTrapZone(Point point)
    {
        if (Random.Range(0,100) < _spawnTrapChance && _countOfTrap < _targetCountOfTrap)
        {
            point.floor.GetComponent<Trap>()._isTrapZone = true;
            _countOfTrap++;
        } 
    }

    private void SpawnPlayer(Point position)
    {
        if(position != null)
        {
            Player player = Instantiate(_playerPrefab, position.transform.position, Quaternion.identity);
        }
    }

    private void OnPlayedDied()
    {
        SpawnPlayer(_spawnPlayerPoint);
    }
}
