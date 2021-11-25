using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField]
    private Text _goldText;

    [SerializeField]
    private Text _levelText;

    [SerializeField]
    private Text _waveText;

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Text _populationText;


    public void SetData(int gold, int level, int wave)
    {
        _goldText.text = $"재화 : {gold}";
        _levelText.text = $"레벨 : {level}";
        _waveText.text = $"웨이브 : {wave}";

    }

    public void SetData(int health)
    {
        _healthText.text = $"체력 : {health}";
    }

    public void SetPopulation(int population, int max)
    {
        _populationText.text = $"인구 : {population} / {max}";
    }
}
