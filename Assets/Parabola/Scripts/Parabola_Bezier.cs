using DG.Tweening;
using UnityEngine;

/// <summary>
/// 抛物线，贝塞尔曲线
/// </summary>
public class Parabola_Bezier : MonoBehaviour
{
    /// <param name="t">0到1的值，0获取曲线的起点，1获得曲线的终点</param>
    /// <param name="start">曲线的起始位置</param>
    /// <param name="center">决定曲线形状的控制点</param>
    /// <param name="end">曲线的终点</param>
    public static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }

    //发射点
    public Transform startTrans;
    //目标点
    public Transform endTrans;
    //中间的点
    public Transform middleTrans;

    //表示要取得路径点数量，值越大，取得的路径点越多，曲线最后越平滑
    public int resolution;

    //曲线路径点
    public Vector3[] linePoints;
    //线
    public LineRenderer lineRender;
    //发射物
    public GameObject ball;
    public void DrawLine()
    {
        for (int i = 0; i < resolution; i++)
        {
            var t = (i + 1) / (float)resolution;//归化到0~1范围
            linePoints[i] = GetBezierPoint(t, startTrans.position, middleTrans.position, endTrans.position);//使用贝塞尔曲线的公式取得t时的路径点
        }

        lineRender.positionCount = linePoints.Length;
        lineRender.SetPositions(linePoints);
    }

    private void Start()
    {
        linePoints = new Vector3[resolution];
    }
    private void Update()
    {
        DrawLine();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(ball,transform);

            obj.transform.DOPath(linePoints, 0.8f, PathType.CatmullRom).SetLookAt(0, Vector3.right, Vector3.up).SetEase(Ease.Linear);
        }


    }
}
