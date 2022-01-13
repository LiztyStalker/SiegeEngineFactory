namespace SEF.Unit
{
    using UnityEngine;
    using Data;
    public interface ITarget
    {
        void DecreaseHealth(DamageData attackData);
        Vector2 NowPosition { get; }
    }
}