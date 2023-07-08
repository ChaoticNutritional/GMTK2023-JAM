using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToDungeon : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(mazeSpawner.Columns * .5f, 0, mazeSpawner.Rows * .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
