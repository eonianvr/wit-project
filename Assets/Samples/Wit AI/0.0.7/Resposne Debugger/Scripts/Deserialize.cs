using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using com.facebook.witai.lib;
using TMPro;
using com.facebook.witai.samples.responsedebugger;
 using System.Linq;

    public class Deserialize : MonoBehaviour
    {
        List<Intent> intents = new List<Intent>();
        List <Intentresult> intentResults = new List<Intentresult>();
        List<Entity> entities= new List<Entity>();
         List<Trait> traits= new List<Trait>();

        WitUIInteractionHandler witHandler;

// variable to collect the deserialized data
 string intentResult ="unknown";
 
 // FIN variable to collect the deserialized data

        void Start()
        {
            witHandler = GetComponent<WitUIInteractionHandler>();
            witHandler.onResponseCallback += deserialize;
        }

        void deserialize(string responseText)
        {
            JSONNode serializedData = JSON.Parse(responseText);

            if(serializedData["intents"]!=null && serializedData["intents"].Count>0)
            {
                for(int i =0;i<serializedData["intents"].Count;i++)
                {
                long parsedId = long.Parse(serializedData["intents"][i]["id"]);
                string parsedName = serializedData["intents"][i]["name"];
                double parsedConfidence = double.Parse(serializedData["intents"][i]["confidence"]);

                Intent intent = new Intent{id=parsedId, intentName = parsedName,intentConfidence = parsedConfidence};
                intents.Add(intent);

                  //  test if the intent confidence is enough

                  Debug.Log("i equal "+i);

                  if (i>0 && intents.Count>1) {
                     if(parsedConfidence >= 1 && parsedConfidence > +intents[i-1].intentConfidence ){intentResult=intents[i].intentName;  }

                      /*  else
                        {
                           intentResult=intents[intents.Count-1].intentName; 
                        } */
                 }
                   else
                        {
                           intentResult=intents[intents.Count-1].intentName; 
                        } 
             Debug.Log("id: "+intents[intents.Count-1].id.ToString() +", name: "+intents[intents.Count-1].intentName+", confidence: "+intents[intents.Count-1].intentConfidence.ToString());          
       
        

            }
// float maxValue = Mathf.Max(parsedConfidence.ToArray());
// Intent highestAttackValueCard = intentConfidence.Max(x => x.intentConfidence);
 //   Debug.Log ("l'intent est une "+intentResult);
         Debug.Log ("total item dans la liste intent =" +intents.Count);
         Debug.Log ("l'intent avec la confidence la plus haute est "+intentResult);

            }

            if(serializedData["entities"]["subject:subject"]!=null && serializedData["entities"]["subject:subject"].Count>0)
            {
                for(int i =0;i<serializedData["entities"]["subject:subject"].Count;i++)
                {
                   
                string parsedName = serializedData["entities"]["subject:subject"][i]["name"];
                string parsedRole = serializedData["entities"]["subject:subject"][i]["role"];
                string parsedBody = serializedData["entities"]["subject:subject"][i]["body"];
                double parsedConfidence = double.Parse(serializedData["entities"]["subject:subject"][i]["confidence"]);
                string parsedValue = serializedData["entities"]["subject:subject"][i]["value"];

                Entity entity = new Entity{entityName=parsedName, role = parsedRole, entityBody=parsedBody, entityConfidence=parsedConfidence, entityValue=parsedValue};
                entities.Add(entity);
                }
                Debug.Log("name: "+entities[entities.Count-1].entityName+", role:"+entities[entities.Count-1].role+", body:"+entities[entities.Count-1].entityBody+", confidence:"+entities[entities.Count-1].entityConfidence.ToString()+", value:"+entities[entities.Count-1].entityValue);
            }
            if(serializedData["traits"]["wit$sentiment"]!=null && serializedData["traits"]["wit$sentiment"].Count>0)
            {
                for(int i =0;i<serializedData["traits"]["wit$sentiment"].Count;i++)
                {
             string parsedId = serializedData["traits"]["wit$sentiment"][i]["id"];
              string parsedValue = serializedData["traits"]["wit$sentiment"][i]["value"];
            double parsedConfidence = double.Parse(serializedData["traits"]["wit$sentiment"][i]["confidence"]);

               Trait trait = new Trait{ traitValue=parsedValue, traitConfidence=parsedConfidence} ;
            
               traits.Add(trait);
                }
                Debug.Log("Trait: wit$sentiment "+"value: "+traits[traits.Count-1].traitValue+",  confidence: "+traits[traits.Count-1].traitConfidence.ToString());
                }
            if(serializedData["traits"]["wit$greetings"]!=null && serializedData["traits"]["wit$greetings"].Count>0)
            {
                for(int i =0;i<serializedData["traits"]["wit$greetings"].Count;i++)
                {
             string parsedId = serializedData["traits"]["wit$greetings"][i]["id"];
              string parsedValue = serializedData["traits"]["wit$greetings"][i]["value"];
            double parsedConfidence = double.Parse(serializedData["traits"]["wit$greetings"][i]["confidence"]);

               Trait trait = new Trait{ traitValue=parsedValue, traitConfidence=parsedConfidence} ;
            
               traits.Add(trait);
                }
                Debug.Log("Trait: wit$greetings "+"value: "+traits[traits.Count-1].traitValue+",  confidence: "+traits[traits.Count-1].traitConfidence.ToString());
            }
        /* if (intentName="question" ){

        }*/


        }
  
    }
    public class Intent{
        public long id;
        public string intentName;
        public double intentConfidence;

    }
     public class Intentresult{
      
        public string intentResultName;
        public double intentResultConfidence;

    }

    public class Entity{
        public string entityName;
        public string role;
        public string entityBody;
        public double entityConfidence;
        public string entityValue;
    }
    public class Trait{
        public string type;
        public long id;
        public string traitValue;
        public double traitConfidence;
    }