using BlindsandCurtains.Books;
using BlindsandCurtans.Main;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UWE;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace BlindsandCurtains.Gen
{
    public class minecart
    {
        public static TechType techType;
        public static void Patch()
        {
            Buildablemc.Register();
        }
    }

    public static class Buildablemc
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("mc", "MineCart", "Add a Mine Cart to your base!", "English")
            .WithIcon(CandB.GetSprite("OpenC"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, "0c8d8cf5-44dc-4666-85da-8a2b9ff32768");

            curtainsClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;
               
                // Find the main model of the curtains
                GameObject model = obj.transform.Find("miningsite_cart.001").gameObject;

                // Add BoxCollider to the main model to allow placement detection
               

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
                PrefabUtils.AddStorageContainer(obj, "PowerStorage", "PS", 6, 6, true);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(curtainsClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2)));

            prefab.Register();

           minecart.techType = Info.TechType;
        }
    }
}
