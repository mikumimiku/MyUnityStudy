using UnityEngine;

/// <summary>
/// 根据目标点，绘制轨迹抛物线
/// </summary>
public class Parabola_Target_Physical : MonoBehaviour
{
    //发射点
    public Transform startTrans;
    //目标点
    public Transform endTrans;

    private Vector3 oldEndPos = Vector3.zero;

    //最高点和目标点的高度差，不能设置为0
    [Range(0.1f, 10f)] public float heightDifference;
    private float oldHeightDifference = 0f;

    //发射的物体
    public GameObject ball;

    //线
    public LineRenderer lineRenderer;
    private Vector3[] linePoints;
    //线的点数
    public int linePointCount;

    //当抛物线为直线的时候
    public float straightForce = 5f;
    //两个点画直线
    private Vector3[] straightLine = new Vector3[2];

    [Header("画线相关参数")]
    //预测点力
    private Vector2 linePointsVelocity;
    //预测点位置
    private Vector2 linePointsPosition;
    //预测点间隔
    private float linePointsInterval;

    [Header("计算参数")]
    //发射物的受重力影响大小
    private float ballGravityScale;

    //物体受到的真实的重力，2D中是系统重力 * 受重力影响
    private float Gravity;

    //上升高度和下降高度
    private float raiseHeight, declineHeight;

    //上升时间和总计时间
    private float raiseTime, totalTime;

    //水平速度
    private float speedX;

    //最终发射速度
    private Vector3 finalVelocity;



    private void Start()
    {
        linePoints = new Vector3[linePointCount];

        ballGravityScale = ball.GetComponent<Rigidbody2D>().gravityScale;

        Gravity = Mathf.Abs(Physics2D.gravity.y * ballGravityScale);
    }
    public void Update()
    {
        CalculateParabola();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /// <summary>
    /// 计算抛物线
    /// </summary>
    private void CalculateParabola()
    {
        //避免每帧计算
        if (endTrans.position == oldEndPos && heightDifference == oldHeightDifference)
            return;
        oldEndPos = endTrans.position;
        oldHeightDifference = heightDifference;


        //目标高于发射点时，最高点基于目标点计算，目标点低于发射点时，最高点基于发射点计算
        if (endTrans.position.y > transform.position.y)
        {
            raiseHeight = heightDifference + endTrans.position.y - transform.position.y;
            declineHeight = heightDifference;
        }
        else
        {
            raiseHeight = heightDifference;
            declineHeight = transform.position.y + heightDifference - endTrans.position.y;
        }

        raiseTime = Mathf.Sqrt(2 * raiseHeight / Gravity);
        //当发射点和结束点的x或者y相同，如果heightDifference为0的话，totalTime会为0，会造成下面除以0
        totalTime = Mathf.Sqrt(2 * raiseHeight / Gravity) + Mathf.Sqrt(2 * declineHeight / Gravity);

        speedX = (endTrans.position - startTrans.position).x / totalTime;

        finalVelocity.Set(speedX, (raiseTime * Gravity * Vector3.up).y, 0f);

        DrawLine();
    }

    /// <summary>
    /// 画抛物线
    /// </summary>
    private void DrawLine()
    {
        //如果发射点和结束点的x相同，就直接用两个点绘制一条直线,heightDifference不可能为0，所以不用判断y
        if (startTrans.position.x == endTrans.position.x)
        {
            lineRenderer.positionCount = 2;

            straightLine[0] = startTrans.position;
            straightLine[1] = endTrans.position;

            lineRenderer.SetPositions(straightLine);
            return;
        }

        linePointsVelocity = finalVelocity;
        linePointsPosition = startTrans.position;
        linePointsInterval = Mathf.Abs(startTrans.position.x - endTrans.position.x) / Mathf.Abs(finalVelocity.x) / linePointCount;

        for (int i = 0; i < linePointCount; i++)
        {
            linePointsPosition += linePointsVelocity * linePointsInterval + Vector2.up * 0.5f * -Gravity * linePointsInterval * linePointsInterval;

            linePointsVelocity += Vector2.up * -Gravity * linePointsInterval;

            linePoints[i] = linePointsPosition;

        }

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    /// <summary>
    /// 发射
    /// </summary>
    private void Shoot()
    {
        //调整发射角度
        startTrans.rotation = Quaternion.FromToRotation(Vector3.up, finalVelocity);

        GameObject obj = Instantiate(ball, startTrans.position, startTrans.rotation);

        obj.GetComponent<Rigidbody2D>().velocity = finalVelocity;
    }
}
