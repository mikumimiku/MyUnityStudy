using DG.Tweening;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    #region 方法一 物理
    //目标点
    //public Transform target;

    ////发射点
    //public Transform point;

    ////最高点
    //public float highestY;

    ////发射的物体
    //public GameObject ball;

    //private void Start()
    //{
    //    InvokeRepeating("Shoot", 0f, 1f);
    //}

    //public void Shoot()
    //{
    //    //计算重力
    //    //float Gravity = Mathf.Abs(Physics2D.gravity.y * ball.GetComponent<Rigidbody2D>().gravityScale);
    //    float Gravity = Mathf.Abs(Physics2D.gravity.y * 1f);

    //    //最高点前的高度，最高点后的高度
    //    float height_1,height_2;

    //    //判断目标点和发射点
    //    if(target.position.y > transform.position.y)
    //    {
    //        height_1 = highestY + target.position.y - transform.position.y;
    //        height_2 = highestY;
    //    }
    //    else
    //    {
    //        height_1 = highestY;
    //        height_2 = transform.position.y + highestY - target.position.y;
    //    }

    //    //计算，不管
    //    float time_1 = Mathf.Sqrt(2 * height_1 / Gravity);
    //    float time_2 = Mathf.Sqrt(2 * height_1 /Gravity) + Mathf.Sqrt(2 * height_2 / Gravity);
    //    Vector3 distance = target.position - transform.position;
    //    distance.y = 0;
    //    Vector3 speed = distance / time_2;
    //    Vector3 velocity = speed + time_1 * Gravity * Vector3.up;


    //    //抛物线







    //    //调整发射角度
    //    point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

    //    //生成物体,执行物体逻辑
    //    //GameObject obj = Instantiate(ball, point);

    //    GameObject obj = Instantiate(ball);

    //    obj.GetComponent<Rigidbody2D>().velocity = velocity;

    //    int colorID = Random.Range(1, 5);

    //    if (colorID == 1)
    //        obj.GetComponent<SpriteRenderer>().color = Color.white;
    //    if (colorID == 2)
    //        obj.GetComponent<SpriteRenderer>().color = Color.red;
    //    if (colorID == 3)
    //        obj.GetComponent<SpriteRenderer>().color = Color.black;
    //    if (colorID == 4)
    //        obj.GetComponent<SpriteRenderer>().color = Color.blue;

    //}
    #endregion


    #region 方法二 贝塞尔曲线

    ///// <param name="t">0到1的值，0获取曲线的起点，1获得曲线的终点</param>
    ///// <param name="start">曲线的起始位置</param>
    ///// <param name="center">决定曲线形状的控制点</param>
    ///// <param name="end">曲线的终点</param>
    //public static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    //{
    //    return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    //}


    //public Transform startTrans;
    //public Transform endTrans;
    //public Transform centerTrans;

    //public float height;
    //public int resolution;
    //public Vector3[] path;
    //public LineRenderer lineRender;
    //public GameObject ball;

    //public AnimationCurve animationCurve;
    //public void Get()
    //{
    //    var startPoint = startTrans.position;
    //    var endPoint = endTrans.position;
    //    //var bezierControlPoint = (startPoint + endPoint) * 0.5f + (Vector3.up * height);
    //    var bezierControlPoint = centerTrans.position;

    //    path = new Vector3[resolution];//resolution为int类型，表示要取得路径点数量，值越大，取得的路径点越多，曲线最后越平滑
    //    for (int i = 0; i < resolution; i++)
    //    {
    //        var t = (i + 1) / (float)resolution;//归化到0~1范围
    //        path[i] = GetBezierPoint(t, startPoint, bezierControlPoint, endPoint);//使用贝塞尔曲线的公式取得t时的路径点
    //    }

    //    lineRender.positionCount = path.Length;
    //    lineRender.SetPositions(path);
    //}

    //private void Start()
    //{
    //    startTrans = transform;

    //    //ball.transform.DOPath(path, 0.8f).SetEase(Ease.Linear);

    //    //ball.transform.DOPath(path, 0.8f, PathType.CatmullRom);

    //}
    //private void Update()
    //{
    //    Get();

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        GameObject obj = Instantiate(ball);
    //        //obj.transform.DOPath(path, 0.8f).SetLookAt(0).SetEase(Ease.Linear);
    //        //obj.transform.DOPath(path, 4f,PathType.CatmullRom,PathMode.TopDown2D).SetLookAt(0).SetEase(Ease.Linear);
    //        obj.transform.DOPath(path, 0.8f, PathType.CatmullRom).SetLookAt(0,Vector3.right,Vector3.up).SetEase(animationCurve);
    //    }


    //}


    #endregion


    #region 物理，调整发射力度，发射角度
    //public LineRenderer lineRenderer;
    //public Vector3[] linePoints = new Vector3[60];//点
    //public Transform startPos;//发射点
    //public float force;//发射的力度
    //public float subdivision = .02f;//细分长度,每一小段的长度

    //public GameObject ball3D;
    //public GameObject ball;

    //private void Update()
    //{
    //    for(int i = 0; i < linePoints.Length; i++)
    //    {
    //        //3D
    //        //linePoints[i] = startPos.position + startPos.forward * force * i
    //              * subdivision + Physics.gravity * (0.5f * (i * subdivision) * (i * subdivision));

    //        //2D
    //        linePoints[i] = startPos.position + startPos.right * force * i * subdivision + 
    //                        (Vector3)Physics2D.gravity * ball.GetComponent<Rigidbody2D>().gravityScale
    //                        * (0.5f * (i * subdivision) * (i * subdivision));
    //    }

    //    lineRenderer.positionCount = linePoints.Length;
    //    lineRenderer.SetPositions(linePoints);

    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //3D
    //        //GameObject obj = Instantiate(ball3D,startPos.position,startPos.rotation);
    //        //obj.GetComponent<Rigidbody>().AddForce(startPos.forward * force, ForceMode.VelocityChange);

    //        //2D
    //        GameObject obj = Instantiate(ball, startPos.position, startPos.rotation);
    //        obj.GetComponent<Rigidbody2D>().AddForce(startPos.right * force, ForceMode2D.Impulse);
    //    }
    //}

    #endregion
}
