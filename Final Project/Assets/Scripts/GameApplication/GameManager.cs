using UnityEngine;
using System.Collections;
using UnityEngine.UI;


    using System.Collections.Generic;       //Allows us to use Lists. 

    public class GameManager : MonoBehaviour
    {
        public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.        
        public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.        
        private int level = 0;                                  //Current level number, expressed in game as "Day 1".
        private List<Enemy> enemies;                            //List of all Enemy units, used to issue them move commands.
        private List<Character> characters;                     // List of all characters
        private int enemiesToSpawn = 0;                         // How many enemies to spawn for the level
        private int enemiesRemaining = 0;
        public GameObject enemyFactory;                         // Instance of EnemyFactory
        public GameObject[] players;
        private Text levelText;                                 //Text to display current level number.
        private GameObject levelImage;                          //Image to block out level as levels are being set up, background for levelText.  
        private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.
        
        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            //Assign enemies to a new List of Enemy objects.
            enemies = new List<Enemy>();

            //Assign characters to a new List of characters
            characters = new List<Character>();

            //Call the InitGame function to initialize the first level 
            InitGame();
        }

        //This is called each time a scene is loaded.
        void OnLevelWasLoaded(int index)
        {
            //Add one to our level number.
            level++;
            //Call InitGame to initialize our level.
            InitGame();
        }

        /*public void updateEnemiesToSpawn()
        {
            enemiesToSpawn--;
        }*/

        public int getEnemiesToSpawn()
        {
            return enemiesToSpawn;
        }

        //Initializes the game for each level.
        void InitGame()
        {
            //While doingSetup is true the player can't move, prevent player from moving while title card is up.
            doingSetup = true;

            //Get a reference to our image LevelImage by finding it by name.
            levelImage = GameObject.Find("LevelImage");

            //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
            levelText = GameObject.Find("LevelText").GetComponent<Text>();

            //Set the text of levelText to the string "Day" and append the current level number.
            levelText.text = "Wave " + level;

            //Set levelImage to active blocking player's view of the game board during setup.
            levelImage.SetActive(true);

            //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
            Invoke("HideLevelImage", levelStartDelay);

            //Clear any Enemy objects in our List to prepare for next level.
            enemies.Clear();

            //Call the SetupScene function of the LevelManager script, pass it current level number.
            enemiesToSpawn = level + 5;//(int)Mathf.Log(level, 2f);

            players = GameObject.FindGameObjectsWithTag("Player");
    }

    //Hides black image used between levels
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        doingSetup = false;

        //Check if EnemyFactory has already been assigned to static variable GameManager.instance or if it's still null
        
        Instantiate(enemyFactory);        

        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Player>().GetCharacterScript() == null)
            {
                Debug.Log("instantiating character");
                GameObject character = players[i].GetComponent<Player>().currentCharacter;
                GameObject characterInstance = Instantiate(character, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                players[i].GetComponent<Player>().SetCharacterScript(characterInstance);
            }
        }
        

    }

    //Update is called every frame.
        void Update()
        {
            if (doingSetup)
                return;

            if (checkIfGameOver())
                StartCoroutine(GameOver());        

        }

        bool checkIfGameOver()
        {
            bool gameOverStatus = false;
            for (int i = 0; (i < characters.Count); i++)
            {
                if (characters[i].checkIfDead() == false)
                {
                    gameOverStatus = false;
                    break;                    
                }
                else
                {
                    gameOverStatus = true;
                }                  
                    
            }
            return gameOverStatus;
        }

        //Call this to add the passed in Enemy to the List of Enemy objects.
        public void AddEnemyToList(Enemy script)
        {
            //Add Enemy to List enemies.
            enemies.Add(script);
            enemiesToSpawn--;
        }

        public void AddCharacterToList(Character script)
        {
            //Add Enemy to List enemies.
            characters.Add(script);
        }


        //GameOver is called when the players reach 0 hp
        IEnumerator GameOver()
        {

            yield return new WaitForSeconds(2);

            //Enable black background image gameObject.
            levelImage.SetActive(true);

            //Disable this GameManager.
            enabled = false;

            enemies.Clear();

            levelText.text = "After " + level + " waves, you were defeated.";
        }

        // Check if all characters dead

        
    }
