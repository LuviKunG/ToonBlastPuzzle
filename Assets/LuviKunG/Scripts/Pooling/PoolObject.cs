using System;
using System.Collections;
using System.Collections.Generic;

namespace LuviKunG.Pooling
{
    public class PoolObject<TPool> : IEnumerable<TPool> where TPool : IPoolable
    {
        public delegate T OnInstantiateDelegate<T>(int index);

        private List<TPool> list;

        private OnInstantiateDelegate<TPool> onInstantiate;

        public int Count => list.Count;
        public TPool this[int index] => list[index];

        private PoolObject()
        {
            list = new List<TPool>();
        }

        public PoolObject(OnInstantiateDelegate<TPool> onInstantiate) : this()
        {
            this.onInstantiate = onInstantiate;
        }

        public PoolObject(OnInstantiateDelegate<TPool> onInstantiate, int size) : this(onInstantiate)
        {
            if (size <= 0)
                throw new ArgumentException("Cannot set initial size below or equal to zero.");
            for (int i = 0; i < size; ++i)
                list.Add(onInstantiate(i));
        }

        public TPool Pick()
        {
            for (int i = 0; i < list.Count; ++i)
                if (!list[i].isPoolActive)
                    return list[i];
            TPool newItem = onInstantiate(list.Count);
            list.Add(newItem);
            return newItem;
        }

        IEnumerator<TPool> IEnumerable<TPool>.GetEnumerator() { return list.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return list.GetEnumerator(); }
    }
}
