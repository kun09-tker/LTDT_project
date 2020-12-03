#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
const int MAX = 100;


bool luuVet[MAX];
int tmp[MAX];
int k = 0;
int viTri = 0;
int vitricu = 0;
int MIN = 1000000;
using namespace std;

struct Graph {
	int soDinh;
	int arr[MAX][MAX];
};



void readGraph(string path, Graph &g) {
	ifstream file;
	file.open(path);

	if (file.is_open()) {
		file >> g.soDinh;
		for (int i = 0; i < g.soDinh; i++) for (int j = 0; j < g.soDinh; j++) file >> g.arr[i][j];
		file.close();
	}
	else {
		cout << "Mo file that bai";
		exit(0);
	}
}



void printGraph(Graph g) {
	cout << "So dinh cua do thi la: " << g.soDinh << endl;

	for (int i = 1; i <= g.soDinh; i++) {
		for (int j = 1; j <= g.soDinh; j++) cout << setw(4) << g.arr[i][j] << " ";
		cout << endl;
	}
}

bool ktHopLe(Graph g) {
	for (int i = 1; i <= g.soDinh; i++)
		if (g.arr[i][i] != 0) return 0;
	return 1;
}

bool loaiDoThi(Graph g) {
	for (int i = 1; i <= g.soDinh; i++) {
		for (int j = 1; j <= g.soDinh; j++)
			if (g.arr[i][j] != g.arr[j][i]) return 1;
	}
	return 0;
}

int bacCuaDinh(int dinh, Graph g, int kieu) {
	int bac = 0;

	if (kieu == 0) for (int i = 1; i <= g.soDinh; i++) bac = bac + g.arr[dinh][i];
	else for (int i = 1; i <= g.soDinh; i++) bac = bac + g.arr[dinh][i] + g.arr[i][dinh];

	return bac;
}

int soCanh(Graph g) {
	int canh = 0;

	for (int i = 1; i <= g.soDinh; i++) canh = canh + bacCuaDinh(i, g, loaiDoThi(g));

	return canh / 2;
}

int main() {
	Graph g;
	string file = "D:./graph.txt";


	readGraph(file, g);

	char ten[MAX];
	for (int i = 0; i < g.soDinh; i++) ten[i] = '-';

	char c = 'a';
	for (int i = 1; i <= g.soDinh; i++) {
		cout << c << "    ";
		c++;
	}
	cout << "\n0* ";
	tmp[viTri] = -1;
	for (int i = 1; i < g.soDinh; i++) {
		cout << "oo" << "   "; 
		tmp[i] = 100000000;

	}
	cout << endl;
	c = 'a';
	for (int i = 0; i < g.soDinh - 1; i++) {
		for (int j = 0; j < g.soDinh; j++) {
			if (tmp[j] == -1) {
				cout << "- ";
			}
			else {
				if (g.arr[viTri][j] != 0 && g.arr[viTri][j] + k < tmp[j])
				{
					ten[j] = char(c + (viTri));
					cout << "(" << g.arr[viTri][j] + k << "," << ten[j] << ") ";
					tmp[j] = g.arr[viTri][j] + k;
				}
				else
				{
					if (tmp[j] == 100000000) {
						cout << "oo ";
					}
					else {
						cout << "(" << tmp[j] << "," << ten[j] << ") ";
					}
				}
			}
		}
		vitricu = viTri;
		for (int j = 0; j < g.soDinh; j++) {
			if (tmp[j] < MIN && tmp[j] != -1) {
				MIN = tmp[j];
				viTri = j;
			}
		}
		tmp[viTri] = -1;
		k = MIN;
		MIN = 100000000;
		c = 'a';
		cout << endl;
	}
	for (int i = 0; i < g.soDinh; i++) cout << "- ";

	cout << endl;

	//cin.get();
}