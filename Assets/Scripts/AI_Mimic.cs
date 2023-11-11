using Assets.SimpleFSM.Demo.Scripts;
using RobustFSM.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mimic : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    public int Speed => _speed;

    [SerializeField]
    private List<Transform> _patrolPoints;
    public List<Transform> PatrolPoints => _patrolPoints;

    [SerializeField]
    public Transform _player;
    public GameObject _projectile;
    public Transform _fireLocation;

    public CharacterFSM FSM { get; set; }
    public Transform Target { get; set; }

    private void Start()
    {
        FSM = GetComponent<CharacterFSM>();
    }
}
