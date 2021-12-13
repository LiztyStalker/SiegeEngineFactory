namespace PoolSystem
{
    using System.Collections.Generic;

    public class PoolSystem<T> where T : IPoolElement
    {
        private Queue<T> _queue;
         
        public static PoolSystem<T> Create()
        {
            return new PoolSystem<T>();
        }

        public void Initialize()
        {
            _queue = new Queue<T>();
        }

        private T CreateInstance(){
            return System.Activator.CreateInstance<T>();
        }
        
        public T GiveElement()
        {
            if(_queue.Count == 0)
            {
                var element = CreateInstance();
                _queue.Enqueue(element);
            }
            return _queue.Dequeue();
        }


        public void RetrieveElement(T element)
        {
            _queue.Enqueue(element);
        }

        public void CleanUp()
        {
            _queue.Clear();
        }
    }
}