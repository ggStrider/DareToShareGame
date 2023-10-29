using UnityEngine;
using qs.QuestionSystem;

namespace qs.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        public PlayerData Data => _data;

        private void Awake()
        {
            if(IsSessionExist())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach(var gameSession in sessions)
            {
                if(gameSession != this)
                    return true;
            }
            return false;
        }

        public void CheckExcludeDublicates(bool checkedBool)
        {
            _data.ExcludeDuplicates = checkedBool;
        }
        public void ChooseInDropDown(int num) =>
            _data.ChosenList = (QuestionList.ChooseList)num;
    }
}