using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int EnemyAmount = 10;
    
    public int EnemySpawnDistance = 12;

    public Enemy[] Enemys;

    private List<Enemy> enemysOnScene = new List<Enemy>{};

    public void Spawn()
    {
        for(int i = EnemyAmount;i>0;i--)
        {
            SpawnEnemy();
        }
    }
    
    private void SpawnEnemy()
    {
        if(GameManager.isGame)
        {
            Vector2 randomPoint = (new Vector2(transform.position.x,transform.position.z) + new Vector2(Random.value-0.5f,Random.value-0.5f).normalized*EnemySpawnDistance);
            if(randomPoint.x <50 && randomPoint.x > - 50 && randomPoint.y <50 && randomPoint.y > -50)
            {
                Enemy enemy = Instantiate(Enemys[Random.Range(0,Enemys.Length)],new Vector3(randomPoint.x,1,randomPoint.y),Quaternion.identity).GetComponent<Enemy>();
                enemy.StartAttack();
                enemysOnScene.Add(enemy);
            }
            else
            {
                SpawnEnemy();
            }
        }
    } 


    public void EnemyDead(Enemy gm)
    {
        enemysOnScene.Remove(gm);
        SpawnEnemy();
    }
}
