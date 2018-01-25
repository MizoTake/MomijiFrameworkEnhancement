using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotation : MonoBehaviour
{

    [SerializeField]
    private Ease _easeType;
    [SerializeField]
    private RotateMode _mode;
    [SerializeField]
    private float _rot;

    // Use this for initialization
    void Start()
    {
        DOTween.Sequence()
            .Append(transform.DORotate(Vector3.up * _rot, 1.0f, _mode))
            .SetEase(_easeType)
            .SetLoops(-1)
            .Play();
    }
}
