using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Momiji;

public class RubikManager : MonoBehaviour
{

    public Material cubeShader;

    [SerializeField]
    private int ONE_SIDE = 1;
    [SerializeField, Range(0.0f, 10.0f)]
    private float ONE_SIDE_DISTANCE = 1.5f;
    [SerializeField]
    private float BLOCK_SCALE = 1.0f;

    private GameObject[,,] rubikCube;

    private List<GameObject> _nears = new List<GameObject>();

    // void OnValidate()
    // {
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         Destroy(transform.GetChild(i).gameObject);
    //     }
    //     Start();
    // }

    // Use this for initialization
    void Start()
    {
        rubikCube = new GameObject[ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1];
        for (int i = -ONE_SIDE; i <= ONE_SIDE; i++)
        {
            for (int j = -ONE_SIDE; j <= ONE_SIDE; j++)
            {
                for (int k = -ONE_SIDE; k <= ONE_SIDE; k++)
                {
                    var cube = rubikCube[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(i, j, k) * ONE_SIDE_DISTANCE;
                    cube.transform.localScale = Vector3.one * BLOCK_SCALE;
                    cube.GetComponent<MeshRenderer>().material = cubeShader;
                    cube.transform.parent = transform;
                    cube.name += i + " " + j + " " + k;
                }
            }
        }

        transform.localScale = Vector3.one * 20;

        DOTween.Sequence()
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                _nears.Clear();
                var vecRand = Random.Range(0, 3);
                var target = Random.Range(0, ONE_SIDE * 2 + 1);
                var center = rubikCube[target, ONE_SIDE, ONE_SIDE];
                switch (vecRand)
                {
                    case 1:
                        center = rubikCube[ONE_SIDE, target, ONE_SIDE];
                        break;
                    case 2:
                        center = rubikCube[ONE_SIDE, ONE_SIDE, target];
                        break;
                }
                for (int i = 0; i < ONE_SIDE * 2 + 1; i++)
                {
                    for (int j = 0; j < ONE_SIDE * 2 + 1; j++)
                    {
                        var obj = rubikCube[target, i, j];
                        switch (vecRand)
                        {
                            case 1:
                                obj = rubikCube[i, target, j];
                                break;
                            case 2:
                                obj = rubikCube[i, j, target];
                                break;
                        }
                        if (obj == center) continue;
                        obj.transform.SetParent(center.transform);
                        _nears.Add(obj);
                    }
                }

                var vec = Vector3.right;
                switch (vecRand)
                {
                    case 1:
                        vec = Vector3.up;
                        break;
                    case 2:
                        vec = Vector3.forward;
                        break;
                }

                DOTween.Sequence()
                    .Append(center.transform.DOLocalRotate(vec * 90, 0.2f, RotateMode.WorldAxisAdd))
                    .Append(center.transform.DOLocalRotate(vec * -90, 0.0f, RotateMode.WorldAxisAdd))
                    .AppendCallback(() => _nears.ForEach(_ =>
                    {
                        Debug.Log("release");
                        _.transform.SetParent(transform);
                    }))
                    .Play();
            })
            .SetLoops(-1)
            .Play();

    }
}
