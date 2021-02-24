using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CommandLineCommand(Name="list",Description= "Lists specified entities")]
    public class ListCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : CommandLineCommandInstanceBase
        {
            string m_entity;

            [CommandLineParameter(Positional = true, Mandatory = true, Name = "entity", Description = "entity which will be listed, set \"entity\" to list supported entities")]
            public string Entity
            {
                get { return m_entity; }
                set { m_entity = value; }
            }


            public override void RunCommand()
            {
                if (Entity == "entity")
                {
                    foreach (var item in AddonRegister.Instance.GetAddonTypes())
                    {
                        Console.Out.WriteLine(item.Name);
                    }
                }
                else
                {
                    AddonType type = AddonRegister.Instance.FindAddonType(Entity);

                    string format = "{0,-30} | {1}";
                    Console.WriteLine(format, "Name", "Title");
                    Console.WriteLine("-----------------------------------------------------");
                    foreach (var item in type.CommonSpace.GetAllAddons())
                    {
                        Console.WriteLine(format, item.Name, Texts.Get(item.Title));
                    }
                }
            }
        }
    }
}
