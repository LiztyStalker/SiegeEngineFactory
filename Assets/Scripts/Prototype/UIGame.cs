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


    [SerializeField]
    private Text _siegeEngineLevelText;

    [SerializeField]
    public Button _upgradeBtn;

          

    public void Initialize()
    {
        _upgradeBtn.onClick.AddListener(UpgradeEvent);
        UpgradeEvent();

    }


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


    private void UpgradeEvent()
    {
        var result = _upgradeEvent();
        if (result.currect)
        {
            _upgradedEvent?.Invoke();
        }
        ShowUpgrade(result.upgrade, result.gold);
    }

    private void ShowUpgrade(int upgrade, int gold)
    {
        _siegeEngineLevelText.text = $"레벨 : {upgrade}";
        _upgradeBtn.GetComponentInChildren<Text>().text = $"업그레이드\n{gold}";
    }


    #region ##### Listener #####

    public System.Func<UpgradeResult> _upgradeEvent;
    public void SetOnUpgaradeListener(System.Func<UpgradeResult> act) => _upgradeEvent = act;

    public System.Action _upgradedEvent;
    public void AddOnUpgradedListener(System.Action act) => _upgradedEvent += act;
    public void RemoveOnUpgradedListener(System.Action act) => _upgradedEvent -= act;


    #endregion


}

public struct UpgradeResult
{
    public int upgrade;
    public int gold;
    public bool currect;
}
