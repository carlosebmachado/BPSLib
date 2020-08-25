#include <iostream>
#include "bps.h"

std::string path = "D:/Documentos/OneDrive/DESKTOP/BPSLib/";
//std::string path = "C:/Users/Panificadora Larissa/OneDrive/DESKTOP/BPSLib/";
std::string wf = "write_test";
std::string rf = "read_test.bps";

void rw_test();
void trim_test();
void split_test();
void section_tests();

int main() {
	rw_test();
	//trim_test();
	//split_test();
	//section_tests();

}

void rw_test() {
	bps::file* file = bps::read(path + rf);

	for(bps::section* s : file->find_all()) {
		std::cout << s->get_name() << std::endl;
		for(bps::data* d : s->find_all()) {
			std::cout << d->get_key() + ":" + d->get_value() << std::endl;
		}
		std::cout << std::endl;
	}

	bps::write(file, path + wf);
}

void trim_test() {
	std::cout << "<" + bps::trim(" frase aqui  ") + ">\n";
	std::cout << "<" + bps::trim(" palavra  ") + ">\n";
	std::cout << "<" + bps::trim("     i  ") + ">\n";
	std::cout << "<" + bps::trim("    ") + ">\n";
	std::cout << "<" + bps::trim(" j         ") + ">\n";
	std::cout << "<" + bps::trim("") + ">\n";
}

void split_test() {
	std::string str = "comi:pao:casa:joao";
	std::vector<std::string> strings = bps::split(str, ':');
	for (size_t i = 0; i < strings.size(); i++) {
		std::cout << strings[i] + "\n";
	}
}

void section_tests() {
	bps::section section = bps::section("section");
	section.add(new bps::data("key", "value"));
	section.add(new bps::data("key2", "value2"));
	section.add(new bps::data("key3", "value3"));
	section.add(new bps::data("key4", "value4"));

	bps::data* dt1 = section.find("key5");
	if (dt1 != nullptr) {
		std::cout << dt1->get_key() + ":" + dt1->get_value() + "\n";
	}

	bps::data* dt2 = section.find("key");
	if (dt2 != nullptr) {
		std::cout << dt2->get_key() + ":" + dt2->get_value() + "\n";
	}

	std::cout << "\n";

	if (section.exists("key"))
		std::cout << "key? true\n";
	else
		std::cout << "key? false\n";

	if (section.exists("key5"))
		std::cout << "key5? true\n";
	else
		std::cout << "key5? false\n";

	std::cout << "\n";

	std::cout << section.get_name() + "\n";
	for (bps::data* d : section.find_all()) {
		std::cout << d->get_key() + ":" + d->get_value() + "\n";
	}
}
