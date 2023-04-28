using RMC.Core.ReadMe;
using UnityEditor;

namespace TezosSDKSamples.RPG
{
	public static class RPGMenuItems
	{
		//  Properties ------------------------------------

        
		//  Fields ----------------------------------------
		
		[MenuItem( RPGConstants.PathMenuItemWindowCompanyProject + "/" + "Open ReadMe", 
			false,
			RPGConstants.PriorityMenuItem_Examples)]
		public static void SelectReadmes()
		{
            //Hardcoded guid for ReadMe
            var guid = "b9b38435c6335be46a3471cf04b7863e";
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));
            ReadMeHelper.PingObject(readmeObject);
            ReadMeHelper.SelectObject(readmeObject);
		}
		
	}
}
