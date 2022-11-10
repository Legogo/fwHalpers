using UnityEditor;

/// <summary>
/// some various context menu related to Unity
/// </summary>

namespace fwp.halpers.editor
{
    using fwp.halpers;

    public class EditorContextMenuToolsUnity
    {
        [MenuItem("Assets/clear:   PlayerPrefs")]
        public static void ctxmClearPPrefs()
        {
            HalperUnity.clearPlayerPrefs();
        }

        [MenuItem("Assets/open:   persistant data path")]
        static public void osOpenDataPathFolder()
        {
            HalperNatives.os_openFolder(HalperNatives.getDataPath());
        }

    }
}
