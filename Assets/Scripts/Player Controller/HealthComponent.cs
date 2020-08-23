using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Thulk Code, 
/// </summary>
class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float hpRegeneratedPerSecond;
    [SerializeField] private float healthPackRate;
    public static float HealthPackHpToAdd = 0;
    public Health Health;
    private void Start()
    {
        Health = new Health(maxHp, maxHp);
        StartCoroutine(RegenHealth());
    }
    
    private IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Health.IsDepleted) yield break;
            if (HealthPackHpToAdd > 0){
                if (HealthPackHpToAdd > healthPackRate){
                    HealthPackHpToAdd -= healthPackRate;
                    Health = Health.Regenerate(healthPackRate); 
                } else {
                    Health = Health.Regenerate(HealthPackHpToAdd);
                    HealthPackHpToAdd = 0;
                }
            }
            Health = Health.Regenerate(hpRegeneratedPerSecond);
        }
    }
}

public readonly struct Health
{
    public readonly float Current;
    public readonly float Max;
    public bool IsDepleted => Current <= 0f;
    public Health(float current, float max)
    {
        Current = Math.Max(0, Math.Min(current, max)); // Can also be writtend as Math.Clamp(current, 0, max);
        Max = max;
    }
    public Health DamagedBy(float damage) => new Health(Current - damage, Max);
    public Health Regenerate(float regen) => new Health(Current + regen, Max);
}