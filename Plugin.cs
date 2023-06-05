using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using ProjectM.UI;
using UnityEngine;

namespace RemoveVersionWatermark;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class RemoveVersionWatermarkPlugin : BasePlugin
{
	private static Harmony harmony;

	public override void Load()
	{
		harmony = Harmony.CreateAndPatchAll(typeof(RemoveVersionWatermarkPlugin));
		Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");
	}

	[HarmonyPostfix]
	[HarmonyPatch(typeof(ConnectingView), nameof(ConnectingView.BackgroundButton_OnClick))]
	private static void BackgroundButton_OnClick()
	{
		var versionStringObject = GameObject.Find("VersionString");
		if (versionStringObject != null && versionStringObject.active == true) {
			versionStringObject.active = false;
		}
	}
}