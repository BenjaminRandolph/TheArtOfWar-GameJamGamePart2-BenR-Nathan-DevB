using UnityEngine;

public class charSwitch : MonoBehaviour
{
    private int counter = 2;
    [SerializeField]
    private GameObject char1;
    [SerializeField]
    private GameObject char2;
    [SerializeField]
    private GameObject char3;



    public void switchChar()
    {
        if (counter == 1) {
            char1.SetActive(true);
            char2.SetActive(false);
            char3.SetActive(false);
        }
        if (counter == 2) {
            char1.SetActive(false);
            char2.SetActive(true);
            char3.SetActive(false);
        }
        if (counter == 3) {
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(true);
        }

        
        if (counter >= 4) {
            counter = 1;
            char1.SetActive(true);
            char2.SetActive(false);
            char3.SetActive(false);
        }
        counter++;
    }

    public void switchCharBack()
    {
        if (counter == 1) {
            char1.SetActive(true);
            char2.SetActive(false);
            char3.SetActive(false);
        }
        if (counter == 2) {
            char1.SetActive(false);
            char2.SetActive(true);
            char3.SetActive(false);
        }
        if (counter == 3) {
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(true);
        }

        
        if (counter <= 0) {
            counter = 3;
            char1.SetActive(false);
            char2.SetActive(false);
            char3.SetActive(true);
        }
        counter--;
    }
}



//If clicked, make var = a number that count one 
// If counted to 4 then set back to 1 and run conditions