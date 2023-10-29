using qs.QuestionSystem;

namespace qs.Model
{
    [System.Serializable]
    public class PlayerData
    {
        public bool ExcludeDuplicates;
        public QuestionList.ChooseList ChosenList;
    }
}