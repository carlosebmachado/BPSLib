/*
 * MIT License
 *
 * Copyright (c) 2020 Carlos Eduardo de Borba Machado
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */


#pragma once

#include <string>
#include <vector>
#include <fstream>

namespace bps {
    const std::string ERR_OPEN_NEW_WOUT_CLOSE_PREV = "Trying to open a new section without closing the previous one.";
    const std::string ERR_CLOSE_WOUT_OPEN_PREV = "Section was closed without open it previously.";

    const std::string KV_HEADER = "# BPS File";
    const std::string KV_NEXTLINE = "\n";
    const std::string KV_LAB = "<";
    const std::string KV_RAB = ">";
    const std::string KV_TAB = "    ";
    const std::string KV_SEPARATOR = ":";

    const std::string FILENAME_EXTENSION = ".bps";

    class data {
    private:
        std::string _key;
        std::string _value;

    public:
        data(std::string key, std::string value) {
            this->_key = key;
            this->_value = value;
        }

        ~data() {
            _key.~basic_string();
            _value.~basic_string();
        }

        std::string get_key() {
            return _key;
        }

        void set_key(std::string key) {
            _key = key;
        }

        std::string get_value() {
            return _value;
        }

        void set_value(std::string value) {
            _value = value;
        }
    };

    class section {
    private:
        std::string _name;
        std::vector<data*> _data;

    public:
        section(std::string name, std::vector<data*> data) {
            _name = name;
            _data = data;
        }

        section(std::string name) {
            _name = name;
            _data = std::vector<data*>();
        }

        ~section() {
            _name.~basic_string();
            _data.~vector();
        }

        bool add(data* data) {
            if (!exists(data->get_key())) {
                _data.push_back(data);
                return true;
            }
            return false;
        }

        void RemoveAll() {
            _data.clear();
        }

        bool remove(std::string key) {
            for(rsize_t i = 0; i < _data.size(); i++) {
                if (_data[i]->get_key()._Equal(key)) {
                    // verificar se está apagando a pos certa
                    _data.erase(_data.begin() + i);
                    return true;
                }
            }
            return false;
        }

        std::vector<data*> find_all() {
            return _data;
        }

        data* find(std::string key) {
            for(data* d : _data) {
                if (d->get_key()._Equal(key)) {
                    return d;
                }
            }
            return nullptr;
        }

        bool exists(std::string key) {
            return find(key) != nullptr;
        }

        std::string get_name() {
            return _name;
        }

        void set_name(std::string name) {
            _name = name;
        }
    };

    class file {
    private:
        std::vector<section*> _sections;

    public:
        file(std::vector<section*> sections) {
            _sections = sections;
        }

        file() {
            _sections = std::vector<section*>();
        }

        ~file() {
            _sections.~vector();
        }

        bool add(section* section) {
            if (!exists(section->get_name())) {
                _sections.push_back(section);
                return true;
            }
            return false;
        }

        void RemoveAll() {
            _sections.clear();
        }

        bool remove(std::string key) {
            for (rsize_t i = 0; i < _sections.size(); i++) {
                if (_sections[i]->get_name()._Equal(key)) {
                    // verificar se está apagando a pos certa
                    _sections.erase(_sections.begin() + i);
                    return true;
                }
            }
            return false;
        }

        std::vector<section*> find_all() {
            return _sections;
        }

        section* find(std::string name) {
            for (section* s : _sections) {
                if (s->get_name()._Equal(name)) {
                    return s;
                }
            }
            return nullptr;
        }

        bool exists(std::string name) {
            return find(name) != nullptr;
        }
    };

    std::vector<std::string> split(std::string str, char sc) {
        std::vector<std::string> strings = std::vector<std::string>();
        std::string sstr = "";
        for (char c : str) {
            if (c == sc) {
                strings.push_back(sstr);
                sstr = "";
                continue;
            }
            sstr += c;
        }
        strings.push_back(sstr);
        return strings;
    }

    std::string trim(std::string str) {
        int s = 0, c = 0;
        for (size_t i = 0; i < str.size(); i++) {
            if (str[i] != ' ') {
                s = i;
                break;
            }
        }
        for (size_t i = str.size(); i > 0; i--) {
            if (str[i - 1] != ' ') {
                c = i - s;
                break;
            }
        }
        return str.substr(s, c);
    }

