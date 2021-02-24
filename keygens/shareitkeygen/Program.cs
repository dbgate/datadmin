// define UNICODE_GEN to support UTF8 encoded in-/output files
#define UNICODE_GEN

// define BINARY_GEN if the returned key is a file of a specified MIME type
// Note that binary key generators can also return text files if the content
// type is specified as "text/plain". Therefore you should always provide
// binary key generators unless you need the CCKey feature of textual keys
// instead of XML notification.
#define BINARY_GEN

/** 
 	C# key generator example

	(c) 2004-2006 element 5 
  (c) 2006 Digital River GmbH

	written by Stefan Weber

	SDK 3 File Revision 3
*/

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace shareitkeygen
{
    public enum KeyGenReturnCode : int
    {
        // success
        ERC_SUCCESS = 00,
        ERC_SUCCESS_BIN = 01,
        // failure
        ERC_ERROR = 10,
        ERC_MEMORY = 11,
        ERC_FILE_IO = 12,
        ERC_BAD_ARGS = 13,
        ERC_BAD_INPUT = 14,
        ERC_EXPIRED = 15,
        ERC_INTERNAL = 16
    };

    // key generator exception class
    public class KeyGenException : System.Exception
    {
        public KeyGenReturnCode ERC;

        public KeyGenException(string message, KeyGenReturnCode e)
            : base(message)
        {
            ERC = e;
        }
    };

    class Program
    {
        // possible key generator exit codes - do not modify

        // input/output file encoding
#if UNICODE_GEN
        private static Encoding fileEncoding = new UTF8Encoding();
#else
	private static Encoding fileEncoding = Encoding.GetEncoding("ISO-8859-1");
#endif

        // list of input values
        private static SortedList Inputs = new SortedList();

        // generated key data
#if BINARY_GEN
        private static string KeyMIMEType; // the MIME type
        private static string KeyDisplayFileName; // the displayed filename
        private static byte[] KeyData; // the actual key data
#else
	private static string KeyResult1; // the key for the user
	private static string KeyResult2; // the cckey for the publisher
#endif

        // get input string values, return empty string if not defined
        public static string GetValue(string key)
        {
            if (Inputs.ContainsKey(key))
                return Inputs[key].ToString();
            else
                return "";
        }

        // a simple example algorithm using MD5 message digests
        public static void GenerateKey()
        {
            // compose a string using a secret value (only known to the author of
            // the key generator and the application to be unlocked by the key)
            // and some of the input values

            // here the secret is simply "S.E.C.R.E.T" :-)
            // and we just use the registration name
            string productid = GetValue("PRODUCT_ID");
            string fname = GetValue("FIRSTNAME");
            string lname = GetValue("LASTNAME");
            string email = GetValue("EMAIL");
            string purid = GetValue("PURCHASE_ID");
            string runningno = GetValue("RUNNING_NO");
            string licid = "shareit-" + purid + "-" + runningno;
            var fullname = new List<string>();
            if (!String.IsNullOrEmpty(fname)) fullname.Add(fname);
            if (!String.IsNullOrEmpty(lname)) fullname.Add(lname);
            string license = keygenlib.LicenseTool.CreateLicense(String.Join(" ", fullname.ToArray()), email, "shareit_id", productid, licid, null);

#if BINARY_GEN
            // create a binary key
            KeyData = Encoding.UTF8.GetBytes(license);
            KeyDisplayFileName = "datadmin.license";
            KeyMIMEType = "application/octet-stream";

            // note: there is no cckey generated for binary key generators since
            // the copy exectly matches the original. it is sent to the publisher
            // via XML order notification mails
#else
		// create a textual key
		byte[] HashCode = md5.ComputeHash(digestInput);

		// result 1 - key for the customer
		KeyResult1 = Convert.ToBase64String(HashCode);

		// result 2 - cckey for the publisher
		KeyResult2 = GetValue("REG_NAME") + " " + KeyResult1;
#endif
        }


        // split a string at the first equals sign and add key/value to Inputs[]
        public static void AddInputLine(string line)
        {
            int posEqual = line.IndexOf('=');

            if (posEqual > 0)
            {
                string akey = line.Remove(posEqual, line.Length - posEqual);
                string avalue = line.Substring(posEqual + 1);

                if (avalue.Length > 0)
                {
                    Inputs.Add(akey, avalue);
                }
            }
        }

        // read the input file and parse its lines into the Inputs[] list
        public static void ReadInput(string pathname)
        {
            Inputs.Clear();

            // attempt to open the input file for read-only access
            FileStream fsIn = new FileStream(pathname, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fsIn, fileEncoding, true);

            // process every line in the file
            for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
            {
                AddInputLine(Line.Trim());
            }
            // explicitly close the StreamReader to properly flush all buffers
            sr.Close(); // this also closes the FileStream (fsIn)

            // check the input encoding
            string EncName = GetValue("ENCODING");
#if UNICODE_GEN
            if (EncName != "UTF8")
            {
                throw new KeyGenException("bad input encoding, expected UTF-8",
                                                                    KeyGenReturnCode.ERC_BAD_INPUT);
            }
#else
		if ((EncName != "") && (EncName != "ISO-8859-1")) {
			throw new KeyGenException("bad input encoding, expected ISO-8859-1",
																KeyGenReturnCode.ERC_BAD_INPUT);
		};
#endif

            // check for valid input
            string regName = GetValue("REG_NAME");
            if (regName.Length < 8)
            {
                throw new KeyGenException("REG_NAME must have at least 8 characters",
                KeyGenReturnCode.ERC_BAD_INPUT);
            }

        }

        // write a string to an output file using the encoding specified in the input file
        public static void WriteOutput(string pathname, string data)
        {
            // Create an instance of StreamWriter to write text to a file.
            // The using statement also closes the StreamWriter.
            FileStream fsOut = new FileStream(pathname, FileMode.Create);

            using (StreamWriter sw = new StreamWriter(fsOut, fileEncoding))
            {
                sw.Write(data);
            }
        }

#if BINARY_GEN
        // write a binary byte array to an output file
        public static void WriteOutputData(string pathname, byte[] data)
        {
            // Create an instance of StreamWriter to write text to a file.
            // The using statement also closes the StreamWriter.
            FileStream fsOut = new FileStream(pathname, FileMode.Create);

            using (BinaryWriter bw = new BinaryWriter(fsOut))
            {
                bw.Write(data);
            }
        }
#endif


        public static void Main(string[] args)
        {
            Console.WriteLine("DatAdmin Share-It key generator");

            try
            {
                if (args.Length == 3)
                {
                    Console.Write("> reading input file: ");
                    Console.WriteLine(args[0]);
                    ReadInput(args[0]);

                    Console.WriteLine("> processing ... ");
                    GenerateKey();

                    Console.WriteLine("> writing output files: ");
#if BINARY_GEN
                    // write MIME type and display filename to output file #1
                    WriteOutput(args[1], KeyMIMEType + ":" + KeyDisplayFileName);
                    WriteOutputData(args[2], KeyData);

                    // binary key generator must return ERC_SUCCESS_BIN on success
                    Environment.ExitCode = (int)KeyGenReturnCode.ERC_SUCCESS_BIN;
#else
				WriteOutput(args[1], KeyResult1);
				WriteOutput(args[2], KeyResult2);
				Environment.ExitCode = (int) KeyGenReturnCode.ERC_SUCCESS;
#endif

                }
                else
                {
                    Console.WriteLine("Usage: <input> <output1> <output2>");
                    Environment.ExitCode = (int)KeyGenReturnCode.ERC_BAD_ARGS;
                }
            }
            catch (KeyGenException e)
            {
                Console.WriteLine("* KeyGen Exception: " + e.Message);

                // set the exit code to the ERC of the exception object
                Environment.ExitCode = (int)e.ERC;

                // and write the error message to output file #1
                try
                {
                    WriteOutput(args[1], e.Message);
                }
                catch { };
            }
            catch (Exception e)
            {
                // for general exceptions return ERC_ERROR
                Environment.ExitCode = (int)KeyGenReturnCode.ERC_ERROR;
                Console.WriteLine("* CLR Exception: " + e.Message);

                // and write the error message to output file #1
                try
                {
                    WriteOutput(args[1], e.Message);
                }
                catch { };
            }

            Console.WriteLine("ExitCode: {0}", Environment.ExitCode);
        }
    }
}
