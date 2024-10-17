using UnityEngine;
using UnityEditor;

namespace fwp.halpers.editor
{

    static public class HalperEditorStyles

    {
        static private GUIStyle gSectionFoldTitle;
        static public GUIStyle getSectionFoldTitle(TextAnchor anchor = TextAnchor.MiddleLeft, int leftMargin = 10)
        {
            if (gSectionFoldTitle == null)
            {
                gSectionFoldTitle = UnityEditor.EditorStyles.foldout;
                gSectionFoldTitle.alignment = anchor;
                gSectionFoldTitle.margin = new RectOffset(leftMargin, 10, 10, 10);
            }
            return gSectionFoldTitle;

        }

    }

}