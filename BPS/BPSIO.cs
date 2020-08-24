using BPS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BPS
{
    public class BPSIO
    {
        #region Vars

        private const string ERR_OPEN_NEW_WOUT_CLOSE_PREV = "Trying to open a new section without closing the previous one.";
        private const string ERR_CLOSE_WOUT_OPEN_PREV = "Section was closed without open it previously.";

        private const string KV_HEADER = "# BPS File";
        private const string KV_NEXTLINE = "\n";
        private const string KV_LAB = "<";
        private const string KV_RAB = ">";
        private const string KV_TAB = "    ";
        private const string KV_SEPARATOR = ":";

        #endregion Vars


        #region Methods

        #region Public

        /// <summary>
        /// Returns the readed BPS file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File readed</returns>
        public static File Read(string path)
        {
            List<string> data;
            File file;

            try
            {
                data = LexicalAnalysis(Extension.Normalize(path));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                file = Parser(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return file;
        }

        /// <summary>
        /// Write a BPS file
        /// </summary>
        /// <param name="file">The file to be write</param>
        public static void Write(File file, string path)
        {
            try
            {
                StreamWriter wf = new StreamWriter(Extension.Normalize(path));

                wf.WriteLine(KV_HEADER + KV_NEXTLINE);

                foreach (Section section in file.FindAll())
                {
                    wf.WriteLine(KV_LAB + section.Name);
                    foreach (Data data in section.FindAll())
                    {
                        wf.WriteLine(KV_TAB + data.Key + KV_SEPARATOR + data.Value);
                    }
                    wf.WriteLine(KV_RAB + KV_NEXTLINE);
                }
                wf.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public


        #region Private

        private static List<string> LexicalAnalysis(string path)
        {
            try
            {
                List<string> data = new List<string>();
                string line;

                StreamReader file = new StreamReader(path);

                while ((line = file.ReadLine()) != null)
                {
                    // Ignora as linhas vazias
                    if (line.Equals(""))
                        continue;
                    // Remove espaços em branco
                    if (line[0].Equals(' '))
                        line = line.Trim();
                    // Remove comentários
                    line = RemoveComments(line);
                    // Caso houvesse linhas com apenas comentários, seria removida
                    if (line.Equals(""))
                        continue;
                    // Remove espaços em branco
                    if (line[0].Equals(' '))
                        line = line.Trim();
                    // Caso ainda haja comentários, termina de remover
                    if (line[0].Equals('#'))
                        continue;
                    // Caso ainda sobre alguma linha vazia
                    line = line.Trim();
                    if (line.Equals(""))
                        continue;
                    // Se a linha passou por todas as verificações é adicionada às linhas válidas
                    data.Add(line);
                }
                file.Close();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static File Parser(List<string> data)
        {
            List<List<string>> rawSections = new List<List<string>>();
            List<Section> sections = new List<Section>();
            bool open = false;

            // Procura nos dados brutos, as seções
            foreach (string d in data)
            {
                // Encontrou a tag de abrir seção
                if (d[0].Equals('<'))
                {
                    // Se encontra uma tag de abrir seção mas ela já foi aberta
                    if (open)
                    {
                        throw new ArgumentException(ERR_OPEN_NEW_WOUT_CLOSE_PREV);
                    }
                    else
                    {
                        open = true;
                    }
                }
                // Encontrou a tag de fechar seção
                if (d.Equals(">"))
                {
                    // Se encontra uma tag de fechar seção mas ela não foi aberta
                    if (!open)
                    {
                        throw new ArgumentException(ERR_CLOSE_WOUT_OPEN_PREV);
                    }
                    else
                    {
                        rawSections.Add(new List<string>());
                        open = false;
                    }
                }

            }

            // Loop para cada seção encontrada anteriormente
            for (int i = 0; i < rawSections.Count(); i++)
            {
                // Passa todas as linhas cruas para o rawSection
                while (true)
                {
                    string curLine = data[0];
                    data.RemoveAt(0);
                    rawSections[i].Add(curLine);
                    if (curLine.Equals(">")) break;
                }
            }

            // Percorre rawSections criando as variáveis
            foreach (List<string> sec in rawSections)
            {
                // Remove qualquer linha que não começe com '<'
                // PROVAVELMENTE SERÁ REMOVIDA
                while (!sec[0][0].Equals('<'))
                {
                    sec.RemoveAt(0);
                }
                foreach (string s in sec)
                {
                    // Se estiver abrindo a seção
                    if (s[0].Equals('<'))
                    {
                        string newS = s.Substring(1, s.Length - 1);
                        sections.Add(new Section(newS));
                        //sections[sections.Count() - 1].Name = newS;
                        continue;
                    }
                    // Encontrou o fim da seção
                    if (s.Equals(">"))
                    {
                        break;
                    }
                    // Senão entrou em nenhum if anterior, significa que é uma key/value e será adicionada a seção
                    // Divide pelo ':'
                    var r = s.Split(':');
                    // Cria um novo dado com key e data
                    sections[sections.Count() - 1].Add(new Data(r[0], r[1]));
                }
            }

            return new File(sections);
        }

        private static string RemoveComments(string str)
        {
            // Divide e retorna apenas a parte esquerda da linha
            // A parte direita é apenas comentário
            var r = str.Split('#');
            r[0] = r[0].Trim();
            return r[0];
        }

        #endregion Private

        #endregion Methods

    }
}