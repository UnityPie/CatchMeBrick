using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{   
    public LayerMask Mask;
    public GameObject ShootPos;
    public Camera mainCamera;
    //Подключаем LineRenderer отрисовываем 100 точек и запускаем линию по формуле расчитываем траекторию
    public LineRenderer lineRendererComponent;

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points1 = new Vector3[1000];
        Vector3[] points2 = new Vector3[1000];
        lineRendererComponent.positionCount = points1.Length;

        for (int i = 0; i < points1.Length; i++)
        {
            float time1 = i * 0.01f;
            float time2 = time1 / 1.5f;

            points1[i] = origin + speed * time1 + Physics.gravity * time1 * time1 / 2f;
            points2[i] = origin + speed * time2 + Physics.gravity * time2 * time2 / 3f;

            RaycastHit hit;
            if(Physics.Linecast(points1[i], points2[i], out hit))
            {
                if(hit.collider.tag != "Player" && hit.collider.isTrigger == false)
                {
                    lineRendererComponent.positionCount = i;
                    break;
                }
            }
        }
        lineRendererComponent.SetPositions(points1);
    }
}
