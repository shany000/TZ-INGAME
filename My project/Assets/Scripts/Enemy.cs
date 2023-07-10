using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health;
    public int Speed;
    public int AttackPower;

    [Range (0f, 1f)]
    public float Protection;

    private EnemyController EC;
    private NavMeshAgent agent;

    public void StartAttack()
    {   
        EC = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyController>();
        agent = GetComponent<NavMeshAgent>();;
        agent.speed = Speed/10;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while(GameManager.isGame)
        {
            agent.SetDestination(EC.gameObject.transform.position);
            yield return new WaitForEndOfFrame();
        }
        agent.SetDestination(transform.position);
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
        EC.EnemyDead(this);
    }
}
    


