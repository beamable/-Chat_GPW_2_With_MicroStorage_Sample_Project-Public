using Beamable.Common;
using Beamable.Samples.Core;
using Beamable.Samples.SampleProjectBase;
using UnityEditor;

using static Beamable.Common.Constants.MenuItems.Windows;

namespace Beamable.Samples.GPW
{
	/// <summary>
	/// Automatically focus on the sample project readme file when Unity first opens.
	/// </summary>
	[CustomEditor(typeof(Readme))]
	[InitializeOnLoad]
	public class AutoOpenReadme : ReadmeEditor
	{
		// Edit per project
		private const string ReadmeName = "GPW" + " " +  ReadmeDisplayName;
		private const string ReadmeSubfolder = "/Chat/";
		
		// Do not edit
		private const string ReadmeDisplayName = "Readme";
		private const string FindAssetsFilter = "Readme t:Readme";
		private const string SessionStateKeyWasAlreadyShown = "Beamable.Samples.TBF.AutoOpenReadme.wasAlreadyShown";
		private static string[] FindAssetsFolders = new string[] { "Assets" };

		static AutoOpenReadme()
		{
			EditorApplication.delayCall += SelectReadmeAutomatically;
		}

		private static void SelectReadmeAutomatically()
		{
			if (!SessionState.GetBool(SessionStateKeyWasAlreadyShown, false))
			{
				SelectSpecificReadmeMenuItem();
				SessionState.SetBool(SessionStateKeyWasAlreadyShown, true);
			}
		}

		[MenuItem(
			Paths.MENU_ITEM_PATH_WINDOW_BEAMABLE_SAMPLES + ReadmeSubfolder +
			Constants.Commons.OPEN + " " + ReadmeName,
			priority = Orders.MENU_ITEM_PATH_WINDOW_PRIORITY_4)]
		private static Readme SelectSpecificReadmeMenuItem()
		{
			// Reset SessionState if/when MenuItem is used
			SessionState.SetBool(SessionStateKeyWasAlreadyShown, false);
			return ReadmeEditor.SelectReadme(FindAssetsFilter, FindAssetsFolders);
		}
	}
}
