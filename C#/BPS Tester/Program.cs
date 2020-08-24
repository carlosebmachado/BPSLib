using BPS;
using System;

namespace Tester
{
    class Program
    {

        public const string path = "D:/Documentos/OneDrive/DESKTOP/C#/BPSLib/";
        public const string wf = "write_test";
        public const string rf = "read_test.bps";

        static void Main(string[] args)
        {
            //ReadWrite_Test();
            //RemoveData_Test();
            //RemoveSection_Test();
            //FindSection_Test();
            //SectionExists_Test();

            //Console.Read();
            //System.Threading.Thread.Sleep(60 * 1000);
        }

        public static void ReadWrite_Test()
        {
            File bpsFile = BPSIO.Read(path+rf);

            foreach (var s in bpsFile.FindAll())
            {
                Console.WriteLine(s.Name);

                foreach (var d in s.FindAll())
                {
                    Console.WriteLine(d.Key + ":" + d.Value);
                }
                Console.WriteLine("\n");
            }

            BPSIO.Write(bpsFile, path+wf);
        }

        public static void RemoveSection_Test()
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.Remove("section"));

            BPSIO.Write(bpsFile, path + wf);
        }

        public static void SectionExists_Test()
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.Exists("section"));

            BPSIO.Write(bpsFile, path + wf);
        }

        public static void RemoveData_Test()
        {
            File bpsFile = BPSIO.Read(path + rf);

            bpsFile.Find("section").Remove("key");

            BPSIO.Write(bpsFile, path + wf);
        }

        public static void FindSection_Test()
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.Find("section").Name);
            foreach (var d in bpsFile.Find("section").FindAll())
            {
                Console.WriteLine(d.Key + ":" + d.Value);
            }

            BPSIO.Write(bpsFile, path + wf);
        }
    }
}
