using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Calculation Variables
    [SerializeField] private float _zombieSpeed = 1;
    private float _distanceToStopFromPlayer = .5f;

    //Custom classes Referece
    private FollowPlayer _zombie = new FollowPlayer();
    private NumberOfZombies _numberOfZombies = new NumberOfZombies();

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
        _zombie.StopDistance(_player, transform, _distanceToStopFromPlayer, _zombieSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}


class FollowPlayer //Handles the following of the player and stoping at certain distance
{
    private Vector2 StartFollowing(Transform zombie, Transform player, float zombieSpeed)
    {
       Vector2 _follow = Vector2.MoveTowards(zombie.position, player.position, zombieSpeed * Time.deltaTime);
        return _follow;
    }

    public void StopDistance(Transform player, Transform zombie, float distanceToStop, float zombieSpeed)
    {
        if (Vector2.Distance(zombie.position, player.position) > distanceToStop)
        {
            zombie.position = StartFollowing(zombie, player, zombieSpeed);
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