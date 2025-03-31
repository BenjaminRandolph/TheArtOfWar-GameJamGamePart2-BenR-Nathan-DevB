using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject playButtonMain;

    [SerializeField]
    GameObject exitButtonMain;

    [SerializeField]
    GameObject settingsButtonMain;

    [SerializeField]
    GameObject playButton;

    [SerializeField]
    GameObject exitButton;

    [SerializeField]
    GameObject settingsButton;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject stage;

    [SerializeField]
    GameObject blackBackground;

    [SerializeField]
    GameObject studioLogo;

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject UDMcheckBox;

    [SerializeField]
    GameObject UDMcheck;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject backButton;

    [SerializeField]
    GameObject leftArrow1;

    [SerializeField]
    GameObject leftArrow2;

    [SerializeField]
    GameObject rightArrow1;

    [SerializeField]
    GameObject rightArrow2;

    [SerializeField]
    GameObject characterSelector;

    [SerializeField]
    GameObject selectorPlayButton;

    [SerializeField]
    GameObject p1HealthText;

    [SerializeField]
    GameObject p2HealthText;

    GameObject p1;

    GameObject p2;

    [SerializeField]
    GameObject p1WinScreen;

    [SerializeField]
    GameObject p2WinScreen;

    [SerializeField]
    GameObject box1;

    [SerializeField]
    GameObject box2;

    [SerializeField]
    GameObject tri1;

    [SerializeField]
    GameObject tri2;

    [SerializeField]
    GameObject star1;

    [SerializeField]
    GameObject star2;

    [SerializeField]
    GameObject wolf1;

    [SerializeField]
    GameObject wolf2;

    [SerializeField]
    GameObject robot1;

    [SerializeField]
    GameObject robot2;

    [SerializeField]
    GameObject sm1;

    [SerializeField]
    GameObject sm2;

    bool mainMenuActive = true;

    bool pauseMenuActive = false;

    bool UDMActive = false;

    bool settingsMenuActive = false;

    bool selectorActive = false;

    int p1Health = 100;

    int p2Health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        blackBackground.SetActive(true);
        studioLogo.SetActive(true);

        playButtonMain.SetActive(true);
        exitButtonMain.SetActive(true);
        settingsButtonMain.SetActive(true);
        mainMenu.SetActive(true);

        characterSelector.SetActive(false);
        leftArrow1.SetActive(false);
        leftArrow2.SetActive(false);
        rightArrow1.SetActive(false);
        rightArrow2.SetActive(false);
        selectorPlayButton.SetActive(false);

        playButton.SetActive(false);
        exitButton.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);

        settingsMenu.SetActive(false);
        UDMcheckBox.SetActive(false);
        UDMcheck.SetActive(false);
        backButton.SetActive(false);

        p1WinScreen.SetActive(false);
        p2WinScreen.SetActive(false);

        stage.SetActive(false);
        p1HealthText.SetActive(false);
        p2HealthText.SetActive(false);

    }

    // Update is called once per frame
    void Update(){

        if(studioLogo.GetComponent<SpriteFader>().logoScene){   // Logo Cutscene
        
            if(Input.anyKeyDown){

                blackBackground.SetActive(false);
                studioLogo.SetActive(false);

            }
        
        }
        else{

            blackBackground.SetActive(false);

        }
        
        if(mainMenuActive){ // Main Menu

            if(playButtonMain.GetComponent<playButtonMain>().clicked){  // Character Selector

                mainMenuActive = false;
                playButtonMain.SetActive(false);
                exitButtonMain.SetActive(false);
                settingsButtonMain.SetActive(false);
                mainMenu.SetActive(false);
                playButtonMain.GetComponent<playButtonMain>().clicked = false;

                characterSelector.SetActive(true);
                leftArrow1.SetActive(true);
                leftArrow2.SetActive(true);
                rightArrow1.SetActive(true);
                rightArrow2.SetActive(true);
                selectorPlayButton.SetActive(true);
                selectorActive = true;

            }
            
            if(exitButtonMain.GetComponent<exitButtonMain>().clicked){

                exitButtonMain.GetComponent<exitButtonMain>().clicked = false;
                Application.Quit();

            }

            if(settingsButtonMain.GetComponent<settingsButtonMain>().clicked){  // Settings Menu (Main)

                settingsButtonMain.GetComponent<settingsButtonMain>().clicked = false;
                playButtonMain.SetActive(false);
                exitButtonMain.SetActive(false);
                settingsButtonMain.SetActive(false);
                mainMenu.SetActive(false);

                settingsMenu.SetActive(true);
                UDMcheckBox.SetActive(true);
                backButton.SetActive(true);
                settingsMenuActive = true;
                
                if(!UDMActive){

                    UDMcheck.SetActive(false);

                }
                else{

                    UDMcheck.SetActive(true);

                }

            }

            if(UDMcheckBox.GetComponent<checkBox>().clicked && UDMcheck.activeSelf){

                UDMcheck.SetActive(false);
                UDMActive = false;
                mainCamera.transform.Rotate(0, 0, -180);

            }
            else if(UDMcheckBox.GetComponent<checkBox>().clicked){

                UDMcheck.SetActive(true);
                UDMActive = true;
                mainCamera.transform.Rotate(0, 0, 180);

            }
            UDMcheckBox.GetComponent<checkBox>().clicked = false;

            if(backButton.GetComponent<backButton>().clicked || Input.GetKeyDown("escape") && settingsMenuActive){

                backButton.GetComponent<backButton>().clicked = false;
                playButtonMain.SetActive(true);
                exitButtonMain.SetActive(true);
                settingsButtonMain.SetActive(true);
                mainMenu.SetActive(true);

                settingsMenu.SetActive(false);
                UDMcheckBox.SetActive(false);
                backButton.SetActive(false);
                UDMcheck.SetActive(false);
                settingsMenuActive = false;

            }

        }
        else{   // Pause Menu

            // The pause menu is open, play and esc can close it
            if(!pauseMenuActive && !settingsMenuActive && Input.GetKeyDown("escape")){

                characterSelector.SetActive(false);
                leftArrow1.SetActive(false);
                leftArrow2.SetActive(false);
                rightArrow1.SetActive(false);
                rightArrow2.SetActive(false);

                selectorPlayButton.SetActive(false);
                
                playButton.SetActive(true);
                exitButton.SetActive(true);
                settingsButton.SetActive(true);
                pauseMenu.SetActive(true);
                pauseMenuActive = true;
                p1HealthText.SetActive(false);
                p2HealthText.SetActive(false);

                Time.timeScale = 0;

            }

            // The pause menu is closed, esc can open it
            else if(pauseMenuActive && !settingsMenuActive && (playButton.GetComponent<playButton>().clicked || Input.GetKeyDown("escape"))){
                
                if(selectorActive){

                    characterSelector.SetActive(true);
                    leftArrow1.SetActive(true);
                    leftArrow2.SetActive(true);
                    rightArrow1.SetActive(true);
                    rightArrow2.SetActive(true);
                    selectorPlayButton.SetActive(true);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);

                }
                else{

                    p1HealthText.SetActive(true);
                    p2HealthText.SetActive(true);

                }

                playButton.SetActive(false);
                exitButton.SetActive(false);
                settingsButton.SetActive(false);
                pauseMenu.SetActive(false);
                pauseMenuActive = false;
                playButton.GetComponent<playButton>().clicked = false;

                Time.timeScale = 1;

            }

            if(pauseMenuActive && exitButton.GetComponent<exitButton>().clicked){

                playButton.SetActive(false);
                exitButton.SetActive(false);
                settingsButton.SetActive(false);
                pauseMenu.SetActive(false);
                stage.SetActive(false);
                pauseMenuActive = false;
                exitButton.GetComponent<exitButton>().clicked = false;
                
                mainMenuActive = true;
                playButtonMain.SetActive(true);
                exitButtonMain.SetActive(true);
                settingsButtonMain.SetActive(true);
                mainMenu.SetActive(true);

            }

            if(settingsButton.GetComponent<settingsButton>().clicked){  // Settings Menu (Pause)

                settingsButton.GetComponent<settingsButton>().clicked = false;
                playButton.SetActive(false);
                exitButton.SetActive(false);
                settingsButton.SetActive(false);
                pauseMenu.SetActive(false);

                settingsMenu.SetActive(true);
                UDMcheckBox.SetActive(true);
                backButton.SetActive(true);
                settingsMenuActive = true;
                
                if(!UDMActive){

                    UDMcheck.SetActive(false);

                }
                else{

                    UDMcheck.SetActive(true);

                }


            }

            if(UDMcheckBox.GetComponent<checkBox>().clicked && UDMcheck.activeSelf){

                UDMcheck.SetActive(false);
                UDMActive = false;
                mainCamera.transform.Rotate(0, 0, -180);

            }
            else if(UDMcheckBox.GetComponent<checkBox>().clicked){

                UDMcheck.SetActive(true);
                UDMActive = true;
                mainCamera.transform.Rotate(0, 0, 180);

            }
            UDMcheckBox.GetComponent<checkBox>().clicked = false;

            if(backButton.GetComponent<backButton>().clicked || Input.GetKeyDown("escape") && settingsMenuActive){

                backButton.GetComponent<backButton>().clicked = false;
                playButton.SetActive(true);
                exitButton.SetActive(true);
                settingsButton.SetActive(true);
                pauseMenu.SetActive(true);

                settingsMenu.SetActive(false);
                UDMcheckBox.SetActive(false);
                backButton.SetActive(false);
                UDMcheck.SetActive(false);
                settingsMenuActive = false;

            }

            bool wolf1bool = false;
            bool wolf2bool = false;
            bool robot1bool = false;
            bool robot2bool = false;
            bool sm1bool = false;
            bool sm2bool = false;

            if(selectorPlayButton.GetComponent<selectorPlayButton>().clicked){  // Start Game

                selectorPlayButton.GetComponent<selectorPlayButton>().clicked = false;
                characterSelector.SetActive(false);
                leftArrow1.SetActive(false);
                leftArrow2.SetActive(false);
                rightArrow1.SetActive(false);
                rightArrow2.SetActive(false);
                selectorPlayButton.SetActive(false);
                selectorActive = false;

                
                if(box1.activeSelf){
                    p1 = Instantiate(wolf1, new Vector3(-2, 0, 0),  Quaternion.identity);
                    wolf1bool = true;
                }else if(tri1.activeSelf){
                    p1 = Instantiate(sm1, new Vector3(-2, 0, 0),  Quaternion.identity);
                    sm1bool = true;
                }else{
                    p1 = Instantiate(robot1, new Vector3(-2, 0, 0),  Quaternion.identity);
                    robot1bool = true;
                }

                if(box2.activeSelf){
                    p2 = Instantiate(wolf2, new Vector3(2, 0, 0),  Quaternion.identity);
                    wolf2bool = true;
                }else if(tri2.activeSelf){
                    p2 = Instantiate(sm2, new Vector3(2, 0, 0),  Quaternion.identity);
                    sm2bool = true;
                }else{
                    p2 = Instantiate(robot2, new Vector3(2, 0, 0),  Quaternion.identity);
                    robot2bool = true;
                }
                

                stage.SetActive(true);
                p1HealthText.SetActive(true);
                p2HealthText.SetActive(true);

            }

            if(wolf1bool){
                p1HealthText.GetComponent<healthText>().setText("Health: " + p1.GetComponent<PlayerControllerWolf>().health);
                if(p1.GetComponent<PlayerControllerWolf>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p2WinScreen.SetActive(true);

                }
            }else if(sm1bool){
                p1HealthText.GetComponent<healthText>().setText("Health: " + p1.GetComponent<PlayerControllerStick>().health);
                if(p1.GetComponent<PlayerControllerStick>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p2WinScreen.SetActive(true);

                }
            }else{
                p1HealthText.GetComponent<healthText>().setText("Health: " + p1.GetComponent<PlayerControllerRobot>().health);
                if(p1.GetComponent<PlayerControllerRobot>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p2WinScreen.SetActive(true);

                }
            }

            if(wolf2bool){
                p2HealthText.GetComponent<healthText>().setText("Health: " + p2.GetComponent<PlayerControllerWolf>().health);
                if(p2.GetComponent<PlayerControllerWolf>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p1WinScreen.SetActive(true);

                }
            }else if(sm2bool){
                p2HealthText.GetComponent<healthText>().setText("Health: " + p2.GetComponent<PlayerControllerStick>().health);
                if(p2.GetComponent<PlayerControllerStick>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p1WinScreen.SetActive(true);

                }
            }else{
                p2HealthText.GetComponent<healthText>().setText("Health: " + p2.GetComponent<PlayerControllerRobot>().health);
                if(p2.GetComponent<PlayerControllerRobot>().health <= 0){

                    stage.SetActive(false);
                    p1HealthText.SetActive(false);
                    p2HealthText.SetActive(false);
                    p1WinScreen.SetActive(true);

                }
            }
        }

    }

}
