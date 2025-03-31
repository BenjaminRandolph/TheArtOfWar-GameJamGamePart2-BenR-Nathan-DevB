using UnityEngine;
using TMPro;

public class healthText : MonoBehaviour
{

    public TMP_Text m_TextComponent;

    public void setText(string text)
    {
    
        m_TextComponent = GetComponent<TMP_Text>();

        m_TextComponent.text = text;

    }
    
}
