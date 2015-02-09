using UnityEngine;
using System.Collections;

public class SwarmController : MonoBehaviour 
{
    public int SwarmSize;
    private ParticleSystem.Particle[] points;
    GameObject[] swarm;
    float[] angleThetaIncreases;
    float[] anglePhiIncreases;
    float[] startAngleTheta;
    float[] startAnglePhi;

    GameObject TheWholeSwarm;

    private void CreatePoints()
    {
        points = new ParticleSystem.Particle[SwarmSize];
        swarm = new GameObject[SwarmSize];
        angleThetaIncreases = new float[SwarmSize];
        anglePhiIncreases = new float[SwarmSize];
        startAngleTheta = new float[SwarmSize];
        startAnglePhi = new float[SwarmSize];
        for (int i = 0; i < points.Length; i++)
        {
            angleThetaIncreases[i] = Random.Range(-1f,1f);
            anglePhiIncreases[i] = angleThetaIncreases[i];
            startAngleTheta[i] = Random.Range(0, 2*Mathf.PI);
            startAnglePhi[i] = Random.Range(0, 2*Mathf.PI);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i]));
            
            GameObject bug = (GameObject)Instantiate(Resources.Load("AlienSwarm"));
            GameObject bugCenter = new GameObject("Bug"+i.ToString());
            bugCenter.transform.position = bug.transform.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            bug.transform.SetParent(bugCenter.transform);
            bugCenter.transform.SetParent(TheWholeSwarm.transform);
            bugCenter.transform.position = points[i].position;
            swarm[i] = bugCenter;
        }
    }
    void Start()
    {
        TheWholeSwarm = new GameObject("AlienSwarm");
        TheWholeSwarm.transform.SetParent(transform);
        CreatePoints();
        particleSystem.SetParticles(points, points.Length);
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            startAnglePhi[i] += anglePhiIncreases[i] * Time.deltaTime * Random.Range(1.0f, 1.5f);
            startAngleTheta[i] += angleThetaIncreases[i] * Time.deltaTime * Random.Range(1.0f, 1.5f);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i])) + transform.position;
            swarm[i].transform.position = points[i].position;
        }
        particleSystem.SetParticles(points, points.Length);
        transform.up = -transform.parent.transform.right;
    }
}
