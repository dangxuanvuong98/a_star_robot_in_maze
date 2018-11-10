#include <stdio.h>

struct Node
{
	int f, g, h, color, wall;
	
	Node()
	{
		this->f = this->g = this->h = 0;
	}
} node[100][100];

struct Pair
{
	int r, c;
} start, dest;

struct HeapofNode
{
	int size;
	Node HoN[10000];
	
	
} heap;

const struct Pair neighbor[4] = {{0, -1}, {-1, 0}, {0, 1}, {1, 0}};

int height, width;

void ReadInput()
{
	FILE *f = fopen("input.txt", "r");
	fscanf(f, "%d%d", &height, &width);
	int tmp;
	for (int i = 0; i < height; i++)
		for (int j = 0; j < width; i++)
		{
			fscanf(f, "%d", tmp);
			node[i][j].wall |= tmp << 2;
			node[i+1][j].wall |= (tmp & 1);
			node[i+1][j].wall |= (tmp & 2);
		}
	fscanf(f, "%d%d%d%d", &(start.r), &(start.c), &(dest.r), &(dest.c));
	fclose(f);
}

void WriteOutput()
{
	FILE *f = fopen("output.txt", "w");
	
	fclose(f);
}

void BFS()
{
	
}

int main()
{
	ReadInput();
	BFS();
	WriteOutput();
	return 0;
