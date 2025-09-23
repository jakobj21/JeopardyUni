using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JeopardyManager : MonoBehaviour{
    [System.Serializable]
    public class Question{
        public string questionText;
        public int pointValue;
        public int categoryIndex;
        public string answerText;
    }
    
    public class Team{
        public string teamName;
        public int score;
    }
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

    public GameObject questionPanel;
    public GameObject buzzPanel;
    public GameObject answerPanel;
    public GameObject gameBoard;
    public GameObject titlePanel;
    public GameObject teamPanel;
    public GameObject scorePanel;

    public TMP_Text TeamName;
    public TMP_Text answerText;
    public TMP_Text questionText;
    public TMP_Text[] teamNameTexts;
    public TMP_Text[] teamScoreTexts;

    public Button skipQuestionButton;
    public Button skipAnswerButton;
    public Button RightButton;
    public Button WrongButton;

    public TMP_InputField[] teamInputs;
    public int numberOfTeams;

    private bool titleActive = true;
    private bool teamActive = false;
    private bool gameBoardActive = false;
    private bool questionActive = false;
    private bool buzzActive = false;
    private bool answerActive = false;

    public List<int> askedQuestions = new List<int>();

    void Start(){
        titlePanel.SetActive(true);
        scorePanel.SetActive(false);
        teamPanel.SetActive(false);
        gameBoard.SetActive(false);
        questionPanel.SetActive(false);
        answerPanel.SetActive(false);
        buzzPanel.SetActive(false);
        skipQuestionButton.onClick.AddListener(SkipQuestion);
        skipAnswerButton.onClick.AddListener(SkipAnswer);
        for(int i = 0; i<25; i++){
            questions[i].questionText = questionArray[i];
            questions[i].answerText = answerArray[i];
        }
    }

    public void ShowQuestion(int questionIndex){
        Debug.Log("ShowQuestion called with index: " + questionIndex);
        currentQuestion = questions[questionIndex];
        askedQuestions.Add(questionIndex);
        gameBoard.SetActive(false);
        scorePanel.SetActive(false);
        questionPanel.SetActive(true);
        questionText.text = currentQuestion.questionText;
        questionActive = true;
    }

    public void PlayerBuzzed(int playerNumber){
        if (!questionActive) return;
        Debug.Log("Player" + playerNumber + "buzzed");
        questionActive = false;
        buzzActive = true;
        questionPanel.SetActive(false);
        buzzPanel.SetActive(true);
    }

    void SkipQuestion(){
        questionActive = false;
        answerActive = true;
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
    }

    void SkipAnswer(){
        UpdateBoard();
        answerActive = false;
        gameBoardActive = true;
        answerPanel.SetActive(false);
        gameBoard.SetActive(true);
        scorePanel.SetActive(true);
    }

    void UpdateBoard(){
        for(int i = 0; i < askedQuestions.Count; i++){
            questionButton[askedQuestions[i]].onClick.RemoveAllListeners();
            questionButton[askedQuestions[i]].GetComponentInChildren<TMP_Text>().text = "";
        }
    }

    public void AddScore(int teamIndex, int points){
        teams[teamIndex].score += points;
        UpdateTeamUI(teamIndex);
    }
    
    void UpdateTeamUI(int teamIndex){
        teamScoreTexts[teamIndex].text = teams[teamIndex].score.ToString();
    }

    public void StartGame(){
        teamActive = false;
        gameBoardActive = true;
        scorePanel.SetActive(true);
        teamPanel.SetActive(false);
        gameBoard.SetActive(true);
    }

    public void ChooseTeams(){
        titleActive = false;
        teamActive = true;
        titlePanel.SetActive(false);
        teamPanel.SetActive(true);
    }

    public void UpdateTeamInputs(int teamCount){
        numberOfTeams = teamCount;
        teams = new Team[teamCount];    
        for (int i = 0; i < teamCount; i++) {
            if (teams[i] == null) {
                teams[i] = new Team();
            }

            teams[i].teamName = "Team " + (i + 1);
            teams[i].score = 0;

            teamInputs[i].gameObject.SetActive(true);
            teamNameTexts[i].gameObject.SetActive(true);
            teamScoreTexts[i].gameObject.SetActive(true);
        }

        for (int i = teamCount; i < teamInputs.Length; i++) {
            teamInputs[i].gameObject.SetActive(false);
            teamNameTexts[i].gameObject.SetActive(false);
            teamScoreTexts[i].gameObject.SetActive(false);
        }

    }

    public void OnDropDownChange(int index){

        Debug.Log("Index: " + index);
        int teamCount = index + 2;
        UpdateTeamInputs(teamCount);
    }

    public void ChangeTeam1Name(string name){
        teams[0].teamName = name;
        teamNameTexts[0].text = name;
    }

    public void ChangeTeam2Name(string name){
        teams[1].teamName = name;
        teamNameTexts[1].text = name;
    }

    public void ChangeTeam3Name(string name){
        teams[2].teamName = name;
        teamNameTexts[2].text = name;
    }

    public void ChangeTeam4Name(string name){
        teams[3].teamName = name;
        teamNameTexts[3].text = name;
    }

    public void UpdateTeamScore(int index, int points){
        teams[index].score = teams[index].score + points; 
    }
    
}