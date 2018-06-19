using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text OfficerText;
    public Text PilotText;
    public Text ActiveText;
    public Text OfficerNameText;
    public Text PilotNameText;
    public Text ActiveNameText;
    public Image OfficerPic;
    public Image PilotPic;
    public Image ActivePic;

    private List<List<string>> DialogueList = new List<List<string>>();

    public Queue<string> sentences;

    public enum Speaker
    {
        Officer,
        Pilot
    };

    Speaker speaker = Speaker.Pilot;

	// Use this for initialization
	void Start ()
    {
        sentences = new Queue<string>();
	}
	
	public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue + " + dialogue.name);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        
        if(speaker == Speaker.Officer)
        {
            speaker = Speaker.Pilot;
        }
        else
        {
            speaker = Speaker.Officer;
        }

        string sentence = sentences.Dequeue();

        SetDialogueSpeaker(speaker);

        ActiveText.text = sentence;
        print(sentence);
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }

    public void SetDialogueSpeaker(Speaker speaker)
    {
        if(speaker == Speaker.Officer)
        {
            // Enable Officer components
            OfficerText.enabled = OfficerNameText.enabled = OfficerPic.enabled = true;            

            // Disable pilot components
            PilotText.enabled = PilotNameText.enabled = PilotPic.enabled = false;       

            // Set officer as active
            ActiveText = OfficerText;
            ActiveNameText = OfficerNameText;
            ActivePic = OfficerPic;
        }
        else
        {
            // Disable Officer components
            OfficerText.enabled = OfficerNameText.enabled = OfficerPic.enabled = false;

            // Enable pilot components
            PilotText.enabled = PilotNameText.enabled = PilotPic.enabled = true;

            // Set pilot as active
            ActiveText = PilotText;
            ActiveNameText = PilotNameText;
            ActivePic = PilotPic;
        }
    }

    /*public void DisableStartButton()
    {
        DisableStartButton = GameObject.Find("")
    }*/
}



/*
* On Scene load, Start Dialogue corresponding to level
* Have skip button that enables Launch button
* Have next button that cycles dialogue
* When dialogue complete, disable Next button and enable Launch
* */