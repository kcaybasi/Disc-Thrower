using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DiscThrower : MonoBehaviour
{
    public static DiscThrower Instance => _sDiscThrower;
    static DiscThrower _sDiscThrower;
    
    private bool _isThrowAllowed=true;
    private GameObject _discObj;
    private float _baseRange;
    private float _baseRate;
    public float DiscThrowRange { get; set; }
    public float DiscThrowRate { get; set; }
    private ThrowType _throwType=ThrowType.RegularThrow;

    public enum ThrowType
    {
        RegularThrow,
        DualShot,
        SpreadShot,
    }
    private void Awake()
    {
        if (_sDiscThrower != null && _sDiscThrower!= this)
        {
            Destroy(gameObject);
            return;
        }
        _sDiscThrower= this;
        
        _baseRange = 40f;
        _baseRate = .40f;
        
        
    }

    public void ChangeThrowType(ThrowType throwType)
    {
        _throwType = throwType;
    }

    private void Update()
    {
        if (_isThrowAllowed)
        {
            StartCoroutine(DiscThrowRoutine());
        }
    }

    void GetThrowType()
    {
        switch (_throwType)
        {
            case ThrowType.RegularThrow:
                ThrowDisc();
                break;
            case ThrowType.DualShot:
                DualThrow();
                break;
            case ThrowType.SpreadShot:
                SpreadShot();
                break;
        }
    }
    void ThrowDisc()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f;
        
        float currentZ = obj.transform.position.z;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
    }

    void DualThrow()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        GameObject obj_2 = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f-Vector3.left*.35f;
        obj_2.transform.position = transform.position+Vector3.up*.2f-Vector3.right*.35f;
        
        float currentZ = obj.transform.position.z;
        float currentZ_2 = obj_2.transform.position.z;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
        obj_2.transform.DOMoveZ(currentZ_2 + _baseRange+DiscThrowRange, 5f);
    }

    void SpreadShot()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        GameObject obj_2 = ObjectPooler.Instance.DiscPool.Get();
        GameObject obj_3 = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f;
        obj_2.transform.position = transform.position+Vector3.up*.2f-Vector3.left*.25f;
        obj_3.transform.position = transform.position+Vector3.up*.2f-Vector3.right*.25f;
        
        float currentZ = obj.transform.position.z;
        float currentZ_2 = obj_2.transform.position.z;
        float currentZ_3 = obj_3.transform.position.z;

        float currentX_2 = obj_2.transform.position.x;
        float currentX_3 = obj_3.transform.position.x;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
        
        obj_2.transform.DOMoveZ(currentZ_2 + _baseRange+DiscThrowRange, 5f);
        obj_2.transform.DOMoveX(.1f*(currentX_2 + _baseRange+DiscThrowRange), 5f);
        
        obj_3.transform.DOMoveZ(currentZ_3 + _baseRange+DiscThrowRange, 5f);
        obj_3.transform.DOMoveX(-.1f*(currentX_3 + _baseRange+DiscThrowRange), 5f);
    }

    IEnumerator DiscThrowRoutine()
    {
        _isThrowAllowed = false;
        GetThrowType();
        yield return new WaitForSeconds(_baseRate-DiscThrowRate);
        _isThrowAllowed = true;
    }

    public void AdjustThrowRate(float value)
    {
        DiscThrowRate += value*.05f;
    }

    public void AdjustThrowRange(float value)
    {
        DiscThrowRange += value;
    }
    
    
    
}
