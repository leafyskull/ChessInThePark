using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;


public class Board : MonoBehaviour
{
    public static Board Instance;
    private List<Coordinate> coordinates;
    private int numRows = 8;
    private int numColumns = 8;

    void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

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

    public Coordinate GetCoordinate(Coordinate coord)
    {
        Column column = coord.getColumn();
        Row row = coord.getRow();

        foreach (Coordinate coordinate in coordinates)
        {
            if (coordinate.getColumn() == column && coordinate.getRow() == row)
            {
                return coordinate;
            }
        }

        return null;
    }



}