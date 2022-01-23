using UnityEngine;
public class WaspSpawner : AEnemySpawner<WaspMovement>
{
    protected override void SetupEnemy(WaspMovement enemy, Vector3 pos)
    {
        enemy.Reset(pos);
    }
}
