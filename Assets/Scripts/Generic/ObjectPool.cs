using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private Queue<T> poolQueue;
    private List<T> poolList;

    public ObjectPool()
    {
        poolQueue = new Queue<T>();
        poolList = new List<T>();
    }

    public void NewItem(T item) => poolList.Add(item);

    public T GetItem()
    {
        if(poolQueue.Count > 0)
            return poolQueue.Dequeue();
        return default;
    }
    public void FreeItem(T item) => poolQueue.Enqueue(item);
}
