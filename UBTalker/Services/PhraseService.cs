using System.Collections.Generic;
using System.Speech.Synthesis;

namespace UBTalker.Services
{
    class PhraseService : IPhraseService
    {
        private List<string[]> phrases;
        private SpeechSynthesizer synth;

        public PhraseService()
        {
            // Setup Phrases
            phrases = new List<string[]>();
            LoadPhrases();

            // Setup Speech Synthesizer
            synth = new SpeechSynthesizer();
            synth.Volume = 100;
            synth.Rate = -2;
        }

        private void LoadPhrases()
        {
            // TODO offload this to a file, and put the loading logic here

            phrases.Add(new string[] { "TV channel up", "TV channel down", "TV volume up", "TV volume down" });
            phrases.Add(new string[] { "Boost", "Adjust body position", "Head of bed up", "Head of bed down" });
            phrases.Add(new string[] { "Clean face", "Need to be changed", "Put on booties", "Take off booties" });
            phrases.Add(new string[] { "Chap stick", "Wipe mouth", "Swab with mouth wash", "Swab with water" });
            phrases.Add(new string[] { "Turn light on", "Turn light off", "Turn on fan", "Turn off fan" });
            phrases.Add(new string[] { "Open window", "Close window", "Open curtain", "Close curtain" });
            phrases.Add(new string[] { "Put sheet on", "Take sheet off", "I'm cold", "I'm hot" });
            phrases.Add(new string[] { "Get letter board", "Text Jen", "Uncover feet", "Perform range of motion" });
            phrases.Add(new string[] { "Pain meds", "Eye drops", "Nose spray", "Flush foley" });
            phrases.Add(new string[] { "Feed on", "Feed off", "Soda in tube", "Water in tube" });
            phrases.Add(new string[] { "Suction mouth", "Suction nose", "Suction track", "Vent tubes pulling" });
            phrases.Add(new string[] { "Air in cuff", "Ties too tight", "Cough assist", "Breathing treatement" });
        }

        public List<string[]> GetPhrases()
        {
            return phrases;
        }
        
        public void Speak(string phrase)
        {
            synth.SpeakAsync(phrase);
        }
    }
}
