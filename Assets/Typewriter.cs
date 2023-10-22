using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{
    //access tmp text
    TMP_Text tmpText;
    //access the text going to be written
    string writer;
    //time between characters
    [SerializeField] float timeBtwChars = 0.1f;
    //leading character
    [SerializeField] string leadingChar = "";

    // Use this for initialization
    void Start()
    {
        //get component of tmp text
        tmpText = GetComponent<TMP_Text>()!;

        if (tmpText != null)
        {
            //set written out text to text on tmpro object
            writer = tmpText.text;
            //start with no text
            tmpText.text = "";
            //begin coroutine
            StartCoroutine("TypeWriterTMP");
        }
    }

    IEnumerator TypeWriterTMP()
    {
        
        foreach (char c in writer)
        {
            //if text length is more than 0
            if (tmpText.text.Length > 0)
            {
                tmpText.text = tmpText.text.Substring(0, tmpText.text.Length - leadingChar.Length);
            }
            //add characters from writer
            tmpText.text += c;
            //add leading character
            tmpText.text += leadingChar;
            //intervals between each character
            yield return new WaitForSeconds(timeBtwChars);
        }
        //if there is leading characters
        if (leadingChar != "")
        {
            tmpText.text = tmpText.text.Substring(0, tmpText.text.Length - leadingChar.Length);
        }
    }
}