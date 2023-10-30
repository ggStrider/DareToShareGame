using qs.Model;
using UnityEngine;
using UnityEngine.UI;

namespace qs.Settings
{
    public class SendInfoToGameSession : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMPro.TMP_Dropdown _dropdown;

        private GameSession _gameSession;

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();

            _toggle.isOn = _gameSession.Data.ExcludeDuplicates;
            _dropdown.value = (int)_gameSession.Data.ChosenList;
        }

        public void SendExcludeBool(bool checkedBool) =>
            _gameSession.CheckExcludeDublicates(checkedBool);

        public void SendChoseInDropDown(int num) =>
            _gameSession.ChooseInDropDown(num);
    }
}