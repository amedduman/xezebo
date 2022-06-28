using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalePunch : MonoBehaviour
{
    [SerializeField] bool _isRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRight)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(DOTween.IsTweening(this.transform)) return;
                transform.DOPunchScale(Vector3.one * .2f, .1f);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(DOTween.IsTweening(this.transform)) return;
                transform.DOPunchScale(Vector3.one * .2f, .1f);
            } 
        }
    }
}
