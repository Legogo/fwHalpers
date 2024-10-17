using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fwp.halpers
{

	static public class HalperGuiOptions
	{
		static public GUILayoutOption wXS = GUILayout.Width(30f);
		static public GUILayoutOption wS = GUILayout.Width(50f);
		static public GUILayoutOption wM = GUILayout.Width(100f);
		static public GUILayoutOption wL = GUILayout.Width(150f);

		//GUILayoutOption btnWidthL = GUILayout.Width(75f);

		static GUILayoutOption width_small;
		static public GUILayoutOption getWSmall()
		{
			if (width_small == null) width_small = wS;
			return width_small;
		}

		static GUILayoutOption width_med;
		static public GUILayoutOption getWMed()
		{
			if (width_med == null) width_med = wM;
			return width_med;
		}

		static GUILayoutOption width_large;
		static public GUILayoutOption getWLarge()
		{
			if (width_large == null) width_large = wL;
			return width_large;
		}

		static GUIStyle text_active;
		static public GUIStyle getTextGreen()
		{

			if (text_active == null)
			{
				text_active = new GUIStyle();
				text_active.normal.textColor = Color.green;
			}

			return text_active;
		}

		static GUIStyle text_inactive;
		static public GUIStyle getTextRed()
		{

			if (text_inactive == null)
			{
				text_inactive = new GUIStyle();
				text_inactive.normal.textColor = Color.red;
			}

			return text_inactive;
		}

		static private GUIStyle gWinTitle;
		static public GUIStyle getWinTitle()
		{
			if (gWinTitle == null)
			{
				gWinTitle = new GUIStyle();

				gWinTitle.richText = true;
				gWinTitle.alignment = TextAnchor.MiddleCenter;
				gWinTitle.normal.textColor = Color.white;
				gWinTitle.fontSize = 20;
				gWinTitle.fontStyle = FontStyle.Bold;
				gWinTitle.margin = new RectOffset(10, 10, 10, 10);
				//gWinTitle.padding = new RectOffset(30, 30, 30, 30);

			}

			return gWinTitle;
		}

		static private GUIStyle gSectionTitle;
		static public GUIStyle getSectionTitle(int size = 15)
		{
			if (gSectionTitle == null)
			{
				gSectionTitle = new GUIStyle();

				gSectionTitle.richText = true;
				gSectionTitle.alignment = TextAnchor.MiddleCenter;
				gSectionTitle.normal.textColor = Color.white;

				gSectionTitle.fontStyle = FontStyle.Bold;
				gSectionTitle.margin = new RectOffset(10, 10, 10, 10);
				//gWinTitle.padding = new RectOffset(30, 30, 30, 30);

			}

			gSectionTitle.fontSize = size;

			return gSectionTitle;
		}
	}

}