using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    
    public Text ActiveText;
    private static Color BlueUI = new Color(0.2117647f, 0.7333333f, 0.9607844f);
    private static Color YellowUI = new Color(0.9686275f, 0.9058824f, 0.3372549f);
    public Queue<string> sentences;
    private GameObject LaunchButton;

    public enum Speaker { Officer, Pilot };

    Speaker speaker = Speaker.Pilot;

    public Dialogue dialogue;

    // Use this for initialization
    void Awake()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);
        LaunchButton = GameObject.Find("ButtonLaunchNextMission");
        LaunchButton.SetActive(false);
    }
	
	public void StartDialogue(Dialogue dialogue)
    {
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
            ActiveText.alignment = TextAnchor.UpperRight;
            ActiveText.color = BlueUI;
        }
        else
        {
            speaker = Speaker.Officer;
            ActiveText.alignment = TextAnchor.UpperLeft;
            ActiveText.color = YellowUI;
        }

        string sentence = sentences.Dequeue();

        ActiveText.text = sentence;
    }

    public void EndDialogue()
    {
        DisableNextButton();
        LaunchButton.SetActive(true);
    }

    public void DisableNextButton()
    {
        GameObject StartButton = GameObject.Find("ButtonNextDialogue");
        GameObject SkipButton = GameObject.Find("ButtonSkipDialogue");

        if (StartButton != null)
        {
            StartButton.SetActive(false);
        }
        if (SkipButton != null)
        {
            SkipButton.SetActive(false);
        }
    }
}

    




/*
* On Scene load, Start Dialogue corresponding to level
* Have skip button that enables Launch button
* Have next button that cycles dialogue
* When dialogue complete, disable Next button and enable Launch
* */