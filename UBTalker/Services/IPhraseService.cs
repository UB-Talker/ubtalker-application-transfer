using System.Collections.Generic;

namespace UBTalker.Services
{
    public interface IPhraseService
    {
        List<string[]> GetPhrases();

        void Speak(string phrase);
    }
}
