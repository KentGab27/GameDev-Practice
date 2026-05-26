using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] GameObject Prefab;
    [SerializeField] int InitialSize = 10;

    private readonly Queue<GameObject> pooling = new();

    private void Awake()
    {
        for (int i = 0; i < InitialSize; i++)
            pooling.Enqueue(CreateNew());
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pooling.Count > 0 ? pooling.Dequeue() : CreateNew();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        obj.GetComponent<IPoolable>()?.OnSpawn();
        if (obj.TryGetComponent(out JunkController junk)) junk.Init(this);
        if (obj.TryGetComponent(out FoodController food)) food.Init(this);

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.GetComponent<IPoolable>()?.OnReturn();
        obj.SetActive(false);
        pooling.Enqueue(obj);
    }

    private GameObject CreateNew()
    {
        GameObject obj = Instantiate(Prefab, transform);
        obj.SetActive(false);
        return obj;
    }
}