using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class ItemPool<T> where T : new()
        {
            private readonly List<T> _pool;
                
            public ItemPool()
            {
                _pool = new List<T>();
            }

            public T GetOrCreate()
            {
                var poolSize = _pool.Count;

                if (poolSize > 0)
                {
                    var item = _pool[poolSize - 1];
                    _pool.RemoveAt(poolSize - 1);
                    return item;
                }

                return new T();
            }

            public void Return(T item)
            {
                _pool.Add(item);
            }
        }
    }
}