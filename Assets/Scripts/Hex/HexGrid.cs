using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{

    [SerializeField]
    private int sideSize;
    [SerializeField]
    private float distancePersent;
    [SerializeField]
    private HexChip chipPrefab;
    private const int ONE_SIDE = 6;
    private const int SIDE = 4;
    private const float ONE_SIZE_X = 2.0f;
    private const float ONE_SIZE_Z = 1.5f;
    private const float ONE_SIZE_Y = ONE_SIZE_X * ONE_SIZE_Z * 10;

    private HexChip[] chips;

    private HexMesh hexMesh;

    // Use this for initialization
    void Start()
    {
        Build();
    }

    void Build()
    {
        int side = (sideSize - 1) * SIDE;
        int maxSize = 0;
        int halfSide = side / 4;
        for (int i = -halfSide; i <= halfSide; i++)
        {
            maxSize += (i == 0) ? 1 : Mathf.Abs(i) * ONE_SIDE;
        }
        int anotherHalf = Mathf.Abs(halfSide) - 1;
        for (int i = -anotherHalf; i <= anotherHalf; i++)
        {
            maxSize += (i == 0) ? 1 : Mathf.Abs(i) * ONE_SIDE;
        }
        chips = new HexChip[maxSize];

        int cnt = 0;
        int size = (side == 0) ? 0 : 1;
        for (int i = -size; i <= size; i++)
        {
            for (int j = -halfSide; j <= halfSide; j++)
            {
                if (i == 1 && j == 0 || i == -1 && j == 0) continue;
                var basePos = AxisChip(i, j, cnt, Vector3.zero);
                if (Mathf.Abs(j) >= 2)
                {
                    for (int k = 0; k < Mathf.Abs(j) - 1; k++)
                    {
                        basePos = AxisChip(i, j, cnt, basePos);
                    }
                }
                cnt += 1;
            }
        }
    }

    Vector3 AxisChip(int vector, int side, int num, Vector3 basePos)
    {
        int newSide = side;
        if (basePos != Vector3.zero)
        {
            newSide = -1 * (int)Mathf.Sign(newSide);
            vector -= 1;
            if (vector == -2)
            {
                vector = 1;
                newSide *= -1;
            }
        }
        int z = newSide * vector;
        Vector3 position = basePos + new Vector3((Mathf.Abs(newSide) - Mathf.Abs(z) * 0.5f) * Mathf.Sign(newSide) * (HexMesh.innerRadius * distancePersent * ONE_SIZE_X), 0f/*ONE_SIZE_Y * -Mathf.Abs(side)*/, z * (HexMesh.outerRadius * distancePersent * ONE_SIZE_Z));
        CreateChip(newSide, z, num, position);
        return position;
    }

    void CreateChip(int x, int z, int num, Vector3 position)
    {
        HexChip chip = chips[num] = Instantiate<HexChip>(chipPrefab);
        chip.name = "Hex Chip (" + x + ", " + z + ", " + num + ")";
        chip.transform.SetParent(transform, false);
        chip.transform.localPosition = position;
        //chip.SetNum(x, y, z);
    }
}