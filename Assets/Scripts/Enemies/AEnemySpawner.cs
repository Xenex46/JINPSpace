using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private ASpawnerConfig<T> m_Config = null;

    [SerializeField]
    private List<Transform> spawnPoints = null;

    private float m_TimeSinceStart = 0f;
    private float m_Timer = 0f;

    private void Start()
    {
        m_Timer = Time.time;
        m_TimeSinceStart = Time.time;
        for (double i = -5f; i <= 5f; i += 1f)
        {
            //Debug.Log("stagePercent = " + i +" "+ NormalDist(i)); 
        }
    }

    private void FixedUpdate()
    {
        print(Time.time);


        double time = Time.timeSinceLevelLoad;

        double offSet = m_Config.SpawnBeginOffsetSeconds;

        if (time < offSet)
        {
            m_Timer = 0f;
            return;
        }

        if (time - offSet > m_Config.SecondsActive && !m_Config.IsEndgame)
        {
            return;
        }

        if (ShouldSpawn(Time.time - m_Timer))
        {
            m_Timer = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        T randomEnemyPrefab = m_Config.GetRandomEnemy();

        if (randomEnemyPrefab == null)
        {
            return;
        }

        PrefabPool<T> pool = PoolManager.Instance.GetPool(randomEnemyPrefab);
        T enemy = pool.Get();

        Transform t = GetRandomPos();

        if (t == null)
        {
            return;
        }

        SetupEnemy(enemy, t.position);
    }

    private double NormalDist(double x)
    {
        //-4 to 4 has positive values up to 0.4 (from 0.0001)
        double miu = 0.0f;
        double sigma = Mathf.Sqrt(5.0f);

        double expo = -0.5f * ((x - miu) / sigma)*((x - miu) / sigma);

        return (1.0f / (sigma * Mathf.Sqrt(2.0f * Mathf.PI))) * Mathf.Exp((float)expo);

    }

    private double FixedDist(double x)
    {
        // 0 - 100 has positive values up to 0.5

        double flatness = m_Config.Flatness;

        return NormalDist((2f*flatness) * x/100.0f -  flatness) * 1f;
    }

    private double EndgameInterval(double x)
    {
        x /= 100f;

        return 1f / (m_Config.EndgameHardness * x);
    }



    private bool ShouldSpawn(float timer)
    {
        double time = Time.time - m_TimeSinceStart;

        time = Mathf.Max(0f, (float)time - m_Config.SpawnBeginOffsetSeconds);

        double stagePercent = time / m_Config.SecondsActive * 100.0f;

        double interval = 100f;

        if (m_Config.IsEndgame)
        {
            interval = EndgameInterval(stagePercent);
        }
        else
        {
            interval = 9f - FixedDist(stagePercent) * m_Config.SpawnIntervalMultiplier;
        }

        if (timer >= interval)
        {
            //Debug.Log("stagePercent = " + stagePercent);

            Debug.Log("interval = " + interval);

            Debug.Log("timer = " + timer);
        }

        return (timer >= interval);

    }

    private Transform GetRandomPos()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }

    protected abstract void SetupEnemy(T enemy, Vector3 pos);
}
