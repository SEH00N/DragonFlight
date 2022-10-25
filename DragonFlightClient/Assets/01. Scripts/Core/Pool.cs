using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();
    private T prefab = null;
    private Transform parent = null;

    public Pool(T _prefab, Transform _parent)
    {
        prefab = _prefab;
        parent = _parent;
    }

    public T Pop()
    {
        T obj = null;
        
        if(pool.Count > 0)
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        
        return obj;
    }

    public void Push(T _obj)
    {
        _obj.gameObject.SetActive(false);
        pool.Push(_obj);
    }
}
