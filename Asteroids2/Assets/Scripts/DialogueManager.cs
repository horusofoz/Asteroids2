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

    private Dialogue dialogue;

    private List<List<string>> dialogueList = new List<List<string>>();

    // Use this for initialization
    void Awake()
    {
        CreateDialogueLists();
        sentences = new Queue<string>();
        StartDialogue(dialogue);
        LaunchButton = GameObject.Find("ButtonLaunchNextMission");
        LaunchButton.SetActive(false);
    }

    private void CreateDialogueLists()
    {
        // Level 1 Brief
        dialogueList.Add(new List<string>()
        {
            "Welcome Captain Yeager.\n\nMy name is Lieutenant Anne.",
            "Lieutenant. What's the mission?",
            "Straight to the point. Ok.\n\nSeveral explorers sent to the Andromeda galaxy have gone missing in action.\n\nWe lose contact when they enter the asteroid fields of sector 42, quadrant B.\n\nWe need someone of your skill to map a path and see if you can find any survivors.",
            "Understood. Ready when you are, Lieutenant."
        });

        // Level 2 Brief
        dialogueList.Add(new List<string>()
        {
            "Captain, you are now approaching the main body of the asteroid field.",
            "That doesn't look good.",
            "Buckle up Captain. it's going to be a bumpy ride."
        });

        // Level 3 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 3 dialogue to be implemented"
        });

        // Level 4 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 4 dialogue to be implemented"
        });

        // Level 5 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 5 dialogue to be implemented"
        });

        // Level 6 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 6 dialogue to be implemented"
        });

        // Level 7 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 7 dialogue to be implemented"
        });

        // Level 8 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 8 dialogue to be implemented"
        });

        // Level 9 Brief
        dialogueList.Add(new List<string>()
        {
            "Level 9 dialogue to be implemented"
        });
    }

        public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in dialogueList[GameController.instance.currentLevel - 1])
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