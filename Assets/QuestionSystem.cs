using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

namespace qs.QuestionSystem
{
    public class QuestionSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _questionText;

        [SerializeField] private QuestionList _questionLists = new QuestionList();
        [SerializeField] private QuestionList.ChooseList _chosenList;
        [SerializeField] private bool _excludeDuplicates;

        private List<string> selectedList;

        [SerializeField] private int _previousQIndex = -2;

        private void Start()
        {
            ChooseInDropDown(0);
        }

        public void CheckSelectedList()
        {
            switch (_chosenList)
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
            _chosenList = (QuestionList.ChooseList)num;

        public void TextNewQuestion(int randomNumber)
        {
            _previousQIndex = -2;

            if (_excludeDuplicates && IsQuestionListComplete())
            {
                _questionText.text = "Ви прочитали всі запитання!";
                return;
            }

            if (_questionLists.PreviousQuestion.Contains(selectedList[randomNumber]))
            {
                if (!_excludeDuplicates)
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
            CheckSelectedList();
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

        public void HaveToCheckDuplicates(bool checkedBool) =>
            _excludeDuplicates = checkedBool;

        private bool IsQuestionListComplete()
        {
            if (selectedList != null && _questionLists.PreviousQuestion != null)
            {
                return selectedList.Count == _questionLists.PreviousQuestion.Count &&
                       selectedList.All(question => _questionLists.PreviousQuestion.Contains(question));
            }
            return false;
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
}
