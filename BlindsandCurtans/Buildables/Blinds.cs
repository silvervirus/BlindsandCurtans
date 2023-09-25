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

namespace BlindsandCurtains.blinds
{
    public class BlindsClosedb
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableBlindsClosed.Register();
        }
    }

    public static class BuildableBlindsClosed
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Blindsopened", "Window Blinds Open", "Add beautiful Open blinds to your base!", "English")
            .WithIcon(CandB.GetSprite("Openb"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate blindsClosedClone = new CloneTemplate(Info, "910ca903-e2b1-493b-bb91-5774a3ce995e");

            blindsClosedClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Wall | ConstructableFlags.AllowedOnConstructable;

                // Loop through all the child objects of the blinds and add BoxColliders to them
                foreach (Transform child in obj.transform)
                {
                    BoxCollider collider = child.gameObject.EnsureComponent<BoxCollider>();
                    collider.center = Vector3.zero;
                    collider.size = Vector3.one;
                }

                // Find the main model of the blinds
                GameObject model = obj.transform.Find("windowblinds_01_a_LOD0").gameObject;
                
                // Add BoxCollider to the main model to allow placement detection
                BoxCollider mainModelCollider = model.EnsureComponent<BoxCollider>();
                mainModelCollider.center = new Vector3(0f, 0.18f, 0.28f);
                mainModelCollider.size = new Vector3(3.0f, 0.1f, 3.0f);
                
                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(blindsClosedClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3), new Ingredient(TechType.Glass, 1)));

            prefab.Register();

            BlindsClosedb.techType = Info.TechType;

        }
        
    }
}
