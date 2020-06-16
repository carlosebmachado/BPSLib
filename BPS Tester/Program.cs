using BPS;
using BPS.Auxiliary;
using System;
using System.Data;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadWrite_Test();
            //RemoveData_Test();
            //RemoveSection_Test();
            //FindSection_Test();
            //SectionExists_Test();

            //Console.Read();
            //System.Threading.Thread.Sleep(60 * 1000);
        }

        public static void RemoveSection_Test()
        {
            BPSFile bpsFile = BPSReader.Read("../../../../read_test");

            Console.WriteLine(bpsFile.RemoveSection("section"));

            BPSWriter.Write(bpsFile, "../../../../write_test");
        }

        public static void SectionExists_Test()
        {
            BPSFile bpsFile = BPSReader.Read("../../../../read_test");

            Console.WriteLine(bpsFile.SectionExists("section"));

            BPSWriter.Write(bpsFile, "../../../../write_test");
        }

        public static void RemoveData_Test()
        {
            BPSFile bpsFile = BPSReader.Read("../../../../read_test");

            bpsFile.RemoveData("section", "key");

            BPSWriter.Write(bpsFile, "../../../../write_test");
        }

        public static void FindSection_Test()
        {
            BPSFile bpsFile = BPSReader.Read("../../../../read_test");

            Console.WriteLine(bpsFile.FindSection("section").Name);
            foreach (var d in bpsFile.FindSection("section").Data)
            {
                Console.WriteLine(d.Key + ":" + d.Value);
            }

            BPSWriter.Write(bpsFile, "../../../../write_test");
        }

        public static void ReadWrite_Test()
        {
            BPSFile bpsFile = BPSReader.Read("../../../../read_test.bps");

            foreach(var s in bpsFile.Sections)
            {
                Console.WriteLine(s.Name);

                foreach (var d in s.Data)
                {
                    Console.WriteLine(d.Key + ":" + d.Value);
                }
                Console.WriteLine("\n");
            }

            BPSWriter.Write(bpsFile, "../../../../write_test");
        }
    }
}