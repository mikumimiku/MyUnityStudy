using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据发射点角度和力，绘制抛物线
/// </summary>
public class Parabola_Force_Physical : MonoBehaviour
{
    //线
    public LineRenderer lineRenderer;

    //点
    public Vector3[] linePoints = new Vector3[20];
    //细分长度,每一小段的长度
    public float subdivision = .02f;
    //发射的力度
    public float force;

    //发射点
    public Transform startPos;


    //发射物体
    public GameObject ball;

    //发射物体的受重力
    private Vector3 gravityScale;

    private void Start()
    {
        gravityScale = ball.GetComponent<Rigidbody2D>().gravityScale * (Vector3)Physics2D.gravity;
    }
    private void Update()
    {
        CalculateParabola();



        Shoot();
    }

    /// <summary>
    /// 计算
    /// </summary>
    private void CalculateParabola()
    {
        for (int i = 0; i < linePoints.Length; i++)
        {
            //3D
            //linePoints[i] = startPos.position + startPos.forward * force * i
            //*subdivision + Physics.gravity * (0.5f * (i * subdivision) * (i * subdivision));

            //2D
            linePoints[i] = startPos.position + startPos.right * force * i * subdivision + gravityScale * (0.5f * (i * subdivision) * (i * subdivision));

            DrawLine();
        }
    }

    /// <summary>
    /// 画线
    /// </summary>
    private void DrawLine()
    {
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    /// <summary>
    /// 发射
    /// </summary>
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //3D
            //GameObject obj = Instantiate(ball3D,startPos.position,startPos.rotation);
            //obj.GetComponent<Rigidbody>().AddForce(startPos.forward * force, ForceMode.VelocityChange);

            //2D
            GameObject obj = Instantiate(ball, startPos.position, startPos.rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(startPos.right * force, ForceMode2D.Impulse);
        }
    }
}
