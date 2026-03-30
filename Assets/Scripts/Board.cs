using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Board : MonoBehaviour
{
    private List<Coordinate> coordinates;
    private int numRows = 8;
    private int numColumns = 8;

    void Start()
    {
        this.coordinates = new List<Coordinate>();

        for (int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                Column column = (Column)i;
                Row row = (Row)j;
                coordinates.Add(new Coordinate(column, row));
                
                Debug.Log($"Coordinate generated on board: {column}{row}");
            }
        }
    }



}