using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace fwp.halpers.editor
{

    /// <summary>
    /// EditorGUILayout.Popup wrapper
    /// </summary>
    public class EditorDrawSelector
    {
        const string _ppref_current = "ppref_selector_current";
        const string _ppref_filter = "ppref_selector_filter";

        int current
        {
            get
            {
                return EditorPrefs.GetInt(_ppref_current, 0);
            }
            set
            {
                EditorPrefs.SetInt(_ppref_current, value);
            }
        }

        string filter
        {
            get
            {
                return EditorPrefs.GetString(_ppref_filter, string.Empty);
            }
            set
            {
                EditorPrefs.SetString(_ppref_filter, value);
                reevealFilter();
            }
        }

        string[] options;
        string[] optionsFiltered;

        bool hasOptions
        {
            get
            {
                if (options == null || optionsFiltered == null) return false;
                if (options != null && options.Length <= 0) return false;
                //if (optionsFiltered != null && optionsFiltered.Length <= 0) return false;
                return optionsFiltered.Length > 0;
            }
        }

        //List<GUILayoutOption> options = new List<GUILayoutOption>();

        System.Action<string> valChanged;

        public EditorDrawSelector(System.Action<string> valChanged)
        {
            this.valChanged = valChanged;
            //reevealFilter();
        }

        public void setup(string[] labels)
        {
            Debug.Log("setup selector with options x" + labels.Length);

            options = labels;
            reevealFilter();
        }

        public void previous()
        {
            loopCurrent(current - 1);
        }

        public void next()
        {
            loopCurrent(current + 1);
        }

        void loopCurrent(int val)
        {
            if (val < 0) val = optionsFiltered.Length - 1;
            if (val >= optionsFiltered.Length) val = 0;

            setCurrentIndex(val);
        }

        void containCurrent()
        {
            if (current < 0) current = 0;

            if (optionsFiltered.Length <= 0) current = 0;
            else if (current >= optionsFiltered.Length) current = 0;
        }

        void setCurrentIndex(int idx)
        {
            current = idx;
            containCurrent();
            valChanged?.Invoke(optionsFiltered[current]);
        }

        void reevealFilter()
        {
            Debug.Assert(options != null, "null options ?");
            Debug.Assert(options.Length > 0, "empty options ?");

            optionsFiltered = null;

            if (string.IsNullOrEmpty(filter))
            {
                optionsFiltered = options; // default options
            }
            else
            {
                // filter options
                List<string> tmp = new List<string>();
                foreach (var o in options)
                {
                    if (o.ToLower().Contains(filter)) tmp.Add(o);
                }
                optionsFiltered = tmp.ToArray();
                //Debug.Log("updated options x" + optionsFiltered.Length + " out of " + options.Length);
            }

            Debug.Assert(optionsFiltered != null, "no filtered options ? #" + current);
            //Debug.Assert(, "oob : "+current + "/" + optionsFiltered.Length);

            containCurrent();

            if (optionsFiltered.Length > 0)
            {
                valChanged?.Invoke(optionsFiltered[current]);
            }
        }

        public void setSelectorTo(string val)
        {
            for (int i = 0; i < optionsFiltered.Length; i++)
            {
                if (optionsFiltered[i].Contains(val))
                {
                    current = i;
                }
            }
        }

        public void draw()
        {
            GUILayout.BeginHorizontal();

            //drawFilter();

            if (hasOptions)
            {
                if (GUILayout.Button("<", EdStyles.getWSmall())) previous();

                int ret = EditorGUILayout.Popup(current, optionsFiltered);
                if (ret != current)
                {
                    setCurrentIndex(ret);
                }

                if (GUILayout.Button(">", EdStyles.getWSmall())) next();

                GUILayout.Label((current + 1) + " / " + optionsFiltered.Length);
            }

            GUILayout.EndHorizontal();
        }

        public void drawFilter()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("clear filter", EdStyles.getWMed()))
            {
                filter = string.Empty; // clear
                                       //setCurrentIndex(0);
            }

            string _filter = EditorGUILayout.TextField(filter, EdStyles.getWLarge());
            if (_filter != filter)
            {
                filter = _filter.ToLower(); // textfield value change
            }

            GUILayout.EndHorizontal();
        }

    }

}