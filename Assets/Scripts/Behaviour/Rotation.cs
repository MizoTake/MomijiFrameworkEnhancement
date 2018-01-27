using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Momiji;

public class Rotation : MonoBehaviour
{

    [SerializeField]
    private Ease _easeType;
    [SerializeField]
    private RotateMode _mode;
    [SerializeField]
    private Vector3 _vector;
    [SerializeField]
    private float _rot;

    // Use this for initialization
    void Start()
    {
        DOTween.Sequence()
            .Append(transform.DORotate(Enum<Vector>.Random.EnumToNormalize() * _rot, 1.0f, _mode))
            .SetEase(_easeType)
            .SetLoops(-1)
            .Play();
    }
}
