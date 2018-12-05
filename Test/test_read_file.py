def read_input(input_file):
	f = open(input_file, "r")
	int_array = [int(x) for x in next(f).split()]
	

closedSet = []
openSet = []

input_file = "input.txt"
read_input(input_file)
#print r