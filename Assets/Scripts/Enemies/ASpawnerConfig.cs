using System.Collections.Generic;
using UnityEngine;

public abstract class ASpawnerConfig<T> : ScriptableObject where T : APooledObject
{
    public List<T> Enemies = new List<T>();

    public float SpawnIntervalMultiplier = 41.17f;

    public float Flatness = 4f;

    public bool IsEndgame = false;

    public float EndgameHardness = 1f;

    public float SpawnBeginOffsetSeconds = 0;

    public int SecondsActive = 60;

    public T GetRandomEnemy()
    {
        if (Enemies == null || Enemies.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, Enemies.Count);
        return Enemies[randomIndex];
    }
}

