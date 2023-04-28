using RMC.Core.ReadMe;
using UnityEditor;

namespace TezosSDKSamples.RPG
{
	/// <summary>
	/// Automatically Load <see cref="ReadMe"/> when Unity opens
	/// </summary>
	
	[InitializeOnLoad]
	public static class RPGInitializeOnLoad
	{
		//  Properties ------------------------------------

		
		//  Fields ----------------------------------------
		private static readonly string HasShownReadMe = "TezosSDKSamples.HasShownReadMe";
		
		//  Other Methods ---------------------------------
		static RPGInitializeOnLoad()
		{
			if (!SessionState.GetBool(HasShownReadMe, false))
			{
				EditorApplication.update += WaitOneFrame;
			}
		}
		

		private static void WaitOneFrame()
		{
			EditorApplication.update -= WaitOneFrame;
			ReadMeHelper.SelectReadmes();
			SessionState.SetBool(HasShownReadMe, true);
		}
	}
}
