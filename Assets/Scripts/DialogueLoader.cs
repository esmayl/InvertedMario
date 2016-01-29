using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;



struct DialogLine
{
    public string characterName;
    public string dialogue;
    public int characterSprite;

    public DialogLine(string c, string content, int spriteType)
    {
        characterName = c;
        dialogue = content;
        characterSprite = spriteType;
    }
}

public class DialogueLoader : MonoBehaviour
{

    List<DialogLine> dialogValues = new List<DialogLine>();
    public bool isDone = false;
    static object locker = new object();

	void Start ()
	{
	    string fileName = "IntroDialog.txt";
        new Thread(() => LoadDialogFile(fileName) ).Start();
	}

    public void LoadDialogFile(string fileName)
    {
        string path = "Assets/Resources/" + fileName;

        string line;

        StreamReader reader = new StreamReader(path);

        using (reader)
        {
            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string[] values = line.Split(',');
                    DialogLine fileLine = new DialogLine(values[0],values[1],int.Parse(values[2]));
                    Debug.Log(fileLine.characterName+"\n"+fileLine.dialogue+"\n"+fileLine.characterSprite.ToString());
                    dialogValues.Add(fileLine);
                }
            } 
            
            while (line!=null);
            reader.Close();
        }

        //makes the isDone threadsafe
        lock (locker)
        {
            isDone = true;       
        }
    }

    public string GetCharacterName(int lineNr)
    {
        if (lineNr < dialogValues.Count)
        {
            return dialogValues[lineNr].characterName;
        }
        return "";
    }

    public string GetText(int lineNr)
    {
        if (lineNr < dialogValues.Count)
        {
            return dialogValues[lineNr].dialogue;
        }
        return "";
    }

    public int GetSpriteType(int lineNr)
    {
        if (lineNr < dialogValues.Count)
        {
            return dialogValues[lineNr].characterSprite;
        }
        return -1;
    }
}
