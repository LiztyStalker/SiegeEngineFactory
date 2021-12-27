namespace SEF.Unit
{
    using UnityEngine;
    using Data;
    public interface ITarget
    {
        void DecreaseHealth(AttackData attackData);
        Vector2 NowPosition { get; }
    }
}