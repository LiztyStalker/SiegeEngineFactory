namespace SEF.Process
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct ProcessEntity
    {
        private IProcessData _data;
        private int _value;

        private float _nowTime;

        private ProcessEntity(IProcessData data)
        {
            _data = data;
            _value = 0;
            _nowTime = 0;
        }

        internal void SetProcessData(int value)
        {
            _value = value;
        }
        internal void RunProcess(float deltaTime, System.Action processCallback)
        {
            _nowTime += deltaTime;
            Debug.Log(_nowTime);
            //�ð� ����
            if (_nowTime >= _data.ProcessTime)
            {
                //�ð� �����ϸ� ���μ��� �̺�Ʈ ����
                //IProcessData�� ������ ����
                processCallback?.Invoke();
                _nowTime -= _data.ProcessTime;
            }
        }

        internal IProcessData GetProcessData() => _data;

        internal static ProcessEntity Create(IProcessData data)
        {
            return new ProcessEntity(data);
        }
    }
}