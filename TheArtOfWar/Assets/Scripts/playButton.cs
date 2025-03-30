using UnityEngine;

public class playButton : MonoBehaviour
{
    public void closePauseMenu(){

        this.gameObject.SetActive(false);
        print("it worked");
    }
}
