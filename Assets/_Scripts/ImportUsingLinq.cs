using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using LINQtoCSV;
using UnityEngine.UI;
using System.IO;

public class ImportUsingLinq : MonoBehaviour {

    [SerializeField] TextAsset links;

    [SerializeField] Text dataDemo;  // for debugging purposes

    [SerializeField] Product[] products;

    void Start()
    {
        CsvFileDescription inputFileDescription = new CsvFileDescription
        {
            SeparatorChar = ';',
            FirstLineHasColumnNames = true,
            EnforceCsvColumnAttribute = true,
            IgnoreUnknownColumns = true
        };

        using (var ms = new MemoryStream())
        {
            using (var txtWriter = new StreamWriter(ms))
            {
                using (var txtReader = new StreamReader(ms))
                {
                    txtWriter.Write(links.text);
                    txtWriter.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    CsvContext cc = new CsvContext();
                    products = cc.Read<Product>(txtReader, inputFileDescription).ToArray();

                    //foreach (Product item in products)
                    //{
                    //    dataDemo.text += string.Format(item.TRACK_ID.ToString());
                    //    Debug.Log(item.TRACK_ID);
                    //}
                }
            }
        }

        Debug.Log("Finished Reading data");
    }

}

class Product
{
    //[CsvColumn(Name = "ProductName", FieldIndex = 1)]
    //public string Name { get; set; }
    //[CsvColumn(FieldIndex = 2, OutputFormat = "dd MMM HH:mm:ss")]
    //public DateTime LaunchDate { get; set; }
    //[CsvColumn(FieldIndex = 3, CanBeNull = false, OutputFormat = "C")]
    //public decimal Price { get; set; }
    //[CsvColumn(FieldIndex = 4)]
    //public string Country { get; set; }
    //[CsvColumn(FieldIndex = 5)]
    //public string Description { get; set; }

    [CsvColumn(Name = "Label", FieldIndex = 0)]
    public int Label { get; set; }
    [CsvColumn(Name = "TRACK_ID", FieldIndex = 1)]
    public int TRACK_ID { get; set; }

    //public int SPOT_SOURCE_ID { get; set; }
    //public int SPOT_TARGET_ID { get; set; }
    //public float LINK_COST { get; set; }
    //public float EDGE_TIME { get; set; }


    //[CsvColumn(Name = "EDGE_X_LOCATION", FieldIndex = 2)]
    //public float EDGE_X_LOCATION { get; set; }
    //[CsvColumn(Name = "EDGE_Y_LOCATION", FieldIndex = 3)]
    //public float EDGE_Y_LOCATION { get; set; }


    //public float EDGE_Z_LOCATION { get; set; }
    //public float VELOCITY { get; set; }
    //public float DISPLACEMENT { get; set; }
    //public string MANUAL_COLOR { get; set; }


}
