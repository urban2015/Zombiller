using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public class NumberOfEntities //Handles the number of enemies on scene. 
    {
        public readonly static List<NumberOfEntities> Enemies = new List<NumberOfEntities>();
        public static int EnemiesInScene => Enemies.Count;
        public void OnEnabled() => Enemies.Add(this);
        public void OnDisabled() => Enemies.Remove(this);
    }
}
