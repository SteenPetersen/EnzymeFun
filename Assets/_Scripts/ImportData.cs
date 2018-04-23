using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ImportData : MonoBehaviour {

    [SerializeField]
    GameObject pacman;

    [SerializeField]
    TextAsset links;

    char fieldSeperator = ',';
    char lineSeperator = '\n';

    [SerializeField]
    string[] lines;

    Dictionary<int, List<Vector2>> data = new Dictionary<int, List<Vector2>>();

    /// <summary>
    /// A public property from which outside scripts can fetch the information in in the "data" dictionary
    /// </summary>
    public Dictionary<int, List<Vector2>> MyData
    {
        get
        {
            return data;
        }

    }


    // Use this for initialization
    void Start () {
        SetUpLines(links);
        SetUpFields(lines);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Populates the lines array with every line in the provided csv file
    /// </summary>
    /// <param name="csvFile">a .CSV file to be parsed</param>
    public void SetUpLines(TextAsset csvFile)
    {
        lines = csvFile.text.Split(lineSeperator);
    }

    /// <summary>
    /// Extracts the fields from each line in the lines array and subsequently places those values in 
    /// the Data dictionary where the Key is the track number, and it's value is a list of Vector2 structs.
    /// </summary>
    /// <param name="lines"></param>
    void SetUpFields(string[] lines)
    {
        // run though all lines
        foreach (var line in lines)
        {
            // if the line is not empty and the line is not the first lines
            if (line != "" && line != lines[0])
            {
                // initialize a list of strings called "valuesInLine" and split the line at every comma to populate it
                List<string> valuesInLine = line.Split(fieldSeperator).ToList();

                // create a new Vector2 that takes the 7th and 8th value in the valuesInLine list
                // casts them as floats and set them as the Vector2 values.
                Vector2 vectorInThisLine = new Vector2(float.Parse(valuesInLine[7]), float.Parse(valuesInLine[8]));

                // if the data dictionary already has the key
                if (data.ContainsKey(int.Parse(valuesInLine[2])))
                {
                    // then simply add the vector2 to the list it already contains
                    data[int.Parse(valuesInLine[2])].Add(vectorInThisLine);
                }
                else
                {
                    // otherwise add a new Key with and set its values to be a new list with the 
                    // current vector2 as its first entry
                    data.Add(int.Parse(valuesInLine[2]), new List<Vector2>() { vectorInThisLine });
                }
            }

        }

        //foreach (KeyValuePair<int, Vector2> kvp in data)
        //{
        //    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        //    textBox3.Text += string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        //}
    }
}
