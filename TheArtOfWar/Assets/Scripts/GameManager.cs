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

    bool mainMenuActive = true;

    bool pauseMenuActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        playButtonMain.SetActive(true);
        exitButtonMain.SetActive(true);
        settingsButtonMain.SetActive(true);
        mainMenu.SetActive(true);

        playButton.SetActive(false);
        exitButton.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);

        stage.SetActive(false);

    }

    // Update is called once per frame
    void Update(){

        
        if(mainMenuActive){

            if(playButtonMain.GetComponent<playButtonMain>().clicked){

                mainMenuActive = false;
                playButtonMain.SetActive(false);
                exitButtonMain.SetActive(false);
                settingsButtonMain.SetActive(false);
                mainMenu.SetActive(false);

                stage.SetActive(true);

            }

        }
        else{

            // The pause menu is open, play and esc can close it
            if(!pauseMenuActive && Input.GetKeyDown("escape")){

                playButton.SetActive(true);
                exitButton.SetActive(true);
                settingsButton.SetActive(true);
                pauseMenu.SetActive(true);
                pauseMenuActive = true;

                Time.timeScale = 0;

            }

            // The pause menu is closed, esc can open it
            else if(pauseMenuActive && (playButton.GetComponent<playButton>().clicked || Input.GetKeyDown("escape"))){
                
                playButton.SetActive(false);
                exitButton.SetActive(false);
                settingsButton.SetActive(false);
                pauseMenu.SetActive(false);
                pauseMenuActive = false;
                playButton.GetComponent<playButton>().clicked = false;

            }

        }

    }

}
