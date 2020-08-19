using System.Collections;
using UnityEngine;

public abstract class SpawnSystem : MonoBehaviour
{
    [Tooltip("Prefab Reference")]
    [SerializeField] protected GameObject _prefab;

    [Tooltip("The lower the Spawn Rate value, the faster it is ")]
    [SerializeField] protected float _spawnRate;

    //Claculation Variables
    [SerializeField] protected int _ScreenYLimit;
    [SerializeField] protected int _ScreenXLimit;
    static bool gameisActive = true;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (gameisActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            Vector2 randomPositionY = new Vector2(Random.Range(_ScreenXLimit, -_ScreenXLimit), Random.Range(_ScreenYLimit, -_ScreenYLimit));
            Instantiate(_prefab, randomPositionY, _prefab.transform.rotation);
        }
    }
}
