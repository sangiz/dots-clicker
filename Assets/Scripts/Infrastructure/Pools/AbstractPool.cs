using System.Collections.Generic;
using UnityEngine;

namespace IgnSDK.Infrastructure.Pools
{
    public abstract class AbstractPool<T>  where T : MonoBehaviour
    {
        protected string ParentName = "Pool";
        protected readonly Queue<T> PooledItems;
        private readonly int initialItemsCount;
        private GameObject parent;

        public AbstractPool(int initialItemsCount)
        {
            this.initialItemsCount = initialItemsCount;
            PooledItems = new Queue<T>();
        }

        public virtual T Take()
        {
            if (PooledItems.Count <= 0)
            {
                Put(CreateNewItem());
            }

            var dequeuedItem = PooledItems.Dequeue();
            dequeuedItem.gameObject.SetActive(true);

            return dequeuedItem;
        }

        public virtual void Put(T intoPool)
        {
            intoPool.gameObject.SetActive(false);
            var transform = intoPool.transform;
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
            PooledItems.Enqueue(intoPool);
        }

        protected void Populate()
        {
            parent = new GameObject(ParentName);

            for (var i = 0; i < initialItemsCount; i++)
            {
                var newItem = CreateNewItem();
                Put(newItem);
            }
        }

        protected abstract T CreateNewItem();
    }
}