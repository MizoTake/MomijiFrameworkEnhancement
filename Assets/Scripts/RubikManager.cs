using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Momiji;
using System.Threading;

public class RubikManager : MonoBehaviour
{

    private const float AMP = 10.0f;

    public Material cubeShader;

    [SerializeField]
    private int ONE_SIDE = 1;
    [SerializeField, Range(0.0f, 10.0f)]
    private float ONE_SIDE_DISTANCE = 1.5f;
    [SerializeField]
    private float BLOCK_SCALE = 1.0f;
    [SerializeField]
    private ControllerFromSound _controller;
    [SerializeField]
    private Rotation _rot;
    [SerializeField]
    private RandomScale _ranScale;

    private GameObject[,,] rubikCube;
    private Vector3[,,] initPos;

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
        var blockScale = Vector3.one * BLOCK_SCALE;
        rubikCube = new GameObject[ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1];
        initPos = new Vector3[ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1, ONE_SIDE * 2 + 1];
        for (int i = -ONE_SIDE; i <= ONE_SIDE; i++)
        {
            for (int j = -ONE_SIDE; j <= ONE_SIDE; j++)
            {
                for (int k = -ONE_SIDE; k <= ONE_SIDE; k++)
                {
                    var cube = rubikCube[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = initPos[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE] = new Vector3(i, j, k) * ONE_SIDE_DISTANCE;
                    cube.transform.localScale = blockScale;
                    cube.GetComponent<MeshRenderer>().material = cubeShader;
                    cube.transform.parent = transform;
                    cube.name += i + " " + j + " " + k;
                }
            }
        }

        _rot.Count = 0;

        StartCoroutine(Roop(blockScale));
    }

    private IEnumerator Roop(Vector3 blockScale)
    {
        while (true)
        {
            var animTime = 0.2f * (_controller.Power * AMP);
            var animTimeHalf = (float)System.Math.Round(animTime / 2.0f, 5, System.MidpointRounding.AwayFromZero);

            if (_controller.Power > 0.4f)
            {
                for (int i = -ONE_SIDE; i <= ONE_SIDE; i++)
                {
                    for (int j = -ONE_SIDE; j <= ONE_SIDE; j++)
                    {
                        for (int k = -ONE_SIDE; k <= ONE_SIDE; k++)
                        {
                            DOTween.Sequence()
                                .Append(rubikCube[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE].transform.DOPunchPosition(initPos[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE], animTime / 2.0f))
                                .Append(rubikCube[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE].transform.DOLocalMove(initPos[i + ONE_SIDE, j + ONE_SIDE, k + ONE_SIDE], animTime / 2.0f))
                                .Play();
                        }
                    }
                }
                yield return new WaitForSeconds(animTime + 0.3f);
                continue;
            }

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

            _ranScale.Time = animTime;
            _ranScale.Run();
            _rot.Time = animTime;
            _rot.Run();

            var randomValue = new int[3];
            for (int i = 0; i < 3; i++)
            {
                randomValue[i] = Random.Range(0, rubikCube.GetLength(i));
            }

            var scaleAnim = DOTween.Sequence()
                .Append(rubikCube[randomValue[0], randomValue[1], randomValue[2]].transform.DOScale(_controller.Power * blockScale / 10.0f, animTimeHalf))
                .Append(rubikCube[randomValue[0], randomValue[1], randomValue[2]].transform.DOScale(blockScale, animTimeHalf));

            DOTween.Sequence()
                .Append(center.transform.DOLocalRotate(vec * 90, animTime, RotateMode.LocalAxisAdd))
                // .Join(scaleAnim)
                // .Append(center.transform.DOLocalRotate(vec * -90, 0.0f, RotateMode.LocalAxisAdd))
                .AppendCallback(() => _nears.ForEach(_ =>
                {
                    _.transform.SetParent(transform);
                }))
                .Play();

            yield return new WaitForSeconds(animTime + 0.3f);
        }
    }
}
