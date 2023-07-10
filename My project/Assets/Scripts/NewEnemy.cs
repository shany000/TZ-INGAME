using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemy : MonoBehaviour
{
    public float Health;
    public int Speed;
    public int AttackPower;

    public GameObject cube;

    [Range (0f, 1f)]
    public float Protection;

    //private EnemyController ec;
    private NavMeshAgent agent;

    void Start()
    {
        //ec = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyController>();
        agent = GetComponent<NavMeshAgent>();
        StartGame();
        agent.speed = Speed/10;
    }

    public void StartGame()
    {
        //agent.SetDestination(ec.gameObject.transform.position);
    }

    public void PausGame()
    {

    }

    public void GameOver()
    {

    }

    public void Damage(int damage)
    {
        Health -= damage*(1-Protection);
        if(Health<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() 
    {
        //ec.EnemyDead();
    }
}
