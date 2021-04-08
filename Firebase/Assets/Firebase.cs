using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Firebase
{
    public class Firebase : MonoBehaviour
    {
        [SerializeField] private string rootUrl;    // address of your Firebase Realtime Database
        [SerializeField] private bool debug;        // if debug logs should be written

        public string RootUrl { get => rootUrl; set => rootUrl = value; }

        // default callbacks
        
        public Action<string> OnGetSuccess;
        public Action<string> OnPostSuccess;

        // mono behaviour
        
        private void Awake()
        {
            if (rootUrl == string.Empty)
                Debug.LogError("Firebase root URL not specified");
        }

        // get methods
        
        public void GetValue(string query) 
            => GetValue(query, OnGetSuccess);
        
        public void GetValue(string query, Action<string> callback)
        {
            string url = $"{rootUrl}{query}.json";
            StartCoroutine(GetRequestCoroutine(url, callback));
        }
        
        // set methods
        
        public void SetValue(string query, string jsonValue) 
            => SetValue(query, jsonValue, OnPostSuccess);
        
        public void SetValue(string query, string jsonValue, Action<string> callback)
        {
            string url = $"{rootUrl}{query}.json";
            StartCoroutine(SetRequestCoroutine(url, jsonValue, callback));
        }
        
        // coroutines
        
        private IEnumerator GetRequestCoroutine(string url, Action<string> callback)
        {
            using UnityWebRequest uwr = UnityWebRequest.Get(url);
            yield return uwr.SendWebRequest();
            bool success = uwr.result == UnityWebRequest.Result.Success;
            
            if(success)
                callback?.Invoke(uwr.downloadHandler.text);

            if (debug)
            {
                if (success)
                {
                    Debug.Log($"GET Request : SUCCESS");
                    Debug.Log($"GET Data : {uwr.downloadHandler.text}");
                }
                else
                {
                    Debug.Log($"GET Request : FAILED");
                    Debug.Log($"GET Error : {uwr.error}");
                }
            }
        }
        
        private IEnumerator SetRequestCoroutine(string url, string data, Action<string> callback)
        {
            using UnityWebRequest uwr = UnityWebRequest.Put(url, data);
            uwr.SetRequestHeader("Content-Type", "application/json");
            yield return uwr.SendWebRequest();
            bool success = uwr.result == UnityWebRequest.Result.Success;
            
            if(success)
                callback?.Invoke(uwr.downloadHandler.text);
            
            if (debug)
            {
                if (success)
                {
                    Debug.Log($"SET Request : SUCCESS");
                    Debug.Log($"SET Data : {uwr.downloadHandler.text}");
                }
                else
                {
                    Debug.Log($"SET Request : FAILED");
                    Debug.Log($"SET Error : {uwr.error}");
                }
            }
        }
    }
}