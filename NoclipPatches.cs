using HarmonyLib;
using UnityEngine;

namespace NoclipTool
{
    [HarmonyPatch(typeof(Player))]
internal static class PlayerNoclipPatch
{
    [HarmonyPatch("FixedUpdate")]
    [HarmonyPostfix]
    private static void Postfix_FixedUpdate(Player __instance)
    {
        // Работаем только со своим персонажем
        if (__instance != Player.m_localPlayer) return;
        NoclipController.UpdateNoclip(__instance);
    }

    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    private static void Postfix_Update(Player __instance)
    {
        if (__instance != Player.m_localPlayer) return;

        if (NoclipToolPlugin.ToggleNoclipKey.Value.IsDown())
            NoclipController.Toggle();

        if (NoclipToolPlugin.ToggleMenuKey.Value.IsDown())
            NoclipController.MenuVisible = !NoclipController.MenuVisible;
    }
}
}