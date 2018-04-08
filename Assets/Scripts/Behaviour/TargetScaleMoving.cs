using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetScaleMoving : MonoBehaviour
{

    public Transform _target;
    public ControllerFromSound _controller;

    private float _initDistance;

    // Use this for initialization
    void Start()
    {
        _initDistance = Vector3.Distance(_target.position, transform.position);

        StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Moving()
    {
        while (true)
        {
            var animTime = 0.5f * (_controller.Power * _controller.AMP);
            yield return new WaitForSeconds(animTime);

            DOTween.Sequence()
                .Append(transform.DOLookAt(_target.position, animTime))
                .Join(transform.DOShakePosition(_controller.Power))
                .Append(transform.DOMoveZ(_initDistance * _target.localScale.x / 5.0f, animTime))
                .Play();
        }
    }
}
