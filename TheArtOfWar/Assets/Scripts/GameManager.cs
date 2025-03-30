using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        
        playButton.SetActive(false);
        exitButton.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update(){

        if(playButton.GetComponent<playButton>().clicked == true){

            playButton.SetActive(false);
            exitButton.SetActive(false);
            settingsButton.SetActive(false);
            pauseMenu.SetActive(false);

        }

        if(Input.GetKeyDown("escape")){

            playButton.GetComponent<playButton>().clicked = false;
            playButton.SetActive(true);
            exitButton.SetActive(true);
            settingsButton.SetActive(true);
            pauseMenu.SetActive(true);

        }

    }
}
