using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Momiji;

public class RandomScale : MonoBehaviour
{

    [SerializeField]
    private float _minScale;
    [SerializeField]
    private float _maxScale;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(RoopScale());
    }

    IEnumerator RoopScale()
    {
        while (true)
        {
            transform.DOScale(Vector3.zero.RandomRange(_minScale, _maxScale), 1.0f).Play();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
