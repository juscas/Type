using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class TextManager : MonoBehaviour {

	public string goalText;
	public string typedText; 
    public InputField inputField;
    [SerializeField]
	private Text currentWord, typedWord;
    [SerializeField]
    int points = 0;
    private string wordsStr = "pension,shrink,bland,solution,feminine,policy,registration,dash,notion,lung,tourist,admire,reporter,waist,represent,poetry,define,administration,analysis,take,investment,outlet";
    public AudioClip textComplete;
    private AudioSource source;

    private Stack<string> words;

    // Use this for initialization
    void Start () {
        // construct stack on words, pop off each time it is correct

        words = makeStack();

        //goalText = GetRandomWord (wordsStr);
        goalText = words.Peek();
		currentWord = GameObject.FindGameObjectWithTag("Text").GetComponent<Text> ();
		currentWord.text = goalText;

		typedWord = GameObject.FindGameObjectWithTag("Input").GetComponent<Text> ();
        GameObject iField = GameObject.Find("InputField");
        inputField = iField.GetComponent<InputField>();
        inputField.Select();

        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //if stack is empty -> game over
        if (words.Count == 0){
            Debug.Log("Stack is empty");
            //TODO: Game Over
            GameObject iFieldGO = GameObject.Find("InputField");
            InputField input = iFieldGO.GetComponent<InputField>();
            input.text = "you win";
        }

        //TODO: created equals method to ignore whitespace
		if (typedWord != null)
			typedText = inputField.text;

        // Word spelled correctly - SFX + Word reset to new word + Text animate out of screen
		if (goalText == typedText && words.Count > 0) {
			Debug.Log ("Yay!");
            source.PlayOneShot(textComplete, 1);
            points++;
            AnimateText();
            UpdateScore();
			ResetWord ();
            }

        // Resizing text 
        // TODO: figure out way to resize text when user backspaces...  
        if (typedWord.text.Length < inputField.text.Length)
        {
            typedWord.fontSize -= 10; //fix this shit to make it a ratio instead of -10 everytime   
        }
        
	}

	string GetRandomWord(string wordsString) {
        
        
        //System.IO.File.ReadAllText(wordListPath);

        string[] wordsArr = wordsStr.Split(',');

        List<string> wordList = new List<string>(wordsArr);
        foreach (string item in wordsArr)
        {
            //Debug.Log(item);
        }

        int randomNumberGenerator = Random.Range (0, wordsArr.Length);

		return wordList [randomNumberGenerator];
            
	}

    void UpdateScore()
    {
        GameObject score = GameObject.Find("Score");
        Text scoreText = score.GetComponent<Text>();
        scoreText.text = points.ToString();
    }

	void ResetWord() {

        // Pop correct word off the stack
        Debug.Log(words.Count);
        words.Pop();
		if (words.Count > 0){
            goalText = words.Peek();
            currentWord.text = goalText;

            GameObject iFieldGO = GameObject.Find("InputField");
            InputField input = iFieldGO.GetComponent<InputField>();
            input.text = "" ;
            typedWord.fontSize = 200;
        }
    }

    void AnimateText(){

    }

    Stack<string> makeStack(){

        string[] wordsArr = wordsStr.Split(',');
        Stack<string> wordStack = new Stack<string>();
        foreach (string word in wordsArr){
            wordStack.Push(word);
            Debug.Log(word);
        }

        return wordStack;
    }
}
