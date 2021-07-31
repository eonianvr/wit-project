using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DataHandler : MonoBehaviour
{

          public string clientData;
         [SerializeField] private TextMeshProUGUI id_field;
         [SerializeField] private TextMeshProUGUI name_field;
         [SerializeField] private TextMeshProUGUI onfidencename_field;
         [SerializeField] private TextMeshProUGUI body_field;
         [SerializeField] private TextMeshProUGUI confidencebodyfield;
         [SerializeField] private TextMeshProUGUI value_field;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private class ProcessJson{
      
           ClientData clientData = new ClientData(); 
   // string json=JsonUtility.ToJson(clientData);
   // Debug.Log(Json);
    }

    public class ClientData {
            public int id;
            public string name;
            public int confidenceName;
            public string body;
            public float confidenceBody;
            public string value;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
