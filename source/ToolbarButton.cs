using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;  //Get Regex
using KSP.UI.Screens;

using System.Runtime.InteropServices;

using static Targeting;

namespace ExceptionDetector
{

    [KSPAddon(KSPAddon.Startup.AllGameScenes, true)]
    public class ToolbarButton : MonoBehaviour
    {
        private static GameObject go;
        private int windowId;
        private bool showPopup;
        private Rect popupPos;
        private Func<int, object, bool> callback;
        private object parameter;

        void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(OnAppLauncherReady);
            DontDestroyOnLoad(this);

        }

        private static readonly string TestsPassingIconLocation = "ExceptionDetector/Icons/ed";

        private ApplicationLauncherButton button;
        private Texture TestsPassingIcon;

        private void OnAppLauncherReady()
        {
            TestsPassingIcon = GameDatabase.Instance.GetTexture(TestsPassingIconLocation, false);

            if (button != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(button);
                button = null;
            }
            button = ApplicationLauncher.Instance.AddModApplication(
                OnTrue, OnFalse,
                null,
                null,
                null,
                null,
                ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.SPACECENTER |
                ApplicationLauncher.AppScenes.MAPVIEW | ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.TRACKSTATION,
                TestsPassingIcon);

        }



        internal const string MODID = "ED_NS";
        internal const string MODNAME = "ExceptionDetector";
        static IssueGUI instance = null;

        void OnTrue()
        {
            if (go == null)
            {
                go = new GameObject("Any");
            }
            if (!IssueGUI.isActive) 
                instance = go.AddComponent<IssueGUI>();
        }

        void OnFalse()
        {
            if (instance != null)
            {
                Destroy(instance);
                instance = null;
                IssueGUI.isActive = false;
            }
        }

    }
}
