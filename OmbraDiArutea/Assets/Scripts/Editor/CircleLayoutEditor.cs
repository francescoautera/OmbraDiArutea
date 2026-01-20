using UnityEngine;

[ExecuteAlways]
public class CircleLayoutEditor : MonoBehaviour
{
    public float radius = 2f;

    void Update()
    {
        int count = transform.childCount;
        if (count == 0) return;

        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle),
                Mathf.Sin(angle),
                0f
            ) * radius;

            transform.GetChild(i).localPosition = pos;
        }
    }
}