using System;
using System.Collections.Generic;
using System.Text;

namespace Eccezioni
{
    class StagioneException : Exception
    {
        public StagioneException(string s): base($"Stagione '{s}' non consentita!!") { }
    }
}
