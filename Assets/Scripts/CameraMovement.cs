using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private float _maxDistance = 5.0f;

    // Use this for initialization
    void Start()
    {
        var initY = transform.position.y;

        DOTween.Sequence()
            .Append(transform.DOMoveY(30, Random.Range(1.0f, _maxDistance)))
            .Append(transform.DOMoveY(initY, Random.Range(1.0f, _maxDistance)))
            .SetLoops(-1)
            .SetEase(Ease.OutBounce)
            .Play();
    }
}
