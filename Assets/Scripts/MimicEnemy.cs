using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MimicEnemy : MonoBehaviour
{
    NavMeshAgent _agent;

    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private GameObject _bullet;
    private int magazineCount = 5;
    private float shootDelay = 0.5f;

    private float patrolTime = 5f;

    [SerializeField] private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frames
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) < 20f)
        {
            if (magazineCount <= 0 && !_agent.hasPath)
            {
                Retreat();
            }
            else if (magazineCount > 0)
            {
                Attack();
            }
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            Search();
        }

        else
            Patrol();
    }

    void Patrol()
    {
        patrolTime -= Time.deltaTime;

        if (patrolTime > 0)
            return;

        patrolTime = 5f;

        Vector3 randomDirection = Random.insideUnitSphere * 21f;

        randomDirection += transform.position;
        
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 5, 1);
        
        Vector3 finalPosition = hit.position;

        if (!_agent.hasPath)
            _agent.SetDestination(finalPosition);
    }

    void Search()
    {
        _agent.SetDestination(_player.position);
    }

    void Attack()
    {
        var lookPos = _player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 25f);


        shootDelay -= Time.deltaTime;

        if (shootDelay > 0)
            return;

        shootDelay = 0.5f;

        Debug.Log("Shooting");

        GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, _bulletSpawn.transform.rotation);
        
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletRB.transform.forward * 100f);

        magazineCount--;
        Destroy(bullet, 10f);
    }

    void Retreat()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 15;

        randomDirection += transform.position;

        Debug.Log("Checking Distance");

        if ((Vector3.Distance(randomDirection, _player.position) > Vector3.Distance(transform.position, _player.position)) && (!_agent.hasPath || !_agent.pathPending))
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5, 1);

            Vector3 finalPosition = hit.position;

            _agent.SetDestination(finalPosition);

            Debug.Log("Valid Retreat");

            Invoke("Reload", 2f);
        }
    }

    void Reload()
    {
        magazineCount = 5;
    }
}
