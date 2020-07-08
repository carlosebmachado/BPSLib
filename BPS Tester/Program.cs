using BPS;
using BPS.Util;
using System;
using System.Data;

namespace Tester
{
    class Program
    {
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
            File bpsFile = BPSIO.Read("../../../../read_test.bps");

            foreach (var s in bpsFile.FindAll())
            {
                Console.WriteLine(s.Name);

                foreach (var d in s.FindAll())
                {
                    Console.WriteLine(d.Key + ":" + d.Value);
                }
                Console.WriteLine("\n");
            }

            BPSIO.Write(bpsFile, "../../../../write_test");
        }

        public static void RemoveSection_Test()
        {
            File bpsFile = BPSIO.Read("../../../../read_test");

            Console.WriteLine(bpsFile.Remove("section"));

            BPSIO.Write(bpsFile, "../../../../write_test");
        }

        public static void SectionExists_Test()
        {
            File bpsFile = BPSIO.Read("../../../../read_test");

            Console.WriteLine(bpsFile.Exists("section"));

            BPSIO.Write(bpsFile, "../../../../write_test");
        }

        public static void RemoveData_Test()
        {
            File bpsFile = BPSIO.Read("../../../../read_test");

            bpsFile.Find("section").Remove("key");

            BPSIO.Write(bpsFile, "../../../../write_test");
        }

        public static void FindSection_Test()
        {
            File bpsFile = BPSIO.Read("../../../../read_test");

            Console.WriteLine(bpsFile.Find("section").Name);
            foreach (var d in bpsFile.Find("section").FindAll())
            {
                Console.WriteLine(d.Key + ":" + d.Value);
            }

            BPSIO.Write(bpsFile, "../../../../write_test");
        }
    }
}
