using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Momiji;

public class RubikManager : MonoBehaviour
{

    public Material cubeShader;

    private const int ONE_SIDE = 3;
    private const float ONE_SIDE_DISTANCE = 1.5f;
    private GameObject[,,] rubikCube = new GameObject[3, 3, 3];

    private List<GameObject> _nears = new List<GameObject>();
    private Sequence _rotateAnim;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ONE_SIDE; i++)
        {
            for (int j = 0; j < ONE_SIDE; j++)
            {
                for (int k = 0; k < ONE_SIDE; k++)
                {
                    var cube = rubikCube[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(i - 1, j - 1, k - 1) * ONE_SIDE_DISTANCE;
                    cube.GetComponent<MeshRenderer>().material = cubeShader;
                    cube.transform.parent = transform;
                    cube.name += i + " " + j + " " + k;
                }
            }
        }

        transform.localScale = Vector3.one * 20;

        DOTween.Sequence()
            .AppendCallback(() =>
            {
                _nears.Clear();
                var vecRand = Random.Range(0, 3);
                var target = Random.Range(0, 3);
                var center = rubikCube[target, 1, 1];
                switch (vecRand)
                {
                    case 1:
                        center = rubikCube[1, target, 1];
                        break;
                    case 2:
                        center = rubikCube[1, 1, target];
                        break;
                }
                for (int i = 0; i < ONE_SIDE; i++)
                {
                    for (int j = 0; j < ONE_SIDE; j++)
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
                    .Append(center.transform.DORotate(vec * 90, 0.2f, RotateMode.LocalAxisAdd))
                    .Append(center.transform.DORotate(vec * -90, 0.0f, RotateMode.LocalAxisAdd))
                    .AppendCallback(() => _nears.ForEach(_ => _.transform.SetParent(transform)))
                    .Play();
            })
            .AppendInterval(1.0f)
            .SetLoops(-1)
            .Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
