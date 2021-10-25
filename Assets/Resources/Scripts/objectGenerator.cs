using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGenerator : MonoBehaviour
{

    //variable declarations
    //variable 1
    string myName="";
    //variable 2
    float temperature = 0.0f;
    //variable 3
    int age = 0;

    bool mouseControl = true;


    //variable 5
    //2 float variables to save the mouse position
    float mouseX,mouseY;

    //variable 6
    Vector3 mouseWorldCoordinates;

    //variable 4
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

            tempSquare.transform.parent = squareParent.transform;

          
            
             GameObject tempSquare2 = Instantiate(square,new Vector3(0f,counter),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare2.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            tempSquare2.transform.parent = squareParent.transform;

            //4 more lines to finish the cross
             GameObject tempSquare3 = Instantiate(square,new Vector3(0f,-counter),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare3.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            tempSquare3.transform.parent = squareParent.transform;

            GameObject tempSquare4 = Instantiate(square,new Vector3(-counter,0f),Quaternion.identity); 
            //3. Set a random colour for the square
            tempSquare4.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            tempSquare4.transform.parent = squareParent.transform;
            


        }

        //set the scale to 0.25 size
        squareParent.transform.localScale = new Vector3(0.25f,0.25f);

        Debug.Log("Time when game started: " + Time.time);

    }

        // Update is called once per frame

    //MEASURE:  The time difference between each click.  

    //First click shows how long since start

    //Second click shows how long since first click

    //third click shows how long between second click and third click

    //use a method to return a string to debug.log
    

    void Update()
    {
        //if mousecontrol is true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mouseControl = !mouseControl;
            Debug.Log("mouseControl:"+mouseControl.ToString());
        }



        if (mouseControl) 
        {
            mouseX = Input.mousePosition.x;
            mouseY = Input.mousePosition.y;

            mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(mouseX,mouseY));

            squareParent.transform.position= new Vector3(mouseWorldCoordinates.x,mouseWorldCoordinates.y);

            //click the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Time when clicked: " + Time.time);
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                squareParent.transform.position += new Vector3(0f, 1f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                squareParent.transform.position -= new Vector3(0f, 1f);
            }

            //horizontal is up to you
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                squareParent.transform.position -= new Vector3(1f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                squareParent.transform.position += new Vector3(1f, 0f);
            }

        }
        //get mouse coordinates 
      
        //Task to do for next lesson

        //the square should EITHER move with the keyboard OR with the mouse.  If you press SPACE it will move with
        //the keyboard, and if you press M it will move with the mouse.


        

    }
}
