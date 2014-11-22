using UnityEngine;
using System.Collections;

public class ParticleCirclesSphere : MonoBehaviour 
{
    private ParticleSystem.Particle[] points;
    float[] angleThetaIncreases;
    float[] anglePhiIncreases;
    float[] startAngleTheta;
    float[] startAnglePhi;

    private void CreatePoints()
    {
        points = new ParticleSystem.Particle[50];
        angleThetaIncreases = new float[50];
        anglePhiIncreases = new float[50];
        startAngleTheta = new float[50];
        startAnglePhi = new float[50];
        for (int i = 0; i < points.Length; i++)
        {
            angleThetaIncreases[i] = Random.Range(-1f,1f);
            anglePhiIncreases[i] = angleThetaIncreases[i];
            startAngleTheta[i] = Random.Range(0, 2*Mathf.PI);
            startAnglePhi[i] = Random.Range(0, 2*Mathf.PI);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i]));
            
            points[i].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0, 1f));
            points[i].size = Random.Range(0.4f,0.6f);
        }
    }
    void Start()
    {
        CreatePoints();
        particleSystem.SetParticles(points, points.Length);
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            startAnglePhi[i] += anglePhiIncreases[i] * Time.deltaTime * Random.Range(1.0f, 2.0f);
            startAngleTheta[i] += angleThetaIncreases[i] * Time.deltaTime * Random.Range(1.0f, 2.0f);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i]));
        }
        particleSystem.SetParticles(points, points.Length);
    }
}
