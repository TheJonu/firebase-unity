using System;
using UnityEngine;
using UnityEngine.UI;

namespace Firebase
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Firebase firebase;
        [SerializeField] private InputField requestInputField;
        [SerializeField] private InputField valueInputField;
        [SerializeField] private Button getButton;
        [SerializeField] private Button postButton;
        [SerializeField] private Text resultText;

        private void Awake()
        {
            getButton.onClick.AddListener(GetButtonClicked);
            postButton.onClick.AddListener(PostButtonClicked);
        }

        private void GetButtonClicked()
        {
            string request = requestInputField.text;
            firebase.GetValue(request, result => resultText.text = result);
        }

        private void PostButtonClicked()
        {
            string request = requestInputField.text;
            string value = valueInputField.text;
            firebase.SetValue(request, value, result => resultText.text = result);
        }
    }
}