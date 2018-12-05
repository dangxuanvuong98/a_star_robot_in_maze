#include <bits/stdc++.h>

using namespace std;

const int N = 105;

int dx[] = {-1, 0, 1, 0};
int dy[] = {0, 1, 0, -1};
int wp[] = {1, 2, 4, 8};

int n, m, q;
int startX, startY, disX, disY, finishX, finishY;
int mz[N][N], mk[N][N], mzv[N][N];

int get_rd(int pos, int lim)
{
  int temp, sign;
  do{
    temp = rand() % lim/2 + lim/2;
    sign = rand() % 2;
    temp *= (sign ? 1 : -1);
  }while(pos + temp > lim || pos + temp < 1);
  return temp;
}

void gen_maze(int px, int py)
{
  do{
    int dir = rand() % 4;
    if((mk[px][py] & (1<<dir)) == 0) continue;
    if(px + dx[dir] < 1 || px + dx[dir] > n ||
       py + dy[dir] < 1 || py + dy[dir] > m) {
      mk[px][py] &= (15-(1<<dir));
      continue;
    }
    int npx = px + dx[dir];
    int npy = py + dy[dir];
    if(npx == finishX && npy == finishY) {
      mz[px][py] &= (15-wp[dir]);
      mz[npx][npy] &= (15-wp[(dir+2)%4]);
      mk[px][py] &= (15-(1<<dir));
      mk[npx][npy] &= (15-(1<<((dir+2)%4)));
      gen_maze(npx, npy);
    }
    int flag = 0;
    for(int i = 0; i < 4; i++){
      if(i != ((dir+2)%4)){
        if((mz[npx][npy] & wp[i]) == 0) {
          flag = 1;
          mk[px][py] &= (15-(1<<dir));
        }
      }
    }
    if(!flag){
      mz[px][py] &= (15-wp[dir]);
      mz[npx][npy] &= (15-wp[(dir+2)%4]);
      mk[px][py] &= (15-(1<<dir));
      mk[npx][npy] &= (15-(1<<((dir+2)%4)));
      gen_maze(npx, npy);
    }
  }while(mk[px][py]);
}

int main()
{
  freopen("input.txt", "w", stdout);
  //freopen("input.txt", "r", stdin);
  srand(time(0));
  //n = rand() % 10 + 5;
  //m = rand() % 10 + 5;
  cin >> n >> m;
  cin >> q;
  startX = rand() % n + 1;
  startY = rand() % m + 1;
  disX = get_rd(startX, n);
  disY = get_rd(startY, m);
  finishX = startX + disX;
  finishY = startY + disY;
  for(int i = 1; i <= n; i++)
    for(int j = 1; j <= m; j++) mz[i][j] = mk[i][j] = 15;
  gen_maze(startX, startY);
  int cc = 0;
  do{
    int xx = rand() % n + 1;
    int yy = rand() % m + 1;
    if((xx == startX && yy == startY)  ||
       (xx == finishX && yy == finishY)||
       (mzv[xx][yy] != 0)) continue;
    cc++;
    int value = rand() % (int)(sqrt(n*m)) + 1;
    int sign = rand() % 2;
    if(!sign) value /= 2;
    mzv[xx][yy] = value * (sign ? 1:-1);
  }while(cc < q);
  for(int i = 1; i <= (n*m)/30; i++){
    int xx = rand() % n + 1;
    int yy = rand() % m + 1;
    if(xx == 1) xx++;
    if(xx == n) xx--;
    if(yy == 1) yy++;
    if(yy == m) yy--;
    for(int j = 0; j < 4; j++){
      int nx = xx + dx[j];
      int ny = yy + dy[j];
      int nj = (j+2)%4;
      if((mz[xx][yy] & (1<<j)) && (mz[nx][ny] & (1<<nj))){
        mz[xx][yy] -= (1<<j);
        mz[nx][ny] -= (1<<nj);
      }
    }
  }
  cout << n << ' ' << m << ' ';
  cout << startX-1 << ' ' << startY-1 << ' ';
  cout << finishX-1 << ' ' << finishY-1 << endl;
  for(int i = 1; i <= n; i++){
    for(int j = 1; j <= m; j++) {
      cout << mz[i][j];
      if(j != m) cout << ' ';
    }
    cout << endl;
  }
  for(int i = 1; i <= n; i++){
    for(int j = 1; j <= m; j++){
      cout << mzv[i][j];
      if(j != m) cout << ' ';
    }
    if(i != n) cout << endl;
  }
  return 0;
}
