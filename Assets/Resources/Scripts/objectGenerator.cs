using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGenerator : MonoBehaviour
{

    //variable declarations
    string myName="";
    float temperature = 0.0f;
    int age = 0;

    GameObject square,squareParent;
    // Start is called before the first frame update
    void Start()
    {
       squareParent = new GameObject();
       squareParent.name = "Cross-parent";
       squareParent.transform.position = new Vector3(0f,0f);
       

       for (int counter = 0; counter < 4; counter++)
       {
           Debug.Log(counter); 
            //1. Load square template from resources
            square = Resources.Load<GameObject>("Prefabs/Square");
            //2. Instantiate a square in the MIDDLE of the screen at 0 degree rotation
            GameObject tempSquare = Instantiate(square,new Vector3(counter,0f),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            
             GameObject tempSquare2 = Instantiate(square,new Vector3(0f,counter),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare2.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            //4 more lines to finish the cross
             GameObject tempSquare3 = Instantiate(square,new Vector3(0f,-counter),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare3.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            GameObject tempSquare4 = Instantiate(square,new Vector3(-counter,0f),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            




        }

    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
