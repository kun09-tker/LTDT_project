#include <bits/stdc++.h>
using namespace std;
#define MAX 100
struct duongDi {
	int begin; // đỉnh đầu 
	int end; // đỉnh cuối 
	int w; //trọng số
};
struct GRAPH {
	int sodinh;
	int a[MAX][MAX];
};
void nhapMaTranKe(string duongdan, GRAPH& dt) {
	ifstream inp;
	inp.open(duongdan);
	if (inp.is_open()) {
		inp >> dt.sodinh;
		for (int i = 1; i <= dt.sodinh; i++) {
			for (int j = 1; j <= dt.sodinh; j++) {
				inp >> dt.a[i][j];
			}
		}
		inp.close();
	}
}
/// <summary>
/// Kiểm tra xem đỉnh có sử dụng được không 
/// </summary>
/// <param name="vA">: Mảng chứa đỉnh xét</param>
/// <param name="x">: Đỉnh cần kiểm tra</param>
/// <param name="soDinh">: số lượng đỉnh</param>
/// <returns></returns>
bool checkAvailable(bool vA[], int x, int soDinh) {
	for (int i = 1; i <= soDinh; i++) {
		if (x == i) {
			return vA[i];
		}
	}
}
/// <summary>
/// Trọng số nhỏ nhất đến đỉnh cần xét 
/// </summary>
/// <param name="dinhB">: Đỉnh cần xét</param>
/// <param name="dd">: Đường đi</param>
/// <param name="ndd">: Số lượng đường đi</param>
/// <param name="soLuongDinh">: Số lượng đỉnh của ma trận</param>
duongDi layTrongSo(int dinhB, duongDi dd[], int ndd, bool vA[], int soLuongDinh, duongDi linePre[], int z) {
	duongDi tt[1000]; // mảng chứa các đường chỉ đi đến dinhB 
	int ntt = 0;
	for (int i = 1; i <= ndd; i++) {
		if (dd[i].end == dinhB && checkAvailable(vA, dd[i].begin, soLuongDinh)) {
			tt[ntt].begin = dd[i].begin;
			tt[ntt].end = dd[i].end;
			tt[ntt].w = dd[i].w;
			ntt++;
		}
	}
	duongDi min = tt[0];
	//sort(tt, tt + ntt);
	for (int i = 0; i < ntt; i++) {
		for (int j = 0; j < ntt; j++) {
			if (tt[i].w < tt[j].w) swap(tt[i], tt[j]);
		}
	}
	if (ntt != 0) {
		for (int i = 0; i < ntt; i++) {
			tt[i].w += linePre[tt[i].begin].w;
		}
		//sort(tt, tt + ntt);
		for (int i = 0; i < ntt; i++) {
			for (int j = 0; j < ntt; j++) {
				if (tt[i].w < tt[j].w) swap(tt[i], tt[j]);
			}
		}
		tt[0].w -= linePre[tt[0].begin].w;
		return tt[0];
	}
	duongDi ddNULL;
	ddNULL.begin = 0;
	ddNULL.w = 0;
	return ddNULL;
}
bool giongNhau(duongDi lineNow[], duongDi linePre[], int soDinh) {
	for (int i = 0; i < soDinh; i++) {
		if (lineNow[i].w != linePre[i].w) return false;
	}
	return true;
}
void changeVertexStart(GRAPH& mt, int start) {
	duongDi dd[1000];
	int ndd = 0;
	// tìm đường đi giữa các đỉnh và trọng số 
	for (int i = 1; i <= mt.sodinh; i++) {
		for (int j = 1; j <= mt.sodinh; j++) {
			ndd++;
			dd[ndd].begin = i;
			dd[ndd].end = j;
			dd[ndd].w = mt.a[i][j];
		}
	}
	//Swap đỉnh 
	int dinh[100];
	dinh[1] = start;
	int tt = 2;
	for (int i = 1; i <= mt.sodinh; i++) {
		if (i != start) {
			dinh[tt] = i;
			tt++;
		}
	}
	for (int i = 1; i <= mt.sodinh; i++) {
		for (int j = 1; j <= mt.sodinh; j++) {
			for (int l = 1; l <= ndd; l++) {
				if (dinh[i] == dd[l].begin && dinh[j] == dd[l].end)
					mt.a[i][j] = dd[l].w;
			}
		}

	}
}
int main() {
	GRAPH mt, change;
	nhapMaTranKe("Text.txt", mt);
	int start;
	while (cin >> start) {
		if (start <= mt.sodinh && start > 0) {
			break;
		}
	}
	changeVertexStart(mt, start);
	duongDi dd[1000]; // ma trận chứa đường đi từ đỉnh x đến xx 
	int ndd = 0; // số lượng đường đi 
	// tìm đường đi giữa các đỉnh và trọng số 
	for (int i = 1; i <= mt.sodinh; i++) {
		for (int j = 1; j <= mt.sodinh; j++) {
			if (mt.a[i][j] != 9999) { //nếu trọng số khác 0 thì đẩy vào đường đi 
				ndd++;
				dd[ndd].begin = i;
				dd[ndd].end = j;
				dd[ndd].w = mt.a[i][j];
			}
		}
	}
	duongDi lineNow[100], linePre[100];
	bool vertexAvailable[100]; //kiểm tra xem đỉnh có xét được hay không 
	bool vertexAvailable1[100]; //kiểm tra xem đỉnh có xét được hay không 
	int nlN = mt.sodinh, nlP = mt.sodinh; // số lượng kí tự 2 mảng 
	// phần tử đầu là 0 còn lại là vô cực 
	for (int i = 1; i <= nlP; i++) {
		if (i == 1) linePre[i].w = 0;
		else linePre[i].w = 9999;
	}
	lineNow[1].w = 0;
	// đỉnh đầu sử dụng được 
	for (int i = 1; i <= mt.sodinh; i++) {
		if (i == 1)vertexAvailable[i] = true;
		else vertexAvailable[i] = false;
	}
	for (int i = 1; i <= mt.sodinh; i++) {
		if (i == 1)vertexAvailable1[i] = true;
		else vertexAvailable1[i] = false;
	}
	bool stop = false;
	int k = -1;
	//dòng 1 
	cout << "k    ";
	for (int i = 1; i <= mt.sodinh; i++) {
		cout << i << "    ";
	}
	cout << "\n";


	while (!stop) {
		if (k == mt.sodinh) {

			break;
		}
		k++;
		cout << k << "   ";
		if (k == 2) {
			int a = 0;
		}
		//dòng 2
		if (k == 0) {
			cout << " 0   ";
			for (int i = 2; i <= mt.sodinh; i++) {
				cout << "oo   ";
			}
			cout << endl;
		}
		else {
			cout << " 0   ";
			for (int i = 2; i <= mt.sodinh; i++) {
				if (k == 6 && i == 2) {
					int z = 9;
				}
				duongDi minStreet = layTrongSo(i, dd, ndd, vertexAvailable, mt.sodinh, linePre, i);
				//trước đó là vô cực và có đường đi đến nó 
				if (linePre[i].w == 9999 && minStreet.w != 0) {
					vertexAvailable1[i] = true;
					lineNow[i].w = linePre[minStreet.begin].w + minStreet.w;
					lineNow[i].begin = minStreet.begin;
					lineNow[i].end = i;
					cout << lineNow[i].w << "," << lineNow[i].begin << "   ";
				}
				//trước đó là vô cực và không có đường đi đến nó 
				else if (linePre[i].w == 9999 && minStreet.w == 0) {
					lineNow[i] = linePre[i];
					cout << "oo   ";
				}
				//trước đó có tồn tại số và đường đi đến nó nhỏ hơn lúc trước
				else if (linePre[i].w != 9999 && linePre[i].w != 9999 && linePre[minStreet.begin].w + minStreet.w <= linePre[i].w) {
					vertexAvailable1[i] = true;
					lineNow[i].w = linePre[minStreet.begin].w + minStreet.w;
					lineNow[i].begin = minStreet.begin;
					cout << lineNow[i].w << "," << lineNow[i].begin << "   ";
				}
				//trước đó có tồn tại số và đường đi đến nó lớn hơn lúc trước
				else if (linePre[i].w != 9999 && linePre[i].w != 9999 && linePre[minStreet.begin].w + minStreet.w > linePre[i].w) {
					//lineNow[i] = linePre[i];
					lineNow[i].begin = linePre[i].begin;
					lineNow[i].end = linePre[i].end;
					lineNow[i].w = linePre[i].w;
					cout << lineNow[i].w << "," << lineNow[i].begin << "   ";
				}
				//không tồn tại đỉnh nào có thể đi đến nó
				else if (minStreet.w == 0) {
					//lineNow[i] = linePre[i];
					lineNow[i].begin = linePre[i].begin;
					lineNow[i].end = linePre[i].end;
					lineNow[i].w = linePre[i].w;
					cout << "oo   ";
				}
			}
			if (giongNhau(lineNow, linePre, mt.sodinh)) {
				break;
			}
			for (int i = 2; i <= mt.sodinh; i++) {
				//linePre[i] = lineNow[i];
				linePre[i].begin = lineNow[i].begin;
				linePre[i].end = lineNow[i].end;
				linePre[i].w = lineNow[i].w;
			}
			for (int i = 1; i <= mt.sodinh; i++) {
				vertexAvailable[i] = vertexAvailable1[i];
			}
			cout << endl;
		}
	}
}