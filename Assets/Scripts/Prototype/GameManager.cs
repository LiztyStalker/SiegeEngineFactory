using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SiegeEngineLine
{
    private int _upgrade;

    public int Upgrade => _upgrade;

    public void AddUpgrade()
    {
        _upgrade++;
    }
}

public class GameManager : MonoBehaviour
{

    private static GameManager _current;

    public static GameManager Current
    {
        get
        {
            if(_current == null)
            {
                _current = FindObjectOfType<GameManager>();
            }
            return _current;
        }
    }

    [SerializeField]
    private UIGame _uiGame;

    [SerializeField]
    private Building _building;

    [SerializeField]
    private SiegeEngineActor _siegeEngine;

    private SiegeEngineData _siegeEngineData = new SiegeEngineData();

    private SiegeEngineLine _siegeEngineLine;

    [SerializeField]
    private Bullet _bullet;

    private int _gold = 0;

    private int Gold
    {
        get
        {
            return _gold;
        }

        set
        {
            _gold = value;
            _uiGame.SetData(_gold, _level, _wave);
        }
    }

    private int _level = 1;

    private int _wave = 1;

    private float _nowTime = 0f;

    private float _createTime = 0f;

    private int _population = 10;

    List<Building> _buildList = new List<Building>();
    List<SiegeEngineActor> _siegeEngineList = new List<SiegeEngineActor>();
    Dictionary<ITarget, List<Bullet>> _bulletDic = new Dictionary<ITarget, List<Bullet>>();

    private void Awake()
    {
        _uiGame.SetOnUpgaradeListener(UpgradeEvent);

        _uiGame.Initialize();
    }


    private void Start()
    {
        CreateBuilding(new Vector2(2f, 2.6f));
        CreateSiegeEngine(new Vector2(-2f, 2.5f));
    }

    private void Update()
    {
        _nowTime += Time.deltaTime;
        _createTime += Time.deltaTime;
        if (_nowTime > 1f)
        {
            Gold++;
//            _gold++;
            _nowTime -= 1f;
//            _uiGame.SetData(_gold, _level, _wave);
        }

        if(_createTime > 2f)
        {
            CreateSiegeEngine();
            _createTime -= 2f;
        }

        for(int i = 0; i < _siegeEngineList.Count; i++)
        {
            _siegeEngineList[i].Process();
        }

        for(int i = 0; i < _buildList.Count; i++)
        {
            _buildList[i].Process();
        }
    }

    public SiegeEngineActor FindSiegeEngine(Building building)
    {
        if (_siegeEngineList.Count > 0)
            return _siegeEngineList[UnityEngine.Random.Range(0, _siegeEngineList.Count)];
        return null;
    }

    public Building FindBuilding(SiegeEngineActor engine)
    {
        if (_buildList.Count > 0)
            return _buildList[UnityEngine.Random.Range(0, _buildList.Count)];
        return null;
    }

    public Bullet CreateBullet(Vector2 originPos, ITarget target, int damage)
    {
        if (target != null)
        {
            var bullet = Instantiate(_bullet);
            bullet.SetData(originPos, target, damage, RemoveBullet);
            AddBullet(target, bullet);
            bullet.Activate();
            return bullet;
        }
        return null;
    }

    public Building CreateBuilding(Vector2 position)
    {
        var building = Instantiate(_building);
        building.transform.position = position;
        building.SetData(_level, _wave);
        building.SetOnHitEvent(HealthEvent);
        building.Activate();
        AddBuilding(building);
        return building;
    }

    public SiegeEngineActor CreateSiegeEngine(Vector2 position)
    {
        var engine = Instantiate(_siegeEngine);
        engine.transform.position = position;
        engine.SetData(_siegeEngineData, _siegeEngineLine);
        engine.Activate();
        AddSiegeEngine(engine);
        return engine;
    }

    private void AddBullet(ITarget target, Bullet bullet)
    {
        if (!_bulletDic.ContainsKey(target))
        {
            _bulletDic.Add(target, new List<Bullet>());
        }

        _bulletDic[target].Add(bullet);
    }

    private void RemoveBullet(ITarget target, Bullet bullet)
    {
        if (_bulletDic.ContainsKey(target))
        {
            _bulletDic[target].Remove(bullet);
        }
    }

    private void AddBuilding(Building building)
    {
        _buildList.Add(building);
        //_uiGame.SetData(_gold, _level, _wave);
    }

    public void RemoveBuilding(Building building)
    {
        if (_buildList.Contains(building))
        {
            building.SetOnHitEvent(null);
            _buildList.Remove(building);
            //_gold += building.gold;
            Gold += building.gold;
            AddWave();

//            CreateSiegeEngine();

            CreateBuilding(new Vector2(2f, 2.6f));
        }
    }

    public void AddSiegeEngine(SiegeEngineActor engine)
    {
        _siegeEngineList.Add(engine);
        RefreshPopulation();
    }

    public void RemoveSiegeEngine(SiegeEngineActor engine)
    {
        if (_siegeEngineList.Contains(engine))
        {
            _siegeEngineList.Remove(engine);
        }
        RefreshPopulation();
    }

    private void RefreshPopulation()
    {
        _uiGame.SetPopulation(_siegeEngineList.Count, _population);
    }

    private bool IsPopulation()
    {
        return _siegeEngineList.Count < _population;
    }

    private void AddWave()
    {
        if (_wave + 1 < 10)
        {
            _wave++;
        }
        else
        {
            _wave = 1;
            _level++;
        }
    }

    private void HealthEvent(int health)
    {
        _uiGame.SetData(health);
    }

    private void CreateSiegeEngine()
    {
        if (IsPopulation())
        {
            CreateSiegeEngine(new Vector2(-2f, 2.5f));
        }
    }

    private UpgradeResult UpgradeEvent()
    {
        var result = new UpgradeResult();
        if(_gold > 10 + _siegeEngineLine.Upgrade)
        {
            Gold -= 10 + _siegeEngineLine.Upgrade;
            _siegeEngineLine.AddUpgrade();
            result.currect = true;
        }
        result.upgrade = _siegeEngineLine.Upgrade;
        result.gold = 10 + _siegeEngineLine.Upgrade;
        return result;
    }

}
