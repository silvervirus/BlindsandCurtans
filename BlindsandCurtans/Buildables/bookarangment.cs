using BlindsandCurtans.Main;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace BlindsandCurtains.Books
{
    public class Bookang
    {
        public static TechType techType;
        public static void Patch()
        {
            BuildableBookang.Register();
        }

        
    }

    public static class BuildableBookang
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Bookarrangement", "Books arrangement", "Decorate your base with a set of books in an arrangement!", "English")
        .WithIcon(CandB.GetSprite("Books"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate bookSetClone = new CloneTemplate(Info, "54c189dd-7ef0-4b1c-9080-186eebbe3fe8");

            bookSetClone.ModifyPrefab += obj =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.AllowedOnConstructable;

                // Loop through all the child objects of the book set and add BoxColliders to them
                foreach (Transform child in obj.transform)
                {
                    BoxCollider collider = child.gameObject.EnsureComponent<BoxCollider>();
                    collider.center = Vector3.zero;
                    collider.size = Vector3.one;
                }

                // Find the main model of the book set
                GameObject model = obj.transform.Find("Generic_Books_01_arrangement_shelf_b").gameObject;

                // Add BoxCollider to the main model to allow placement detection
               

                // Change the layer of the main model to the Default layer to avoid red placement indicators
                model.layer = LayerMask.NameToLayer("Default");

                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
            };

            prefab.SetUnlock(TechType.Builder);
            prefab.SetGameObject(bookSetClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 1)));

            prefab.Register();

            Bookang.techType = Info.TechType;
        }
       
    }
}
