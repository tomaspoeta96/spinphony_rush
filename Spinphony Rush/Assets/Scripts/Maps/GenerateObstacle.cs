using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateObstacle : MonoBehaviour
{
    public GameObject map;
    private Mesh mapMesh;
    private Collider mapCollider;
    private Bounds colliderBounds;
    private Vector3 randomCoord;
    public Transform obstacle;
    public int maxCountObstacles;
    private String obstacleName;
    public float position;
    public float howFarRayCast;
    private int i = 0;
    private List<Vector3> vectors;
    private List<GameObject> goList = new List<GameObject>();
    private bool hasGeneratedObstacle = false;
    private float elapsedTimeSpawn = 0f;
    private float timerDurationSpawn;
    private float elapsedTimeDelete = 0f;
    private float timerDurationDelete;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        mapCollider = map.GetComponent<Collider>();
        mapMesh = map.GetComponent<MeshFilter>().mesh;
        colliderBounds = mapCollider.bounds;
        vectors = getPointsFromFile(Application.dataPath + "/DataMaps/TreeRandomPoints.txt");
        obstacleName = obstacle.name;
        timerDurationSpawn = UnityEngine.Random.Range(10, 20);
        timerDurationDelete = UnityEngine.Random.Range(12, 17);
    }

    void Update()
    {

        elapsedTimeSpawn += Time.deltaTime;
        if (elapsedTimeSpawn >= timerDurationSpawn)
        {
            if (goList.Count < maxCountObstacles)
            {
                foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
                {
                    if (go.name == obstacleName)
                    {
                        goList.Add(go);
                    }

                }
                Vector3 auxPosition = vectors[UnityEngine.Random.Range(0, vectors.Count - 1)];
                auxPosition.y = position;
                if (!(goList.Count == 0))
                {
                    if (Vector3.Distance(goList[goList.Count - 1].transform.position, auxPosition) >= 2)
                    {
                        obstacle = Instantiate(obstacle) as Transform;
                        obstacle.name = obstacleName;                            
                        obstacle.position = auxPosition;
                    }
                }
                else
                {
                    obstacle = Instantiate(obstacle) as Transform;
                    obstacle.name = obstacleName;
                    obstacle.position = auxPosition;
                }
            }
            timerDurationSpawn = UnityEngine.Random.Range(5, 10);
            elapsedTimeSpawn = 0f;
        }

        elapsedTimeDelete += Time.deltaTime;
        if(elapsedTimeDelete >= timerDurationDelete)
        {
            if(goList.Count > 0)
            {
                int indexRemoved = UnityEngine.Random.Range(0, goList.Count);
                GameObject toDestroy = goList[indexRemoved];
                goList.RemoveAt(indexRemoved);
                try
                {
                    toDestroy.GetComponent<ObstacleBehaviour>().setDestroySignal(true);
                } catch(NullReferenceException e)
                {

                }
                
            }
            timerDurationDelete = UnityEngine.Random.Range(5, 10);
            elapsedTimeDelete = 0f;
        }
        
    }

    private void generatePoints()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(UnityEngine.Random.Range(colliderBounds.min.x, colliderBounds.max.x), position, UnityEngine.Random.Range(colliderBounds.min.z, colliderBounds.max.z)), -Vector3.up, out hit, howFarRayCast))
        {
            if (hit.point.y == 68.46669f)
            {
                obstacle = Instantiate(obstacle) as Transform;
                obstacle.transform.position = hit.point;
                i += 1;
                if (i <= 200)
                {
                    savePreset(Application.dataPath + "/DataMaps/TreeRandomPoints.txt", hit.point);
                }
            }
        }
    }

    public void savePreset(string doc, Vector3 vect)
    {
        File.AppendAllText(doc, vect+"\n");
    }
    public List<Vector3> getPointsFromFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        List<Vector3> vectors = new List<Vector3>();
        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            inp_ln = inp_ln.Substring(1, inp_ln.Length - 2);
            string[] vectorValues = inp_ln.Split(',');
            Vector3 result = new Vector3(
             float.Parse(vectorValues[0], System.Globalization.CultureInfo.InvariantCulture.NumberFormat),
             float.Parse(vectorValues[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat),
             float.Parse(vectorValues[2], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            vectors.Add(result);
        }
        inp_stm.Close();
        return vectors;
    }



}
