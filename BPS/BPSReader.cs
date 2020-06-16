using BPS.Auxiliary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BPS
{
    public class BPSReader
    {
        #region Vars

        /// <summary></summary>
        private const string ERR_OPEN_NEW_WOUT_CLOSE_PREV = "Trying to open a new section without closing the previous one.";
        /// <summary></summary>
        private const string ERR_CLOSE_WOUT_OPEN_PREV = "Section was closed without having been opened previously.";

        #endregion Vars

        #region Methods

        #region Public

        /// <summary>
        /// Returns the readed BPS file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File readed</returns>
        public static BPSFile Read(string path)
        {
            List<string> rawData;
            List<List<string>> rawSections;

            try
            {
                rawData = ReadData(Extension.Normalize(path));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                rawSections = PrepareRawSections(rawData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Secure functions
            DistributeSections(rawSections, rawData);
            return new BPSFile(CreateSections(rawSections));
        }

        #endregion Public

        #region Private

        /// <summary>
        /// Reads and clears all lines from the file passed by the path
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>A list of all lines in the file</returns>
        private static List<string> ReadData(string path)
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
                    if (line[0] == ' ')
                        line = line.Trim();
                    // Remove comentários
                    line = RemoveComments(line);
                    // Caso houvesse linhas com apenas comentários, seria removida
                    if (line.Equals(""))
                        continue;
                    // Caso ainda haja comentários, termina de remover
                    if (line[0] == '#')
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

        /// <summary>
        /// Removes comments on a valid line
        /// </summary>
        /// <param name="str">The string that will be removed from the comment</param>
        /// <returns>A string without the comment</returns>
        private static string RemoveComments(string str)
        {
            // Divide e retorna apenas a parte esquerda da linha
            // A parte direita é apenas comentário
            var r = str.Split('#');
            r[0] = r[0].Trim();
            return r[0];
        }

        /// <summary>
        /// Initializes rawSections with the required number of sections
        /// </summary>
        /// <param name="data">Raw data</param>
        /// <returns>A list ready to receive data separated by sections</returns>
        private static List<List<string>> PrepareRawSections(List<string> data)
        {
            List<List<string>> rawSections = new List<List<string>>();
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
            return rawSections;
        }

        /// <summary>
        /// Distribute variables to sections
        /// </summary>
        /// <param name="rawSections">Untreated sections</param>
        /// <param name="data">Untreated data</param>
        private static void DistributeSections(List<List<string>> rawSections, List<string> data)
        {
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
        }

        /// <summary>
        /// Creates sections
        /// </summary>
        /// <param name="rawSections">Separate but untreated sections</param>
        private static List<Section> CreateSections(List<List<string>> rawSections)
        {
            List<Section> sections = new List<Section>();

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
                    // Senão entrou em nenhum if anterior, significa que é uma variável e será adicionada a seção
                    // Divide pelo ':'
                    var r = s.Split(':');
                    // Cria um novo dado com key e data
                    sections[sections.Count() - 1].Data.Add(new Data(r[0], r[1]));
                }
            }
            return sections;
        }

        #endregion Private

        #endregion Methods

    }
}