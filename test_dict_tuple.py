def read_input():
	return [1, 2, 3], [4, 5, 6];


closedSet = []
openSet = []

closedSet.append(("Vuong", 22, 'a'))

print(closedSet[0][0], closedSet[0][1], closedSet[0][2])
s = "-"
s = s.join(str(x) for x in closedSet[0])
print(s)

array1, array2 = read_input();
print array1[0], array2[1], array1[2]
