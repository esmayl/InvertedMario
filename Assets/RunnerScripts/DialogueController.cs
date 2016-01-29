using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class DialogueController : MonoBehaviour
{
    public Text characterName1Text;
    public Text characterName2Text;
    public Text contentText;

    DialogueLoader loader;
    FontData currentFontData;

    int currentDialogNr
    {
        get { return _currentDialogNr; }
        set
        {
            _currentDialogNr = value;
            SetNewDialogText();
        }
    }

    int _currentDialogNr;

    void Awake ()
    {
        Time.timeScale = 0;

        loader = GameObject.Find("DialogueLoader").GetComponent<DialogueLoader>();

        StartCoroutine("WaitForThread");

    }

    private IEnumerator WaitForThread()
    {
        while (!loader.isDone)
        {
            Debug.Log(loader.isDone);
            yield return null;
        }
        currentDialogNr = 0;

        SetCharacterName();
        SetCharacterName2();
        SetNewDialogText();
        
        yield break;
    }


    void Update () 
    {
	    if (Input.GetMouseButtonDown(0))
	    {
	        currentDialogNr ++;

	        if (loader.GetCharacterName(currentDialogNr) == "")
	        {
	            ReActivateGame();
	        }
	    }
	}

    private void ReActivateGame()
    {
        characterName1Text.transform.parent.gameObject.SetActive(false);
        characterName2Text.transform.parent.gameObject.SetActive(false);
        contentText.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetCharacterName2()
    {
        string s = loader.GetCharacterName(currentDialogNr+1);
        if (s != null)
        {
            characterName2Text.text = s;
        }
    }

    private void SetCharacterName()
    {
        string s = loader.GetCharacterName(currentDialogNr);
        if (s != null)
        {
            characterName1Text.text = s;
        }
    }

    private void SetNewDialogText()
    {
        string s = loader.GetText(currentDialogNr);
        if (s != null)
        {
            contentText.text = s;
        }
    }
}
