using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject floor, wall;
    struct level
    {
        public level(int x, int y, int lvl, int[,] lo)
        {
            sizeX = x;
            sizeY = y;
            lvlNo = lvl;
            layout = lo;
        }

        public int sizeX, sizeY, lvlNo;
        public int[,] layout;
    }


    // Start is called before the first frame update
    void Start()
    {
        level lvl;
        lvl.sizeX = 20;
        lvl.sizeY = 20;
        lvl.lvlNo = 0;
        lvl.layout = new int[,]{ 
                       { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                       { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2},
                       { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2, 1, 2, 1, 2, 2, 1, 2},
                       { 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2, 2, 1, 2},
                       { 2, 1, 1, 1, 2, 2, 1, 2, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2},
                       { 2, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 1, 2},
                       { 2, 1, 1, 1, 1, 1, 2, 2, 1, 2, 2, 2, 2, 1, 1, 1, 1, 2, 1, 2},
                       { 2, 1, 2, 2, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 2, 1, 2},
                       { 2, 1, 1, 1, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 1, 2, 1, 1, 1, 2},
                       { 2, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 2, 1, 2, 2, 1, 2, 2},
                       { 2, 1, 2, 1, 2, 2, 2, 2, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2},
                       { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 2, 2, 1, 2},
                       { 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2},
                       { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 1, 2},
                       { 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1, 2},
                       { 2, 1, 2, 1, 2, 1, 1, 1, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2},
                       { 2, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2},
                       { 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2},
                       { 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
                       { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
                     };
        GenerateLevel(lvl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLevel(level lvl)
    {
        for (int y = 0; y < lvl.sizeY; y++)
        {
            for (int x = 0; x < lvl.sizeX; x++)
            {
                SpawnTile(x, lvl.sizeY - y, lvl.layout[y,x]);
            }
        }
    }

    void SpawnTile(int x, int y, int type)
    {
        switch (type)
        {
            case 0: //Blank
                break;
            case 1: //floor
                Instantiate(floor, new Vector2(x, y), Quaternion.identity);
                break;
            case 2: //wall
                Instantiate(wall, new Vector2(x, y), Quaternion.identity);
                break;
        }
    }
}
