using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;
using System.Collections.Generic;

//123

public class JeopardyManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public int pointValue;
        public int categoryIndex;
        public string answerText;
    }

    public class Team
    {
        public string teamName;
        public int score;
    }

    //implement file reader for reading questions and answers

    string[] questionArray = new string[]
    {
    "Placeholder Question 1",
    "Placeholder Question 2",
    "Placeholder Question 3",
    "Placeholder Question 4",
    "Placeholder Question 5",
    "Placeholder Question 6",
    "Placeholder Question 7",
    "Placeholder Question 8",
    "Placeholder Question 9",
    "Placeholder Question 10",
    "Placeholder Question 11",
    "Placeholder Question 12",
    "Placeholder Question 13",
    "Placeholder Question 14",
    "Placeholder Question 15",
    "Placeholder Question 16",
    "Placeholder Question 17",
    "Placeholder Question 18",
    "Placeholder Question 19",
    "Placeholder Question 20",
    "Placeholder Question 21",
    "Placeholder Question 22",
    "Placeholder Question 23",
    "Placeholder Question 24",
    "Placeholder Question 25"
    };
    string[] answerArray = new string[]
    {
    "Placeholder Answer 1",
    "Placeholder Answer 2",
    "Placeholder Answer 3",
    "Placeholder Answer 4",
    "Placeholder Answer 5",
    "Placeholder Answer 6",
    "Placeholder Answer 7",
    "Placeholder Answer 8",
    "Placeholder Answer 9",
    "Placeholder Answer 10",
    "Placeholder Answer 11",
    "Placeholder Answer 12",
    "Placeholder Answer 13",
    "Placeholder Answer 14",
    "Placeholder Answer 15",
    "Placeholder Answer 16",
    "Placeholder Answer 17",
    "Placeholder Answer 18",
    "Placeholder Answer 19",
    "Placeholder Answer 20",
    "Placeholder Answer 21",
    "Placeholder Answer 22",
    "Placeholder Answer 23",
    "Placeholder Answer 24",
    "Placeholder Answer 25"
    };


    public Button[] questionButton;
    public Question[] questions;
    public Question currentQuestion;
    public Team[] teams;
    public GridLayoutGroup gridLayout;
    public Vector2 referenceResolution = new Vector2(1920, 1080);
    public Vector2 baseCellSize = new Vector2(218, 65);

    public GameObject questionPanel;
    public GameObject buzzPanel;
    public GameObject answerPanel;
    public GameObject titlePanel;
    public GameObject teamPanel;
    public GameObject categoryPanel;

    public TMP_Text TeamName;
    public TMP_Text answerText;
    public TMP_Text questionText;
    public TMP_Text[] teamNameTexts;
    public TMP_Text[] teamScoreTexts;
    public TMP_Text[] buzzTeamTexts;

    public Button skipQuestionButton;
    public Button skipAnswerButton;
    public Button RightButton;
    public Button WrongButton;

    public TMP_InputField[] teamInputs;
    public int numberOfTeams;
    public int teamBuzzed = 0;
    private bool titleActive = true;
    private bool teamActive = false;
    private bool gameBoardActive = false;
    private bool questionActive = false;
    private bool buzzActive = false;
    private bool answerActive = false;

    public List<int> askedQuestions = new List<int>();
    public List<int> alreadyBuzzed = new List<int>();

    void Start()
    {
        //ResizeGridCells();
        ResetBuzzNames();
        titlePanel.SetActive(true);
        categoryPanel.SetActive(false);
        teamPanel.SetActive(false);
        questionPanel.SetActive(false);
        answerPanel.SetActive(false);
        buzzPanel.SetActive(false);
        skipQuestionButton.onClick.AddListener(SkipQuestion);
        skipAnswerButton.onClick.AddListener(SkipAnswer);
        for (int i = 0; i < 25; i++)
        {
            questions[i].questionText = questionArray[i];
            questions[i].answerText = answerArray[i];
        }
    }

    public void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called with index: " + questionIndex);
        currentQuestion = questions[questionIndex];
        askedQuestions.Add(questionIndex);
        categoryPanel.SetActive(false);
        questionPanel.SetActive(true);
        questionText.text = currentQuestion.questionText;
        questionActive = true;
    }

    void SkipQuestion()
    {
        questionActive = false;
        answerActive = true;
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
    }

    void SkipAnswer()
    {
        UpdateBoard();
        alreadyBuzzed = new List<int>();
        answerActive = false;
        gameBoardActive = true;
        answerPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }

    void UpdateBoard()
    {
        for (int i = 0; i < askedQuestions.Count; i++)
        {
            questionButton[askedQuestions[i]].onClick.RemoveAllListeners();
            questionButton[askedQuestions[i]].GetComponentInChildren<TMP_Text>().text = "";
        }
    }

    public void StartGame()
    {
        teamActive = false;
        gameBoardActive = true;
        teamPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }

    public void ChooseTeams()
    {
        titleActive = false;
        teamActive = true;
        titlePanel.SetActive(false);
        teamPanel.SetActive(true);
        UpdateTeamInputs(4);
    }

    public void UpdateTeamInputs(int teamCount)
    {
        numberOfTeams = teamCount;
        teams = new Team[4];
        for (int i = 0; i < teamCount; i++)
        {
            if (teams[i] == null)
            {
                teams[i] = new Team();
            }

            teams[i].teamName = "Team " + (i + 1);
            teams[i].score = 0;
            teamScoreTexts[i].text = "score: 0";

            teamInputs[i].gameObject.SetActive(true);
            teamNameTexts[i].gameObject.SetActive(true);
            teamScoreTexts[i].gameObject.SetActive(true);
        }

        for (int i = teamCount; i < teamInputs.Length; i++)
        {   
            if (teams[i] == null)
            {
                teams[i] = new Team();
            }
            teams[i].teamName = "";
            teamScoreTexts[i].text = "";
            teamNameTexts[i].text = "";
            teamInputs[i].gameObject.SetActive(false);
            teamNameTexts[i].gameObject.SetActive(true);
            teamScoreTexts[i].gameObject.SetActive(true);

        }

    }

    public void OnDropDownChange(int index)
    {

        Debug.Log("Index: " + index);
        int teamCount = index + 2;
        UpdateTeamInputs(teamCount);
    }

    public void ChangeTeam1Name(string name)
    {
        teams[0].teamName = name;
        teamNameTexts[0].text = name;
        buzzTeamTexts[0].text = name;
    }

    public void ChangeTeam2Name(string name)
    {
        teams[1].teamName = name;
        teamNameTexts[1].text = name;
        buzzTeamTexts[1].text = name;
    }

    public void ChangeTeam3Name(string name)
    {
        teams[2].teamName = name;
        teamNameTexts[2].text = name;
        buzzTeamTexts[2].text = name;
    }

    public void ChangeTeam4Name(string name)
    {
        teams[3].teamName = name;
        teamNameTexts[3].text = name;
        buzzTeamTexts[3].text = name;
    }

    public void UpdateTeamScore(int index, int points)
    {
        teams[index].score = teams[index].score + points;
        teamScoreTexts[index].text = "Score: " + teams[index].score.ToString();
    }

    public void TeamBuzzed(int team)
    {   
        if (alreadyBuzzed.Contains(team)) return;
        alreadyBuzzed.Add(team);
        questionActive = false;
        buzzActive = true;
        questionPanel.SetActive(false);
        buzzPanel.SetActive(true);
        buzzTeamTexts[team].gameObject.SetActive(true);
        teamBuzzed = team;
    }

    void ResetBuzzNames()
    {
        for (int i = 0; i < 4; i++)
        {
            buzzTeamTexts[i].gameObject.SetActive(false);
        }
    }

    public void QuestionRight()
    {
        teams[teamBuzzed].score = teams[teamBuzzed].score + currentQuestion.pointValue;
        teamScoreTexts[teamBuzzed].text = "Score " + teams[teamBuzzed].score.ToString();
        buzzActive = false;
        answerActive = true;
        buzzPanel.SetActive(false);
        answerPanel.SetActive(true);
        ResetBuzzNames();
    }

    public void QuestionWrong()
    {
        teams[teamBuzzed].score = teams[teamBuzzed].score - currentQuestion.pointValue;
        teamScoreTexts[teamBuzzed].text = "Score " + teams[teamBuzzed].score.ToString();
        buzzActive = false;
        questionActive = true;
        buzzPanel.SetActive(false);
        questionPanel.SetActive(true);
        ResetBuzzNames();
    }

    void Update()
    {
        if (!questionActive) return;
            //player 1 buzzer 
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                if (teamNameTexts[0].text != "")
                {
                    TeamBuzzed(0);
                }
            }
            //player 2 buzzer 
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                if (teamNameTexts[1].text != "")
                {
                    TeamBuzzed(1);
                }    
            }
            //player 3 buzzer 
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                if (teamNameTexts[2].text != "")
                {
                    TeamBuzzed(2);
                }    
            }
            //player 4 buzzer
            if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                if (teamNameTexts[3].text != "")
                {
                    TeamBuzzed(3);
                }    
            }

            //controller 1 input = 1
            //controller 2 input = 6
            //controller 3 input = 11
            //controller 4 input = 16
            /* check mapping
            foreach (var joystick in Joystick.all)
            {
                for (int i = 0; i < joystick.allControls.Count; i++)
                {
                    var control = joystick.allControls[i];
                    if (control is ButtonControl button && button.wasPressedThisFrame)
                    {
                        Debug.Log($"Pressed: {control.displayName} (index {i}) on {joystick.displayName}");
                    }
                }
            }
            */
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) TeamBuzzed(0); 
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) TeamBuzzed(1); 
        if (Input.GetKeyDown(KeyCode.Joystick1Button10)) TeamBuzzed(2); 
        if (Input.GetKeyDown(KeyCode.Joystick1Button15)) TeamBuzzed(3);
    }
    

    void ResizeGridCells()
    {
        float widthRatio = (float)Screen.width / referenceResolution.x;
        float heightRatio = (float)Screen.height / referenceResolution.y;
        float scale = Mathf.Min(widthRatio, heightRatio);

        gridLayout.cellSize = new Vector2(baseCellSize.x * scale, baseCellSize.y * scale);
    }

}