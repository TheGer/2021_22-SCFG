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


    //After the player enters his/her name and presses 'start', I want to show the following
    //instruction: 
    //A square will appear on a random location of the screen, click on it as quickly as you can
    //after 1second, I want that text to become a countdown from 3
    //when the countdown is 0, the countdown disappears
    //after a random interval, a box is spawned and the time is taken
    //I will then measure the time taken for the player to react to the box being spawned
    //that number is the score of round 1.


    //Each round of the game, will work as follows:
    //1. I will wait for a random interval, after which I will generate a random square 
    //   on screen.
    //2. I will save the time when the random square was generated
    //3. When the player click ON THE SQUARE only, the square will disappear and the round ends.
    //4. I will display the reaction time of that round and the round number
    //5. Countdown again, and back to 1.

    
    void playRound()
    {


        StartCoroutine(generateRandomSquare());
    }


    IEnumerator gotoNextRound()
    {
        //first show the countdown
        currentRound++;
        countdownCounter = 3;
        yield return showInstructions();
      
    }

    GameObject enemyBoxParent;

    IEnumerator generateRandomSquare()
    {
        //1. Wait a random interval
        yield return new WaitForSeconds(Random.Range(0f,2f));
        //save the time when the box is generated
        timeToCompareTo = Time.time;
        float randomX = Random.Range(-4.5f,4.5f);
        float randomY = Random.Range(-4.5f,4.5f);
    
        GameObject enemySquare = makeOneSquare(randomX,randomY,enemyBoxParent);
        enemySquare.AddComponent<BoxCollider2D>();
        //coroutine ends here
        yield return null;

    }


    bool usingMouse, gameStarted;

    public float keyboardSpeed;

    float[] reactionTimes;

    string playerName;

    int squarecounter;

    int currentRound;

    Text inputSelectorText, roundTimerText;

    GameObject square, parentObject;

    //UI Variables
    GameObject hudPrefab, menuPrefab, countDownPrefab, hudInstance, menuInstance, countDownInstance;

    float timeToCompareTo;



    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;

        reactionTimes = new float[10];

        currentRound = 1;
        //the name of the prefab in the actual folder is CaseSenSiTive
        menuPrefab = Resources.Load<GameObject>("Prefabs/StartMenu");
        //hud prefab in the same way
        hudPrefab = Resources.Load<GameObject>("Prefabs/HUD");
        //countdown prefab in the same way
        countDownPrefab = Resources.Load<GameObject>("Prefabs/CountDown");

        //group all enemy boxes under one parent
        enemyBoxParent = new GameObject();
        //draw the menu in the middle of the screen
        setupMenu();
    }


    void setupMenu()
    {
        menuInstance = Instantiate(menuPrefab, Vector3.zero, Quaternion.identity);

        //PlayerName + StartButton
        GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(
            () =>
            {
                playerName = GameObject.Find("PlayerName").GetComponent<InputField>().text;
                Debug.Log("Player name is: " + playerName);
                //destroy the menu
                Destroy(menuInstance);

               
                //start the round
                StartCoroutine(showInstructions());

            }
        );

    }

    //show instructions for one second
    IEnumerator showInstructions()
    {
        //show on screen
        
        countDownInstance = Instantiate(countDownPrefab, Vector3.zero, Quaternion.identity);
        if (currentRound == 1)
        {
            countDownInstance.GetComponentInChildren<Text>().text =
        "A square will appear on a random location of the screen!\nClick on it as quickly as you can!";
        }
        else
        {
            countDownInstance.GetComponentInChildren<Text>().text =
                "Get ready for Round: " + currentRound;
        }
        yield return new WaitForSeconds(1f);

        //round counter in the top left corner
        roundTimerText.text = currentRound.ToString();
        Debug.Log(countdownCounter);
        yield return countDown();
    }

    int countdownCounter = 3;
    //count down from 3
    IEnumerator countDown()
    {
        while (countdownCounter > 0)
        {
            countDownInstance.GetComponentInChildren<Text>().text = countdownCounter.ToString();
            yield return new WaitForSeconds(1f);
            countdownCounter--;
        }
        Destroy(countDownInstance);
        startGame();
        yield return null;
    }


    //when escape is pressed while the round is started, the hud and game should stop and I should go back
    //to the menu.

    void backToMenu()
    {
        Destroy(hudInstance);
        Destroy(parentObject);
        gameStarted = false;
        setupMenu();
    }

    int counter = 0;
    void showDurationBetweenClicks()
    {
        counter++;
        Debug.Log(counter + ":" + (Time.time - timeToCompareTo));
        timeToCompareTo = Time.time;
    }

    void startGame()
    {
       
        timeToCompareTo = Time.time;
        //get the input selector text
        
        //1. Load square template from resources
        square = Resources.Load<GameObject>("Prefabs/Square");
        //2. Instantiate a square in the MIDDLE of the screen at 0 degree rotation
        // GameObject tempSquare = Instantiate(square,new Vector3(0f,0f),Quaternion.identity); 
        //3. Set a random colour for the square
        //  tempSquare.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //4. Find the coordinates of the edges of the square
        if (currentRound == 1)
        {
            hudInstance = Instantiate(hudPrefab, Vector3.zero, Quaternion.identity);

            generateNSquares(5);

            parentObject.transform.localScale = new Vector3(0.25f, 0.25f);
        }

        inputSelectorText = GameObject.Find("InputSelector").GetComponent<Text>();
        //round timer text
        roundTimerText = GameObject.Find("RoundTimer").GetComponent<Text>();

        inputSelectorText.text = "M";

        usingMouse = true;

        squarecounter = 0;


        gameStarted = true;
        playRound();
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
        //this method will use raycast to shoot a ray in 
        //this means left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(asterixPosition,Vector3.forward);
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
                float reactiontime = Time.time - timeToCompareTo;
                //-1 becase arrays start from 0
                reactionTimes[currentRound - 1] = reactiontime;
                Debug.Log(reactiontime);
                if (currentRound < 11) { 
                    StartCoroutine(gotoNextRound());
                }
                else
                {
                    StartCoroutine(showAverageReactionTime());
                }
            }
        }

    }

    IEnumerator showAverageReactionTime()
    {

        float totalTimes = 0;
        float avgTime = 0;
        foreach(float rtime in reactionTimes)
        {
            totalTimes += rtime;
        }

        GameObject highScoreInstance = Instantiate(countDownPrefab, Vector3.zero, Quaternion.identity);

        avgTime = totalTimes / reactionTimes.Length;

        highScoreInstance.GetComponentInChildren<Text>().text = "Congratulations Player: "+ playerName + " your average reaction time is: "
            + avgTime.ToString() ;


        yield return new WaitForSeconds(3f);
        backToMenu();
    }

    void keyboardControl(float keyspeed)
    {
        parentObject.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * keyspeed * Time.deltaTime);
        parentObject.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * keyspeed * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        //only happens if gameStarted is true
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backToMenu();
            }

          
        }

    }
}
