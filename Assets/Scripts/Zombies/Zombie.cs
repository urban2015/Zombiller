using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    //Calculation Variables
    [Header("Zombie Behaviour Stats")]

    [SerializeField] private Transform _target;
    [SerializeField] float _walkSpeed = 2f;
    [SerializeField] float _runSpeed = 4f;
    [SerializeField] float _stopDistance = 15f;
    [SerializeField] float _walkDistance = 14f;
    [SerializeField] float _runDistance = 6f;
    [SerializeField] bool canFollow = true;


    //Custom classes Referece
    private readonly NumberOfZombies _numberOfZombies = new NumberOfZombies();

    //Reference variables
    private float _distanceFromTarget = Mathf.Infinity;

    // Component variables
    NavMeshAgent navMeshAgent;
    Rigidbody zombieRigidbody;
    bool isProvoked = false;

    private void Start()
    {
        zombieRigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        _target =  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _numberOfZombies.OnEnabled();
    }

    private void OnDisable()
    {
        _numberOfZombies.OnDisabled();
    }

    void Update()
    {
        if (canFollow)
        {
            ProcessZombieBehaviour();
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    void ProcessZombieBehaviour()
    {
        _distanceFromTarget = Vector3.Distance(_target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        // Within walk and run range --> engage target
        if (_distanceFromTarget <= _stopDistance)
        {
            isProvoked = true;
        }
        // Too far from target --> stop moving
        if (_distanceFromTarget > _stopDistance)
        {
            isProvoked = false;
            navMeshAgent.isStopped = true;
        }
    }

    void EngageTarget()
    {
        // within enemy range but not so near
        if (_distanceFromTarget > _runDistance && _distanceFromTarget <= _walkDistance)
        {
            ChaseTarget(_target, _walkSpeed);
        }
        else if (_distanceFromTarget <= _runDistance)
        {
            ChaseTarget(_target, _runSpeed);
        }
    }


    private void ChaseTarget(Transform target, float speed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.position);
    }
}


/// <summary>
/// Thulk Code
/// </summary>
public class NumberOfZombies //Handles the number of enemies on scene. 
{
    public readonly static List<NumberOfZombies> Enemies = new List<NumberOfZombies>();
    public static int EnemiesInScene => Enemies.Count;
    public void OnEnabled() => Enemies.Add(this);
    public void OnDisabled() => Enemies.Remove(this);

    public int GetEnemiesInScene() => EnemiesInScene;
}