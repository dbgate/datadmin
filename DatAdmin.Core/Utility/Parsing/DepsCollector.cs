using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class DepsName
    {
        public List<string> Components = new List<string>();
        public void AddComponent(string name)
        {
            Components.Add(name);
        }

        public bool Match(DependencyItem obj)
        {
            return Components.Count >= 1 && String.Compare(Components[Components.Count - 1], obj.Name.Name, true) == 0;
        }

        public override string ToString()
        {
            return Components.CreateDelimitedText(".");
        }

        public NameWithSchema GetFullName()
        {
            if (Components.Count == 0) return null;
            if (Components.Count == 1) return new NameWithSchema(Components.Last());
            return new NameWithSchema(Components[Components.Count - 2], Components.Last());
        }

        public bool SimilarTo(NameWithSchema name)
        {
            var my = GetFullName();
            if (my == null) return false;
            return my.Name.ToUpperInvariant() == name.Name.ToUpperInvariant();
        }
    }

    public class DepsCollector
    {
        public List<DepsName> Names = new List<DepsName>();
        public void AddName(DepsName name)
        {
            Names.Add(name);
        }
    }
}
