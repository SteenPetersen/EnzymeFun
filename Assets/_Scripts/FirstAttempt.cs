using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttempt : MonoBehaviour {

    [SerializeField]
    TextAsset tracks;                   // Reference to the Tracks_ statistics data
    [SerializeField]
    TextAsset linksToTracks;            // Reference to the Links_In_Tracks_Statistics data

    [SerializeField]
    string[] lines;                     // a string array to hold the lines in each parsed script

    Dictionary<int, string[]> valuesInLine = new Dictionary<int, string[]>();
    Dictionary<int, List<Vector2>> plots = new Dictionary<int, List<Vector2>>();

    char lineSeperator = '\n';          // character with which to seperate by lines
    char fieldSeperator = ',';          // character with which to seperate by lines

    [SerializeField]
    GameObject enzyme;                  // the gameobject that will represent the enzymes

    [SerializeField]
    int numberOfFieldsParsed = 0;

    List<Vector2> tmpVectors = new List<Vector2>(); // temporary list used to fill the Plot dictionary for each enzyme.

    private void Start()
    {
        SetUpLines(linksToTracks);
        SetUpFields(lines);
        CreatePlotDictionary();
    }

    private void Update()
    {
        SpawnEnzyme();
    }

    private void SpawnEnzyme()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    var enzymeObj = Instantiate(enzyme, null, true);
        //    var positions = plots[1];

        //    var script = enzymeObj.GetComponent<EnzymeMovement>();
        //    script.placesToGo = positions;
        //    enzymeObj.transform.position = positions[0];

        //    foreach (var item in plots[1])
        //    {
        //        Debug.Log(item);
        //    }

        //}
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        if (Input.GetKeyDown(KeyCode.T))
        {

        }

    }

    private void CreatePlotDictionary()
    {
        List<int> keyList = new List<int>(this.valuesInLine.Keys);
        List<Vector2> vectors = new List<Vector2>();

        var currentEnzyme = 0;
        var previousEnzyme = 0;

        foreach (var key in keyList)
        {
            //Debug.Log(key);
            var lineOfValues = valuesInLine[key];


            int.TryParse(lineOfValues[2], out currentEnzyme);
            Debug.Log(currentEnzyme);

            if (currentEnzyme == 0)
                continue;

            if (currentEnzyme == 1)
            {
                float enzymeXPos = 0.0000f;
                float enzymeYPos = 0.0000f;

                float.TryParse(lineOfValues[7], out enzymeXPos);
                float.TryParse(lineOfValues[8], out enzymeYPos);

                Vector2 tmp = new Vector2(enzymeXPos, enzymeYPos);

                vectors.Add(tmp);
                Debug.Log(tmp);

                previousEnzyme = currentEnzyme;

                continue;
            }

            Debug.Log(vectors.Count);

            foreach (var item in vectors)
            {
                Debug.Log(item);
            }

            Debug.Log(previousEnzyme);

            plots.Add(previousEnzyme, vectors);

            Debug.Log("ending");
            return;

            // Debug.Log(lineOfValues[2]);
            // go through every single value

        }
    }

    public void SetUpLines(TextAsset csvFile)
    {
        lines = csvFile.text.Split(lineSeperator);
    }

    void SetUpFields(string[] lines)
    {
        foreach (var line in lines)
        {
            if (line != "")
            {
                string[] fields = line.Split(fieldSeperator);

                this.valuesInLine.Add(numberOfFieldsParsed, fields);

                numberOfFieldsParsed++;
            }

        }
    }
}
