using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public class MenuManager : MonoBehaviour
    {

        //public static Transform Canvas = GameObject.Find("Canvas").transform;

        public GameObject MainMenu = null;

        public GameObject PauseMenu = null;

        public GameObject InstructionMenu = null;

        public bool started = false;

        public bool MenuUp;

        public Button Start_button;

        public Button Instructions;

        public Button Quit;

        public Button Back;


    }
}
