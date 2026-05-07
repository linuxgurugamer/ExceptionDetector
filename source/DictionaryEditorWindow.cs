using ExceptionDetectorEnhanced.Support;
using KSP.Localization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExceptionDetectorEnhanced
{
    public class DictionaryEditorWindow : MonoBehaviour
    {
        private Rect windowRect = new Rect(200, 100, 700, 500);
        private Vector2 scrollPos;
        static bool active = false;

        void Start()
        {
            active = true;
        }

        public void Destroy()
        {
            if (active)
            {
                active = false;
                Destroy(this);
            }
        }

        private enum ListType
        {
            Whitelist,
            Alwayslist
        }

        private ListType currentList = ListType.Whitelist;

        private string newKey = "";
        private string newValue = "";

        private void OnGUI()
        {
            if (!ExceptionDetectorEnhanced.UseAltSkin)
                GUI.skin = HighLogic.Skin;
            windowRect = CBTWrapper.GUILayoutWindow(
                GetInstanceID() + 1,
                windowRect,
                DrawWindow,
                Localizer.Format("#EXCD-16")
            );
        }

        private Dictionary<string, string> CurrentDictionary
        {
            get
            {
                return currentList == ListType.Whitelist
                    ? ExceptionDetectorEnhanced.WhitelistValues
                    : ExceptionDetectorEnhanced.AlwayslistValues;
            }
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Toggle(currentList == ListType.Whitelist, "Whitelist", GUI.skin.button))
                currentList = ListType.Whitelist;

            if (GUILayout.Toggle(currentList == ListType.Alwayslist, "Alwayslist", GUI.skin.button))
                currentList = ListType.Alwayslist;

            GUILayout.EndHorizontal();

            GUILayout.Space(8);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                switch (currentList)
                {
                    case ListType.Whitelist:
                        GUILayout.Label(Localizer.Format("#EXCD-17"));
                        break;
                    case ListType.Alwayslist:
                        GUILayout.Label(Localizer.Format("#EXCD-18"));
                        break;
                }
            }
            DrawDictionaryEditor(CurrentDictionary);

            GUILayout.Space(8);

            DrawAddRow(CurrentDictionary);
            GUILayout.Space(20);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(Localizer.Format("#autoLOC_149410"), GUILayout.Width(120)))
                    Destroy();
                GUILayout.FlexibleSpace();
            }
            GUI.DragWindow();
        }

        private void DrawDictionaryEditor(Dictionary<string, string> dict)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Value", GUILayout.Width(320 + 250));
            GUILayout.Label("", GUILayout.Width(70));
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

            // Copy to list so we can safely edit/delete while iterating
            List<KeyValuePair<string, string>> rows = dict.ToList();

            foreach (KeyValuePair<string, string> row in rows)
            {
                string oldKey = row.Key;
                string oldValue = row.Value;

                string editedKey = oldKey;
                string editedValue = oldValue;

                bool delete = false;

                GUILayout.BeginHorizontal();

                // editedKey = GUILayout.TextField(editedKey, GUILayout.Width(250));
                editedValue = GUILayout.TextField(editedValue, GUILayout.Width(320 + 250));

                if (GUILayout.Button(	Localizer.Format("#autoLOC_129950") , GUILayout.Width(70)))
                    delete = true;

                GUILayout.EndHorizontal();

                if (delete)
                {
                    dict.Remove(oldKey);
                    continue;
                }

                bool keyChanged = editedKey != oldKey;
                bool valueChanged = editedValue != oldValue;

                if (keyChanged)
                {
                    editedKey = editedKey.Trim();

                    if (!string.IsNullOrEmpty(editedKey) && !dict.ContainsKey(editedKey))
                    {
                        dict.Remove(oldKey);
                        dict[editedKey] = editedValue;
                    }
                }
                else if (valueChanged)
                {
                    dict[oldKey] = editedValue;
                }
            }

            GUILayout.EndScrollView();
        }

        private void DrawAddRow(Dictionary<string, string> dict)
        {
            GUILayout.Label(Localizer.Format("#EXCD-19"));

            GUILayout.BeginHorizontal();

            newValue = GUILayout.TextField(newValue, GUILayout.Width(320 + 250));

            GUI.enabled = (newValue.Length > 0);
            if (GUILayout.Button(Localizer.Format("#autoLOC_8100307"), GUILayout.Width(70)))
            {
                dict[(dict.Count + 1).ToString()] = newValue;

                newKey = "";
                newValue = "";
            }

            GUI.enabled = true;

            GUILayout.EndHorizontal();
        }
    }
}