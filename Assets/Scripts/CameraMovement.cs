using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var initY = transform.position.y;

        DOTween.Sequence()
            .Append(transform.DOMoveY(30, Random.Range(1.0f, 5.0f)))
            .Append(transform.DOMoveY(initY, Random.Range(1.0f, 5.0f)))
            .SetLoops(-1)
            .SetEase(Ease.OutBounce)
            .Play();
    }
}
