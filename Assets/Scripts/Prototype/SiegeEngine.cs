using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITarget
{
    Transform transform { get; }
    void DecreaseHealth(int value);
}

public class SiegeEngine : MonoBehaviour, ITarget
{
    [SerializeField]
    private int _health;

    private int _nowHealth;

    [SerializeField]
    private float _attackTime;

    [SerializeField]
    private int _attackDamage;


    Building _target;

    float _nowTime = 0;


    private void Start()
    {
        _nowHealth = _health;
    }

    private void Update()
    {
        if (_target == null)
        {
            _target = GameManager.Current.FindBuilding(this);
            _nowTime = 0;
        }
        else
        {
            _nowTime += Time.deltaTime;
            if (_nowTime > _attackTime)
            {
                GameManager.Current.CreateBullet(transform.position, _target, _attackDamage);
                _nowTime = 0;
            }
        }
    }


    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void DecreaseHealth(int value)
    {
        if (_nowHealth - value < 0)
            _nowHealth = 0;
        else
            _nowHealth -= value;

        if (_nowHealth == 0)
        {
            GameManager.Current.RemoveSiegeEngine(this);
            DestroyImmediate(gameObject);
        }
    }
}
