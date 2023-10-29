using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using qs.Model;

namespace qs.QuestionSystem
{
    public class QuestionSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private QuestionList _questionLists = new QuestionList();

        private List<string> selectedList;
        private GameSession _gameSession;
        private int _previousQIndex = -2;

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();

            ChooseInDropDown((int)_gameSession.Data.ChosenList);
            CheckSelectedList();
        }

        public void CheckSelectedList()
        {
            switch (_gameSession.Data.ChosenList)
            {
                case QuestionList.ChooseList.CommonQuestions:
                    selectedList = _questionLists.commonQuestion;
                    break;
                case QuestionList.ChooseList.intimateIssuesQuestions:
                    selectedList = _questionLists.intimateIssues;
                    break;
            }
        }

        public void ChooseInDropDown(int num) =>
            _gameSession.Data.ChosenList = (QuestionList.ChooseList)num;

        public void TextNewQuestion(int randomNumber)
        {
            _previousQIndex = -2;

            if (_gameSession.Data.ExcludeDuplicates && IsQuestionListComplete())
            {
                _questionText.text = "Ви прочитали всі запитання!";
                return;
            }

            if (_questionLists.PreviousQuestion.Contains(selectedList[randomNumber]))
            {
                if (!_gameSession.Data.ExcludeDuplicates)
                {
                    _questionLists.PreviousQuestion.Add(selectedList[randomNumber]);
                    _questionText.text = selectedList[randomNumber];
                }
                else
                {
                    GenerateNewQuestion();
                }
            }
            else
            {
                _questionLists.PreviousQuestion.Add(selectedList[randomNumber]);
                _questionText.text = selectedList[randomNumber];
            }
        }

        public void GenerateNewQuestion()
        {
            var questionCount = selectedList.Count;
            var chooseRandomQuestion = Random.Range(0, questionCount);

            TextNewQuestion(chooseRandomQuestion);
        }

        public void ShowPreviousQuestion()
        {
            var prevQIndex = _previousQIndex;
            var index = _questionLists.PreviousQuestion.Count + _previousQIndex;

            _previousQIndex--;
            if (index < 0)
            {
                _previousQIndex = prevQIndex;
                return;
            }

            _questionText.text = _questionLists.PreviousQuestion[index];
        }

        public void ShowNextToPreviousQuestion()
        {
            var prevQIndex = _previousQIndex;
            var index = _questionLists.PreviousQuestion.Count + _previousQIndex + 2;

            if (index >= _questionLists.PreviousQuestion.Count)
            {
                _previousQIndex = prevQIndex;
                return;
            }

            _previousQIndex++;
            _questionText.text = _questionLists.PreviousQuestion[index];
        }

        private bool IsQuestionListComplete()
        {
            if (selectedList != null && _questionLists.PreviousQuestion != null)
            {
                return selectedList.Count == _questionLists.PreviousQuestion.Count &&
                       selectedList.All(question => _questionLists.PreviousQuestion.Contains(question));
            }
            return false;
        }
    }

    [System.Serializable]
    public class QuestionList
    {
        public enum ChooseList
        {
            CommonQuestions, intimateIssuesQuestions
        };

        public List<string> commonQuestion = new List<string>();
        public List<string> intimateIssues = new List<string>();

        public List<string> PreviousQuestion = new List<string>();
    }
}
