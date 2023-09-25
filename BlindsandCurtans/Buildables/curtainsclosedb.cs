﻿using BlindsandCurtains.Books;
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

namespace BlindsandCurtains.Curtainsb
{
    public class CurtainsClosedb
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableCurtainsClosedb.Register();
        }
    }

    public static class BuildableCurtainsClosedb
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("WindowCurtainsClosedb", "Window Curtains Closedb", "Add beautiful curtains to your base!", "English")
            .WithIcon(CandB.GetSprite("ClosedC"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, "8b5db1a9-6540-4173-bb98-16b9d7dc7949");

            curtainsClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;

                // Loop through all the child objects of the curtains and add BoxColliders to them
                foreach (Transform child in obj.transform)
                {
                    BoxCollider collider = child.gameObject.EnsureComponent<BoxCollider>();
                    collider.center = Vector3.zero;
                    collider.size = Vector3.one;
                }

                // Find the main model of the curtains
                GameObject model = obj.transform.Find("WindowCurtains_01_closed_a_floating").gameObject;

                // Add BoxCollider to the main model to allow placement detection
                BoxCollider mainModelCollider = model.EnsureComponent<BoxCollider>();
                mainModelCollider.center = new Vector3(0f, 0f, 0.14f); // Adjust the center to the desired position
                mainModelCollider.size = new Vector3(3.0f, 0.1f, 3.0f); // Increase the height (Y) to make it easier to interact with the curtains

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(curtainsClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2)));

            prefab.Register();

            TechType techType = Info.TechType;
        }
    }
}
