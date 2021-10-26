using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGenerator : MonoBehaviour
{

    //variable declarations
    //variable 1
    string myName = "";
    //variable 2
    float temperature = 0.0f;
    //variable 3
    int age = 0;

    float keySpeed = 25f;

    bool mouseControl = true;

    float timeToCompareTo;

    int clickCounter;
    //variable 5
    //2 float variables to save the mouse position
    float mouseX, mouseY;

    //variable 6
    Vector3 mouseWorldCoordinates;

    //variable 4
    GameObject square, squareParent;


    GameObject startGameMenuPrefab,tempMenu;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
        if (gameStarted)
        {
            squareParent = new GameObject();
            squareParent.name = "Cross-parent";
            squareParent.transform.position = new Vector3(0f, 0f);
            square = Resources.Load<GameObject>("Prefabs/Square");
            for (int counter = 0; counter < 4; counter++)
            {
                Debug.Log(counter);
                //1. Load square template from resources

                //2. Instantiate a square in the MIDDLE of the screen at 0 degree rotation
                GameObject tempSquare = Instantiate(square, new Vector3(counter, 0f), Quaternion.identity);
                //3. Set a random colour for the square
                tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                tempSquare.transform.parent = squareParent.transform;



                GameObject tempSquare2 = Instantiate(square, new Vector3(0f, counter), Quaternion.identity);
                //3. Set a random colour for the square
                tempSquare2.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                tempSquare2.transform.parent = squareParent.transform;

                //4 more lines to finish the cross
                GameObject tempSquare3 = Instantiate(square, new Vector3(0f, -counter), Quaternion.identity);
                //3. Set a random colour for the square
                tempSquare3.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                tempSquare3.transform.parent = squareParent.transform;

                GameObject tempSquare4 = Instantiate(square, new Vector3(-counter, 0f), Quaternion.identity);
                //3. Set a random colour for the square
                tempSquare4.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                tempSquare4.transform.parent = squareParent.transform;



            }

            //set the scale to 0.25 size
            squareParent.transform.localScale = new Vector3(0.25f, 0.25f);
            timeToCompareTo = Time.time;
            Debug.Log("Time when game started: " + Time.time);
        } else {
            //load the menu from file
            startGameMenuPrefab = Resources.Load<GameObject>("Prefabs/StartGame");
            //create the menu in the middle of the screen
            tempMenu = Instantiate(startGameMenuPrefab,Vector3.zero,Quaternion.identity);

        }
    }

    // Update is called once per frame

    //MEASURE:  The time difference between each click.  

    //First click shows how long since start

    //Second click shows how long since first click

    //third click shows how long between second click and third click

    //use a method to return a string to debug.log

    //calculateDuration(20f,10f) - 10f
    //calculateDuration(10f,20f) + 10f
    float calculateDuration(float compareTo, float current)
    {
        return current - compareTo;
    }


    void Update()
    {
        //if mousecontrol is true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mouseControl = !mouseControl;
            Debug.Log("mouseControl:" + mouseControl.ToString());
        }



        if (mouseControl)
        {
            mouseX = Input.mousePosition.x;
            mouseY = Input.mousePosition.y;

            mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY));

            squareParent.transform.position = new Vector3(mouseWorldCoordinates.x, mouseWorldCoordinates.y);

            //click the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                clickCounter++;
                Debug.Log(calculateDuration(timeToCompareTo, Time.time) + " clicks:" + clickCounter);
                timeToCompareTo = Time.time;

            }


        }
        else
        {
            //set up the movement here
            squareParent.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * keySpeed * Time.deltaTime);
            squareParent.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * keySpeed * Time.deltaTime);

        }
        //get mouse coordinates 

        //Task to do for next lesson

        //the square should EITHER move with the keyboard OR with the mouse.  If you press SPACE it will move with
        //the keyboard, and if you press M it will move with the mouse.




    }
}
