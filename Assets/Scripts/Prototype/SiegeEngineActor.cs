using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITarget
{
    Transform transform { get; }
    void DecreaseHealth(int value);
}

public class SiegeEngineActor : MonoBehaviour, ITarget
{

    private SiegeEngineData _data;

    private SiegeEngineLine _line;

    private int _nowHealth;

    Building _target;

    float _nowTime = 0;

    public void SetData(SiegeEngineData data, SiegeEngineLine line)
    {
        _data = data;
        _line = line;
    }

    public void Process()
    {
        if (_target == null)
        {
            _target = GameManager.Current.FindBuilding(this);
            _nowTime = 0;
        }
        else
        {
            _nowTime += Time.deltaTime;
            if (_nowTime > _data.GetAttackTime(_line))
            {
                GameManager.Current.CreateBullet(transform.position, _target, _data.GetAttackDamage(_line));
                _nowTime = 0;
            }
        }
    }


    public void Activate()
    {
        _nowHealth = _data.GetHealth(_line);
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
