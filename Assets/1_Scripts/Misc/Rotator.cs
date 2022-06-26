using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _timeToRotate = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0,30,0), _timeToRotate).
        SetLoops(-1, LoopType.Incremental).
        SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        DOTween.Kill(this.transform);
    }
}
