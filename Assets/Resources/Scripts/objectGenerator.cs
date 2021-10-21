using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGenerator : MonoBehaviour
{

    GameObject square,parentObject;
    // Start is called before the first frame update
    void Start()
    {
        squarecounter = 0;
       //1. Load square template from resources
       square = Resources.Load<GameObject>("Prefabs/Square");
       //2. Instantiate a square in the MIDDLE of the screen at 0 degree rotation
      // GameObject tempSquare = Instantiate(square,new Vector3(0f,0f),Quaternion.identity); 
       //3. Set a random colour for the square
     //  tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
       //4. Find the coordinates of the edges of the square
        generateNSquares(5);

        parentObject.transform.localScale = new Vector3(0.25f,0.25f);
    }

    //generate N squares horizontally

    //modify this code to generate a full row, a full column, one diagonal going up
    //and the opposite as well (similar to the English flag)
    int squarecounter;
    GameObject makeOneSquare(float x,float y,GameObject myparentobject)
    {
        GameObject tempSquare = Instantiate(square,new Vector3(x,y),Quaternion.identity); 
        squarecounter++;
        tempSquare.name = "Square-" + squarecounter;
        tempSquare.transform.parent = myparentobject.transform;
        tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        return tempSquare;
    }
    void generateNSquares(int numberOfSquares)
    {
        parentObject = new GameObject();
        parentObject.name = "allSquares";
        //for loop to generate N squares horizontally
        for(int counter = -numberOfSquares; counter<= numberOfSquares; counter++)
        {
           makeOneSquare(counter,0f,parentObject); 
           makeOneSquare(0f,counter,parentObject); 
           makeOneSquare(counter,counter,parentObject);
           makeOneSquare(counter,-counter,parentObject);
        }
    }


        // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 asterixPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX,mouseY,0f));

        parentObject.transform.position = new Vector3(asterixPosition.x,asterixPosition.y);
        
    }
}
