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

    bool mainMenuActive = true;

    bool pauseMenuActive = false;

    bool UDMActive = false;

    bool settingsMenuActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        blackBackground.SetActive(true);
        studioLogo.SetActive(true);


        playButtonMain.SetActive(true);
        exitButtonMain.SetActive(true);
        settingsButtonMain.SetActive(true);
        mainMenu.SetActive(true);

        playButton.SetActive(false);
        exitButton.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);

        settingsMenu.SetActive(false);
        UDMcheckBox.SetActive(false);
        UDMcheck.SetActive(false);
        backButton.SetActive(false);

        stage.SetActive(false);

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

            if(playButtonMain.GetComponent<playButtonMain>().clicked){

                mainMenuActive = false;
                playButtonMain.SetActive(false);
                exitButtonMain.SetActive(false);
                settingsButtonMain.SetActive(false);
                mainMenu.SetActive(false);
                stage.SetActive(true);
                playButtonMain.GetComponent<playButtonMain>().clicked = false;

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

                playButton.SetActive(true);
                exitButton.SetActive(true);
                settingsButton.SetActive(true);
                pauseMenu.SetActive(true);
                pauseMenuActive = true;

                Time.timeScale = 0;

            }

            // The pause menu is closed, esc can open it
            else if(pauseMenuActive && !settingsMenuActive && (playButton.GetComponent<playButton>().clicked || Input.GetKeyDown("escape"))){
                
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

        }

    }

}
