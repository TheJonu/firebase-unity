# Firebase-Unity
 
This is a simple and straightforward script for Unity that provides basic communication with your Google Firebase Realtime Database.

It handles `GetValue` (GET) and `SetValue` (PUT) requests.

Tested on Unity 2020.2.4f1 (WebGL build).

Firebase is a MonoBehaviour, it needs to be instantiated in a scene and provided with the URL of your database.

### Usage
```cs
[SerializeField] private Firebase firebase;

// default "Get" success callback
firebase.OnGetSuccess += result => Debug.Log(result);

// "Get" request with default success callback
firebase.GetValue("players/player1");

// "Get" request with specified success callback
firebase.GetValue("players/player1/score", result => Debug.Log($"get score: {result}"));

// default "Set" success callback
firebase.OnSetSuccess += result => Debug.Log(result);

// "Set" request with default success callback
firebase.SetValue("players/player1", "{\"score\":100, \"name\":\"carter\"}");

// "Set" request with specified success callback
firebase.SetValue("players/player1/score", "200", result => Debug.Log($"set score: {result}"));
```