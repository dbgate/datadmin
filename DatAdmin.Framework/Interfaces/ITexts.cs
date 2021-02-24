using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface ITextProvider
    {
        string GetText(string name, string lang);
    }
}
