using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RandomBoost : MonoBehaviour {
    public int DISTANCE_PHONY_BOOST = 6;
    public GameObject[] boosts = new GameObject[16];
    public PhonyPlayerController phony;
    private int index = 0;
    public float elapsedTime = 0f;
    public float repeatTime = 5f;
    public bool on_map_Jump = false;
    public bool on_map_Shield = false;
    public bool on_map_Fuelle = false;
    public bool on_map_Move = false;
    private Vector3 auxPosition;
    private List<Vector3> vectors;
    public String fileName;

    private void Start() {
        vectors = getPointsFromFile(Application.dataPath + "/DataMaps/" + fileName);
    }
    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= repeatTime) {
            index = UnityEngine.Random.Range (0, boosts.Length);
            GameObject[] phonies = GameObject.FindGameObjectsWithTag("Phony");
            float distance = Mathf.Infinity;
            GameObject closest = null;
            
            Vector3 rand = vectors[UnityEngine.Random.Range(0, vectors.Count - 1)];
            auxPosition.x = rand.x;
            auxPosition.z = rand.z;
            auxPosition.y = 72f;
            

            foreach (GameObject go in phonies)
            {
                Vector3 diff = go.transform.position - boosts[index].transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            try
            {
                switch (boosts[index].name)
                {
                    case "JumpBoost":
                        if (!on_map_Jump)
                        {
                            if (Vector3.Distance(boosts[index].transform.position, closest.transform.position) > DISTANCE_PHONY_BOOST)
                            {
                                boosts[index].SetActive(true);
                                boosts[index].transform.position = auxPosition;
                            }
                            on_map_Jump = true;
                            boosts[index].GetComponentInChildren<Animator>().SetBool("DisplaySalto", true);
                        }
                        break;
                    case "ShieldBoost":
                        if (!on_map_Shield)
                        {
                            if (Vector3.Distance(boosts[index].transform.position, closest.transform.position) > DISTANCE_PHONY_BOOST)
                            {

                                boosts[index].SetActive(true);
                                boosts[index].transform.position = auxPosition;
                            }
                            on_map_Shield = true;
                            boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayShield", true);
                        }
                        break;
                    case "FuelleBoost":
                        if (!on_map_Fuelle)
                        {
                            if (Vector3.Distance(boosts[index].transform.position, closest.transform.position) > DISTANCE_PHONY_BOOST)
                            {
                                boosts[index].SetActive(true);
                                boosts[index].transform.position = auxPosition;
                            }
                            on_map_Fuelle = true;
                            boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayFuelle", true);
                        }
                        break;
                    case "MoveBoost":
                        if (!on_map_Move)
                        {
                            if (Vector3.Distance(boosts[index].transform.position, closest.transform.position) > DISTANCE_PHONY_BOOST)
                            {
                                boosts[index].SetActive(true);
                                boosts[index].transform.position= auxPosition;
                            }
                            on_map_Move = true;
                            boosts[index].GetComponentInChildren<Animator>().SetBool("DisplayHandle", true);
                        }
                        break;
                }
            } catch(System.NullReferenceException ex)
            {

            }
            elapsedTime -= repeatTime;
        }
    }

    public List<Vector3> getPointsFromFile(string file_path) {
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
