using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CommandLineCommand(Name = "help", Description = "Shows help abut specified command")]
    public class HelpCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : CommandLineCommandInstanceBase
        {
            string m_command;

            [CommandLineParameter(Positional = true, Mandatory = true, Name = "command", Description = "Command, which help is shown")]
            public string Command
            {
                get { return m_command; }
                set { m_command = value; }
            }

            public override void RunCommand()
            {
                ICommandLineCommand cmd = CmdLine.FindCommand(Command);
                if (cmd == null) throw new CommandLineError("DAE-00154 Unknown command:" + Command);
                ICommandLineCommandInstance inst = cmd.CreateInstance();
                List<CmdLine.ParamHolder> holders = CmdLine.LoadHolders(new object[] { inst });
                Dictionary<int, string> posparams = new Dictionary<int, string>();
                foreach (CmdLine.ParamHolder holder in holders)
                {
                    if (holder.Position != null) posparams[holder.Position.Value] = holder.Name;
                }
                List<string> posparamslist = new List<string>();
                while (posparams.Count > 0)
                {
                    int min = PyList.Minimum(posparams.Keys);
                    posparamslist.Add(posparams[min]);
                    posparams.Remove(min);
                }
                Console.Out.WriteLine("Usage: daci " + Command + " " + String.Join(" ", posparamslist.ToArray()) + " [--param1 value1 --param2 value2...]");
                Console.Out.WriteLine(cmd.Description);
                foreach (CmdLine.ParamHolder holder in holders)
                {
                    Console.WriteLine("  " + holder.Name + " - " + holder.Description);
                }
            }
        }
    }
}
