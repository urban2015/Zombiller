using UnityEngine;

class ZombieBehavior_Follow
{
    public Vector3 StartFollowing(Transform zombie, Transform player, float zombieSpeed)
    {
        Vector3 _follow = Vector3.MoveTowards(zombie.position, player.position, zombieSpeed * Time.deltaTime);
        return _follow;
    }

    public void StopDistance(Transform player, Transform zombie, float distanceToStop, float zombieSpeed)
    {
        if (Vector3.Distance(zombie.position, player.position) > distanceToStop)
        {
            zombie.position = StartFollowing(zombie, player, zombieSpeed);
        } else {
            //ATTACK GOES HERE
        }
    }

    public void LookAt(Transform target, Transform zombie){
        Vector3 direction = target.position - zombie.position;
        float turnAngle = -(Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + 90f);
        zombie.eulerAngles = new Vector3(0, turnAngle, 0);
    }
}