
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
        obj.transform.eulerAngles = new Vector3(0, 90, 0);
    }

    void OnReturnDiscToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion


}