using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float _timeToStart;
    [SerializeField] private float _timeToResetProtect;

    [Header("Colors")]
    [SerializeField] private Material _targetColorOfProtect;
    [SerializeField] private Material _defaultPlayerColor;

    private NavMeshAgent _navMeshOfPlayer;
    private Renderer _playerColor;
    private MazeSpawner _mazeSpawner;

    private Vector3 _finishPoint;
    private float _timer;
    private bool _isStart = false;

    public bool _isProtected = false;

    private void Awake()
    {
        _navMeshOfPlayer = GetComponent<NavMeshAgent>();
        _playerColor = GetComponent<Renderer>();
        _mazeSpawner = FindObjectOfType<MazeSpawner>().GetComponent<MazeSpawner>();
    }

    private void OnEnable()
    {
        _mazeSpawner._finishPointSpawned.AddListener(OnFinishSpawned);
    }

    private void OnDisable()
    {
        _mazeSpawner._finishPointSpawned.RemoveListener(OnFinishSpawned);      
    }

    private void Update()
    {
        ControlTimer();
    }

    public void ProtectPlayer()
    {
        _isProtected = true;
        ChangeColor();
    }

    public void UnprotectPlayer()
    {
        _isProtected = false;
        _timer = 0;
        ChangeColor();
    }

    private void ControlTimer()
    {
        if (!_isStart) _timer += Time.deltaTime;

        if (_timer >= _timeToStart)
        {
            _isStart = true;
            _timer = 0;
        }

        if (_isStart)
        {
            _navMeshOfPlayer.SetDestination(_finishPoint);
        }

        if (_isProtected)
        {
            _timer += Time.deltaTime;
            ChangeColor();
        }

        if (_timer >= _timeToResetProtect && _isStart)
        {
            _isProtected = false;
            _timer = 0;
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        if (_isProtected) _playerColor.material.color = _targetColorOfProtect.color;
        if (!_isProtected) _playerColor.material.color = _defaultPlayerColor.color;
    }

    private void OnFinishSpawned(GameObject finish)
    {
        _finishPoint = finish.transform.position;
    }

}
