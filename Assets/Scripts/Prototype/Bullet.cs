using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    ITarget _target;
    int _damage;
    Vector2 _originPos;
    Vector2 _targetPos;

    private System.Action<ITarget, Bullet> _callback;

    public void SetData(Vector2 originPos, ITarget target, int damage, System.Action<ITarget, Bullet> callback)
    {
        _target = target;
        _targetPos = target.transform.position;
        _damage = damage;
        _originPos = originPos;
        _callback = callback;
    }

    public void Activate()
    {
        transform.position = _originPos;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, 0.05f);
        if (Vector2.Distance(_targetPos, transform.position) < 0.1f)
        {
            _target.DecreaseHealth(_damage);
            _callback?.Invoke(_target, this);
            DestroyImmediate(gameObject);
        }
    }
}