    std::string normalize_path(std::string path) {
        int length = path.size();
        if (length > 4) {
            if (!path.substr(length - 4, 4)._Equal(FILENAME_EXTENSION)) {
                return path + FILENAME_EXTENSION;
            } else {
                return path;
            }
        } else if (path._Equal(FILENAME_EXTENSION)) {
            return path;
        } else {
            return path + FILENAME_EXTENSION;
        }
    }

    std::string remove_comments(std::string str)
    {
        std::vector<std::string> r = split(str, '#');
        r[0] = trim(r[0]);
        return r[0];
    }

    file* read(std::string path) {
        std::vector<std::string> raw_data = std::vector<std::string>();
        std::vector<std::vector<std::string>> raw_sections = std::vector<std::vector<std::string>>();
        std::vector<section*> sections = std::vector<section*>();
        bool open = false;

        std::string line;

        std::ifstream f;
        f.open(normalize_path(path));

        while (!f.eof()) {
            getline(f, line);

            // Ignora as linhas vazias
            if (line._Equal(""))
                continue;
            // Remove espaços em branco
            if (line.at(0) == ' ')
                line = trim(line);
            // Remove comentários
            line = remove_comments(line);
            // Caso houvesse linhas com apenas comentários, seria removida
            if (line._Equal(""))
                continue;
            // Remove espaços em branco
            if (line.at(0) == ' ')
                line = trim(line);
            // Caso ainda haja comentários, termina de remover
            if (line.at(0) == '#')
                continue;
            // Caso ainda sobre alguma linha vazia
            line = trim(line);
            if (line._Equal(""))
                continue;
            if (line.at(0) == '\t')
                if (line.size() > 1) {
                    line = line.substr(1, line.size() - 1);
                } else {
                    continue;
                }
            // Se a linha passou por todas as verificações é adicionada às linhas válidas
            raw_data.push_back(line);

        }
        f.close();

        // Procura nos dados brutos, as seções
        for(std::string d : raw_data) {
            // Encontrou a tag de abrir seção
            if (d.at(0) == '<') {
                // Se encontra uma tag de abrir seção mas ela já foi aberta
                if (open) {
                    return nullptr;
                } else {
                    open = true;
                }
            }
            // Encontrou a tag de fechar seção
            if (d._Equal(">")) {
                // Se encontra uma tag de fechar seção mas ela não foi aberta
                if (!open) {
                    return nullptr;
                } else {
                    raw_sections.push_back(std::vector<std::string>());
                    open = false;
                }
            }

        }

        // Loop para cada seção encontrada anteriormente
        for (int i = 0; i < raw_sections.size(); i++) {
            // Passa todas as linhas cruas para o rawSection
            while (true) {
                std::string curLine = raw_data[0];
                raw_data.erase(raw_data.begin());
                raw_sections[i].push_back(curLine);
                if (curLine._Equal(">")) break;
            }
        }

        // Percorre rawSections criando as variáveis
        for(std::vector<std::string> raw_section : raw_sections) {
            // Remove qualquer linha que não começe com '<'
            // PROVAVELMENTE SERÁ REMOVIDA
            while (!raw_section[0].at(0) == '<') {
                raw_section.erase(raw_section.begin());
            }
            for(std::string rs_lines : raw_section) {
                // Se estiver abrindo a seção
                if (rs_lines.at(0) == '<') {
                    std::string ns = rs_lines.substr(1, rs_lines.size() - 1);
                    sections.push_back(new section(ns));
                    //sections[sections.Count() - 1].Name = newS;
                    continue;
                }
                // Encontrou o fim da seção
                if (rs_lines._Equal(">")) {
                    break;
                }
                // Senão entrou em nenhum if anterior, significa que é uma key/value e será adicionada a seção
                // Divide pelo ':'
                std::vector<std::string> r = split(rs_lines, ':');
                // Cria um novo dado com key e data
                sections[sections.size() - 1]->add(new data(r[0], r[1]));
            }
        }

        return new file(sections);
    }

    void write(file* file, std::string path) {
        std::ofstream wf;
        wf.open(normalize_path(path));

        wf << KV_HEADER + KV_NEXTLINE + KV_NEXTLINE;

        for(section* s : file->find_all())
        {
            wf << KV_LAB + s->get_name() + KV_NEXTLINE;
            for(data* d : s->find_all())
            {
                wf << KV_TAB + d->get_key() + KV_SEPARATOR + d->get_value() + KV_NEXTLINE;
            }
            wf << KV_RAB + KV_NEXTLINE + KV_NEXTLINE;
        }
        wf.close();
    }
}
