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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        bool mainMenu = true;
        playButtonMain.SetActive(true);
        exitButtonMain.SetActive(true);
        settingsButtonMain.SetActive(true);
        mainMenu.SetActive(true);

        playButton.SetActive(false);
        exitButton.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update(){

        
        if(mainMenu){
            //if(playButtonMain.GetComponent<playButton>().clicked){}


        }
        else{
            if(playButton.GetComponent<playButton>().clicked){

                playButton.SetActive(false);
                exitButton.SetActive(false);
                settingsButton.SetActive(false);
                pauseMenu.SetActive(false);

            }
            else if(Input.GetKeyDown("escape")){

                playButton.GetComponent<playButton>().clicked = false;
                playButton.SetActive(true);
                exitButton.SetActive(true);
                settingsButton.SetActive(true);
                pauseMenu.SetActive(true);

            }

        }

    }

}
