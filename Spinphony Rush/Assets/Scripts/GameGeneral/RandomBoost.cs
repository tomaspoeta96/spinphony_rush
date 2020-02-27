using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RandomBoost : MonoBehaviour {
    public int DISTANCE_PHONY_BOOST = 6;

    private int index = 0;
    public GameObject[] boosts = new GameObject[4];
    private List<int> indexInMap = new List<int>();
    public float elapsedTime = 0f;
    public float repeatTime = 5f;
    public bool on_map_Jump = false;
    public bool on_map_Shield = false;
    public bool on_map_Fuelle = false;
    public bool on_map_Move = false;
    public float Yboost;
    private Vector3 auxPosition;
    private List<Vector3> vectors;
    public String fileName;

    private void Start() {

        vectors = getPointsFromFile(Application.dataPath + "/DataMaps/" + fileName);
        index = UnityEngine.Random.Range(0, boosts.Length);
    }
    void Update() {
        vectors = shuffle(vectors);
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= repeatTime) {
            while (true)
            {
                if (indexInMap.Contains(index)) { 
                    index = UnityEngine.Random.Range(0, boosts.Length);
                    break;
                }
                else
                {
                    indexInMap.Add(index);
                }
            }
            GameObject[] phonies = GameObject.FindGameObjectsWithTag("Phony");
            float distance = Mathf.Infinity;
            GameObject closest = null;

            
            Vector3 rand = vectors[UnityEngine.Random.Range(0, vectors.Count - 1)];
            auxPosition.x = rand.x;
            auxPosition.z = rand.z;
            auxPosition.y = Yboost;

            GameObject[] concat = new GameObject[phonies.Length + boosts.Length];

            for(int i = 0; i < phonies.Length ; i++ )
            {
                concat[i] = phonies[i];
            }

            for (int i = 0; i < boosts.Length; i++)
            {
                concat[i+phonies.Length] = boosts[i];
            }

            foreach (GameObject go in concat )
            {
                Vector3 diff = go.transform.position - auxPosition;
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

    public void boostPicked(PhonyPlayerController playerCtlr, Collider c) {
        switch (c.gameObject.name)
        {
            case "JumpBoost":
                playerCtlr.setHaveJump(true);
                on_map_Jump = false;
                playerCtlr.playEffect(c.gameObject);
                c.gameObject.GetComponentInChildren<Animator>().SetBool("PickedSalto", true);
                removeIndexMap(c.gameObject);
                c.gameObject.SetActive(false);
                break;
            case "ShieldBoost":
                playerCtlr.setHaveShield(true);
                on_map_Shield = false;
                playerCtlr.playEffect(c.gameObject);
                c.gameObject.GetComponentInChildren<Animator>().SetBool("PickedShield", true);
                removeIndexMap(c.gameObject);
                c.gameObject.SetActive(false);
                break;
            case "FuelleBoost":
                playerCtlr.setHaveFuelle(true);
                on_map_Fuelle = false;
                playerCtlr.playEffect(c.gameObject);
                c.gameObject.GetComponentInChildren<Animator>().SetBool("PickedFuelle", true);
                removeIndexMap(c.gameObject);
                c.gameObject.SetActive(false);
                break;
            case "MoveBoost":
                playerCtlr.setHaveMove(true);
                on_map_Move = false;
                playerCtlr.playEffect(c.gameObject);
                c.gameObject.GetComponentInChildren<Animator>().SetBool("PickedHandle", true);
                removeIndexMap(c.gameObject);
                c.gameObject.SetActive(false);
                break;
        }
    }

    private void removeIndexMap(GameObject boost) {
        for(int i = 0; i< boosts.Length; i++) {
            if(boosts[i].name == boost.name) {
                indexInMap.Remove(i);
            }
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

    private List<Vector3> shuffle(List<Vector3> list) {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
