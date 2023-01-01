
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public ObjectPool<GameObject> DiscPool;
    [FormerlySerializedAs("brickPrefab")] public GameObject discPrefab;
    private void Awake()
    {
        Instance = this;
        DiscPool= new ObjectPool<GameObject>(CreateDisc, OnTakeDiscFromPool, OnReturnDiscToPool);
    }

    #region Disc Pooling
    GameObject CreateDisc()
    {
        var brick = Instantiate(discPrefab);
        return brick;
    }

    void OnTakeDiscFromPool(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<Collider>().enabled = true; // To activate colliders because we disable them if they collide with tower stone
    }

    void OnReturnDiscToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion


}