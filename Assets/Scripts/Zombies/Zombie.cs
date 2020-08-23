using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Calculation Variables
    [SerializeField] private float zombieSpeed = 1;
    private float _distanceToStopFromPlayer = 0.5f;

    //Custom classes Referece
    private readonly ZombieBehavior_Follow _zombie = new ZombieBehavior_Follow();
    private readonly NumberOfZombies _numberOfZombies = new NumberOfZombies();

    //Reference to player
    private Transform _player;

    private void Start()
    {
        _player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        _zombie.StopDistance(_player, transform, _distanceToStopFromPlayer, zombieSpeed);
        _zombie.LookAt(_player, transform);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
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
}