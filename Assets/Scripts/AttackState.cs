using RobustFSM.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoState
{
    private GameObject _proj;
    private AI_Mimic _mimic;
    private float _timer;

    public override void OnEnter()
    {
        base.OnEnter();
    }

    private void Start()
    {
        _proj = _mimic._projectile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_mimic.transform.position, _mimic._player.position) < 10f)
        {
            _timer -= Time.deltaTime;

            if (_timer > 0)
                return;

            _timer = 2f;

            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_proj, _mimic._fireLocation.transform.position, _mimic.transform.rotation);
        Rigidbody bulletRig = bullet.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * 10f);
        Destroy(bullet, 0.1f);
    }
}
