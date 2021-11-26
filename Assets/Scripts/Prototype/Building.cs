using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ITarget
{
    [SerializeField]
    private int _health;

    private int _nowHealth;

    [SerializeField]
    private float _attackTime;

    private float _nowTime;

    SiegeEngineActor _target;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private int _gold;

    private int _level = 1;

    public int gold => _gold + _level;


    public void Process()
    {
        if (_target == null)
        {
            _target = GameManager.Current.FindSiegeEngine(this);
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

    public void SetData(int level, int wave)
    {
        _level = level;
        _nowHealth = _health * level + (wave * level * 10);
    }

    public void Activate()
    {
        RefreshHealth();
        gameObject.SetActive(true);
    }

    public void DecreaseHealth(int value)
    {
        if (_nowHealth - value < 0)
            _nowHealth = 0;
        else
            _nowHealth -= value;

        RefreshHealth();

        if (_nowHealth == 0)
        {
            GameManager.Current.RemoveBuilding(this);
            try
            {
                DestroyImmediate(gameObject);
            }
            catch
            {

            }
        }
    }

    private void RefreshHealth()
    {
        _hitEvent?.Invoke(_nowHealth);
    }

    #region ##### Listener #####

    private System.Action<int> _hitEvent;

    public void SetOnHitEvent(System.Action<int> act) => _hitEvent = act;

    #endregion
}
