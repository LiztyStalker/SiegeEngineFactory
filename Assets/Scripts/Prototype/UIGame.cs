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
        _goldText.text = $"��ȭ : {gold}";
        _levelText.text = $"���� : {level}";
        _waveText.text = $"���̺� : {wave}";

    }

    public void SetData(int health)
    {
        _healthText.text = $"ü�� : {health}";
    }

    public void SetPopulation(int population, int max)
    {
        _populationText.text = $"�α� : {population} / {max}";
    }
}
