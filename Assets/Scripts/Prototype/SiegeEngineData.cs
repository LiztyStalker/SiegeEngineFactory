using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEngineData// : ScriptableObject
{

    [SerializeField]
    private int _health;

    [SerializeField]
    private int _increaseHealth;

    [SerializeField]
    private float _attackTime;

    [SerializeField]
    private float _decreaseAttackTime;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private int _increaseAttackDamage;


    public SiegeEngineData()
    {
        _health = 200;
        _increaseHealth = 10;
        _attackTime = 2f;
        _decreaseAttackTime = 0.01f;
        _attackDamage = 20;
        _increaseAttackDamage = 4;

    }
    public int GetHealth(SiegeEngineLine line) => _health + _increaseHealth * line.Upgrade;
    public float GetAttackTime(SiegeEngineLine line) => _attackTime - _decreaseAttackTime * (float)line.Upgrade;
    public int GetAttackDamage(SiegeEngineLine line) => _attackDamage + _increaseAttackDamage * line.Upgrade;
}
