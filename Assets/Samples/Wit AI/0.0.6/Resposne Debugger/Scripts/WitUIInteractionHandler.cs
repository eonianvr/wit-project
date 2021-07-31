/*
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System.Net;
using com.facebook.witai.lib;
using TMPro;
using UnityEngine;

namespace com.facebook.witai.samples.responsedebugger
{


    public class WitUIInteractionHandler : MonoBehaviour


    {
        [Header("Wit")]
        [SerializeField] private Wit wit;
        [Header("UI")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI textArea;
        [SerializeField] private TextMeshProUGUI textAreaSerialized;
        [Header("Configuration")]
        [SerializeField] private bool showJson;

        private string pendingText;
         [SerializeField] private TextMeshProUGUI id_field;

       public class ClientData {
            public int id;
            public string name;
            public int confidenceName;
            public string body;
            public float confidenceBody;
            public string value;
        

    }

        private void OnValidate()
        {
            if (!wit) wit = FindObjectOfType<Wit>();
        }

        private void Update()
        {
            if (null != pendingText)
            {
                textArea.text = pendingText; 
                ClientData clientData = new ClientData();
            /*    clientData.id= 0;
                clientData.name="none";
                clientData.confidenceName=0;
                clientData.body="none";
                clientData.confidenceBody=0;
                clientData.value="none"; */
JSONObject j = new JSONObject(pendingText);

               accessData(j);
             /* ClientData LoadClientData = JsonUtility.FromJson<ClientData>(pendingText);
                Debug.Log("id: "+LoadClientData.id);
                 Debug.Log("name: "+LoadClientData.name);
                    Debug.Log("body: "+LoadClientData.body);
                Debug.Log("done!");*/
           //    private vomit;
             //  vomit = textAreaSerialized.text;
        //   vomit.ToString(LoadClientData);
                pendingText = null;
            }
        }
void accessData(JSONObject obj){
    switch(obj.type){
        case JSONObject.Type.OBJECT:
            for(int i = 0; i < obj.list.Count; i++){
                string key = (string)obj.keys[i];
                JSONObject j = (JSONObject)obj.list[i];
                Debug.Log(key);
                accessData(j);
            }
            break;
        case JSONObject.Type.ARRAY:
            foreach(JSONObject j in obj.list){
                accessData(j);
            }
            break;
        case JSONObject.Type.STRING:
            Debug.Log(obj.str);
            break;
        case JSONObject.Type.NUMBER:
            Debug.Log(obj.n);
            break;
        case JSONObject.Type.BOOL:
            Debug.Log(obj.b);
            break;
        case JSONObject.Type.NULL:
            Debug.Log("NULL");
            break;
        
    }
}
        private void OnEnable()
        {
            wit.events.OnRequestCreated.AddListener(OnRequestStarted);
        }

        private void OnDisable()
        {
            wit.events.OnRequestCreated.RemoveListener(OnRequestStarted);
        }

        private void OnRequestStarted(WitRequest request)
        {
            // The raw response comes back on a different thread. We store the
            // message received for display on the next frame.
            if (showJson) request.onRawResponse += (response) => pendingText = response;
            request.onResponse += (r) =>
            {
                if (r.StatusCode == (int) HttpStatusCode.OK)
                {
                    OnResponse(r.ResponseData);
                }
                else
                {
                    OnError($"Error {r.StatusCode}", r.StatusDescription);
                }
            };
        }

        public void OnResponse(WitResponseNode response)
        {
            if (!showJson) textArea.text = response["text"];
        }

        public void OnError(string error, string message)
        {
            textArea.text = $"Error: {error}\n\n{message}";
        }

        public void ToggleActivation()
        {
            if (wit.Active) wit.Deactivate();
            else
            {
                textArea.text = "The mic is active, start speaking now.";
                wit.Activate();
            }
        }

        public void Send()
        {
            textArea.text = $"Sending \"{inputField.text}\" to Wit.ai for processing...";
            wit.Activate(inputField.text);
        }

        public void LogResults(string[] parameters)
        {
            Debug.Log("Got the following entities back: " + string.Join(", ", parameters));
        }
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Intent
    {
        public string id { get; set; }
        public string name { get; set; }
        public int confidence { get; set; }
    }

    public class SubjectSubject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string body { get; set; }
        public double confidence { get; set; }
        public List<object> entities { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Entities
    {
        [JsonProperty("subject:subject")]
        public List<SubjectSubject> SubjectSubject { get; set; }
    }

    public class Traits
    {
    }

    public class Root
    {
        public string text { get; set; }
        public List<Intent> intents { get; set; }
        public Entities entities { get; set; }
        public Traits traits { get; set; }
    }


        
    }

 

}
