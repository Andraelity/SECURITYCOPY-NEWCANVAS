using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace readDataFile{


    public class ScriptCommunication: MonoBehaviour
    {

        
        public static int SetOnData = 0;
        
        public static int sizeOfWord0 = 0;

        public static bool activatePrefabRedStar = false;

        public static int sizeTextField = 0;

        public static bool enableKeyboard = true;

    
        public static int positionWordToHandle = 99;

        public static bool activeHandle = false;


        public static List<int> sizeOfWords_OneLanguage = new List<int>();

        public static List<string> listWords_OneLanguage = new List<string>();

        public static List<List<int>> Position_ListOfListInt = new List<List<int>>();


        public static bool operationCheck = false;

        public static bool operationCounter = false;


    }


}