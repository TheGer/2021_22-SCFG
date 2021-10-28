using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectGenerator : MonoBehaviour
{

    //Reaction Time Game
    //------------------
    //Our reaction time is measured by the time taken for us to see something
    //and take action about it.

    //We will need to ask the user to input his/her name.

    //In this case, we are going to generate a square every RANDOM interval
    //for 15 times. We are going to measure the time FOR EACH square generated
    //between the square being generated, and us clicking on the square.

    //Each box will be a round, so we will inform the user before each box is going 
    //to be generated, measure the reaction time, and move to the next round, informing
    //the user of which round he/she is in.

    //After 15 rounds, the average reaction time of the user will be shown and the game
    //ends.

    bool usingMouse,gameStarted;

    public float keyboardSpeed;

    float[] reactionTimes;

    string playerName;

    int squarecounter;

    Text inputSelectorText, roundTimerText;

    GameObject square, parentObject;
    
    //UI Variables
    GameObject hudPrefab,menuPrefab,hudInstance,menuInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        //the name of the prefab in the actual folder is CaseSenSiTive
        menuPrefab = Resources.Load<GameObject>("Prefabs/StartMenu");
        //hud prefab in the same way
        hudPrefab = Resources.Load<GameObject>("Prefabs/HUD");
        //draw the menu in the middle of the screen
        menuInstance = Instantiate(menuPrefab,Vector3.zero,Quaternion.identity);
    }


    void startRound()
    {
        //get the input selector text
        inputSelectorText = GameObject.Find("InputSelector").GetComponent<Text>();
        //round timer text
        roundTimerText = GameObject.Find("RoundTimer").GetComponent<Text>();

        inputSelectorText.text = "M";

        usingMouse = true;

        squarecounter = 0;
        //1. Load square template from resources
        square = Resources.Load<GameObject>("Prefabs/Square");
        //2. Instantiate a square in the MIDDLE of the screen at 0 degree rotation
        // GameObject tempSquare = Instantiate(square,new Vector3(0f,0f),Quaternion.identity); 
        //3. Set a random colour for the square
        //  tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //4. Find the coordinates of the edges of the square
        generateNSquares(5);

        parentObject.transform.localScale = new Vector3(0.25f, 0.25f);
    }
    //generate N squares horizontally

    //modify this code to generate a full row, a full column, one diagonal going up
    //and the opposite as well (similar to the English flag)

    GameObject makeOneSquare(float x, float y, GameObject myparentobject)
    {
        GameObject tempSquare = Instantiate(square, new Vector3(x, y), Quaternion.identity);
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
        for (int counter = -numberOfSquares; counter <= numberOfSquares; counter++)
        {
            makeOneSquare(counter, 0f, parentObject);
            makeOneSquare(0f, counter, parentObject);
            makeOneSquare(counter, counter, parentObject);
            makeOneSquare(counter, -counter, parentObject);
        }
    }

    void mouseControl()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 asterixPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0f));

        parentObject.transform.position = new Vector3(asterixPosition.x, asterixPosition.y);
    }

    void keyboardControl(float keyspeed)
    {
        parentObject.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * keyspeed * Time.deltaTime);
        parentObject.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * keyspeed * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (usingMouse)
            {
                if (inputSelectorText.text != "M")
                    inputSelectorText.text = "M";
                mouseControl();

            }
            else
            {
                if (inputSelectorText.text != "K")
                    inputSelectorText.text = "K";
                keyboardControl(keyboardSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                usingMouse = !usingMouse;
            }
        }

    }
}
