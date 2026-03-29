using BaseLib.Abstracts;
using HarmonyLib;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace BaseLib.Patches.UI;

[HarmonyPatch(typeof(ImageHelper))]
class RoomIconPathPatch
{
    [HarmonyPatch(nameof(ImageHelper.GetRoomIconPath))]
    [HarmonyPrefix]
    static bool CustomPath(MapPointType mapPointType, RoomType roomType, ModelId? modelId, ref string? __result)
    {
        if (modelId != null && ModelDb.GetById<AbstractModel>(modelId) is ICustomModel customModel)
        {
            switch (customModel)
            {
                case CustomAncientModel ancient:
                    __result = ancient.CustomRunHistoryIconPath;
                    return __result == null;
            }
        }

        return true;
    }
    
    [HarmonyPatch(nameof(ImageHelper.GetRoomIconOutlinePath))]
    [HarmonyPrefix]
    static bool CustomOutlinePath(MapPointType mapPointType, RoomType roomType, ModelId? modelId, ref string? __result)
    {
        if (modelId != null && ModelDb.GetById<AbstractModel>(modelId) is ICustomModel customModel)
        {
            switch (customModel)
            {
                case CustomAncientModel ancient:
                    __result = ancient.CustomRunHistoryIconOutlinePath;
                    return __result == null;
            }
        }

        return true;
    }
}