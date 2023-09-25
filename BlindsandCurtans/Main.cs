using BepInEx;
using Nautilus.Utility;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BlindsandCurtans.Main
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class CandB : BaseUnityPlugin
    {
        #region[Declarations]
        private const string
            MODNAME = "Curtains and Blinds",
            AUTHOR = "Cookay",
            GUID = "com.CurtainsandBlinds.CKmod",
            VERSION = "1.0.0.0";
        #endregion
        public IEnumerator Start()
        {

           yield return new WaitUntil(() => SpriteManager.hasInitialized);

            BlindsandCurtains.Curtains.Curtains.Patch();
            BlindsandCurtains.Curtains.Curtainsb.Patch();
            BlindsandCurtains.Curtains.Curtainsc.Patch();
            BlindsandCurtains.Curtains.CurtainsClosed.Patch();
            BlindsandCurtains.Curtains.Curtainsdamaged.Patch();
            BlindsandCurtains.Curtains.Curtainsdamageda.Patch();
            BlindsandCurtains.blinds.BlindsClosedb.Patch();
            BlindsandCurtains.Books.BookSet.Patch();
            BlindsandCurtains.Books.Bookopen.Patch();
            BlindsandCurtains.Books.Bookang.Patch();
            BlindsandCurtains.Gen.Hgen.Patch();
            BlindsandCurtains.Gen.TechBarrel.Patch();
            BlindsandCurtains.Gens.BaseGen.Patch();
            BlindsandCurtains.Gen.PowerStorage.Patch();
            BlindsandCurtains.Gen.bioShelves.Patch();
            BlindsandCurtains.Gen.BioCounter.Patch();
            BlindsandCurtains.Gen.subcons.Patch();
            BlindsandCurtains.Gen.SpecimenAnalyzer.Patch();
            BlindsandCurtains.Gen.ShipWreckcon1.Patch();
            BlindsandCurtains.Gen.ShipWreckcon3.Patch();
            BlindsandCurtains.Gen.ShipWreckcon4.Patch();
            BlindsandCurtains.Gen.BioCart.Patch();
            BlindsandCurtains.Gen.extractor.Patch();
            BlindsandCurtains.Gen.minecart.Patch();
            BlindsandCurtains.Gen.Forklift.Patch();
            BlindsandCurtains.Gen.Precursorlab.Patch();
    }

        public static string ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Sprite GetSprite(string name)
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine($"{ModPath}/Assets/{name}.png"));
        }
    }
}
