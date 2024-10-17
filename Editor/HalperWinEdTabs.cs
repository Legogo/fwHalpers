using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalperWinEdTabs
{
    int tabActive = 0;
    string[] tabsLabels;
    GUIContent[] tabs;
    
    public HalperWinEdTabs(string[] tabsLabels)
    {
        this.tabsLabels = tabsLabels;
        refresh();
    }

    public void refresh(bool force = false)
    {

        if (tabsLabels == null || force)
        {
            tabActive = 0;
            
            tabs = new GUIContent[tabsLabels.Length];

            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i] = new GUIContent(tabsLabels[i]);
            }
        }

    }

    public bool isValid()
    {
        if (tabActive < 0)
        {
            Debug.LogWarning("wrong index ?");
            return false;
        }

        if (tabs == null)
        {
            refresh(true);
            //Debug.LogWarning("tabs null ?");
            return false;
        }

        if (tabs.Length <= 0)
        {
            Debug.LogWarning("no tabs labels ?");
            return false;
        }

        return true;
    }

    public int drawTabsHeader()
    {
        if (!isValid()) return -1;

        tabActive = GUILayout.Toolbar(tabActive, tabs, "LargeButton");
        return tabActive;
    }

    public int getTabIndex() => tabActive;


}
