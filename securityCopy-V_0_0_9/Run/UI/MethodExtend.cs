using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace readDataFromUI
{


    public class MethodExtend : MonoBehaviour
    {


        static int propiedadCodigo = 100;
        
        public static int readFromButton = 1;
        
        public static string fileName_words0 = "";

        public static string fileName_words1 = "";

        public static int keyboardLanguage = 0;
        

        public static void PlayGame(int number)
        {

            readFromButton = 1;

        }

        public static void MenuGame(int number)
        {

            readFromButton = number;

        }

        public static void SelectLanguageItalian(int number)
        {

            keyboardLanguage = 0;
        
        }

        public static void SelectLanguagePortuguese(int number)
        {

            keyboardLanguage = 1;
        
        }

        public static void Language_Italian_ITALIAN1(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN1/1000EnglishMOST_1";
            fileName_words1 = "ITALIAN/ITALIAN1/1000ItalianMOST_1";

        }


        public static void Language_Italian_ITALIAN2(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN2/1000EnglishMOST_2";
            fileName_words1 = "ITALIAN/ITALIAN2/1000ItalianMOST_2";

        }

        public static void Language_Italian_ITALIAN3(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN3/1000EnglishMOST_3";
            fileName_words1 = "ITALIAN/ITALIAN3/1000ItalianMOST_3";

        }

        public static void Language_Italian_ITALIAN4(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN4/1000EnglishMOST_4";
            fileName_words1 = "ITALIAN/ITALIAN4/1000ItalianMOST_4";

        }

        public static void Language_Italian_ITALIAN5(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN5/1000EnglishMOST_5";
            fileName_words1 = "ITALIAN/ITALIAN5/1000ItalianMOST_5";

        }

        public static void Language_Italian_ITALIAN6(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN6/1000EnglishMOST_6";
            fileName_words1 = "ITALIAN/ITALIAN6/1000ItalianMOST_6";

        }

        public static void Language_Italian_ITALIAN7(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN7/1000EnglishMOST_7";
            fileName_words1 = "ITALIAN/ITALIAN7/1000ItalianMOST_7";

        }

        public static void Language_Italian_ITALIAN8(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN8/1000EnglishMOST_8";
            fileName_words1 = "ITALIAN/ITALIAN8/1000ItalianMOST_8";

        }

        public static void Language_Italian_ITALIAN9(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN9/1000EnglishMOST_9";
            fileName_words1 = "ITALIAN/ITALIAN9/1000ItalianMOST_9";

        }

        public static void Language_Italian_ITALIAN10(string English_Italian)
        {

            fileName_words0 = "ITALIAN/ITALIAN10/1000EnglishMOST_10";
            fileName_words1 = "ITALIAN/ITALIAN10/1000ItalianMOST_10";

        }
    }
}

