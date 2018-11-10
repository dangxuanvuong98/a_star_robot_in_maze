

def manhatan(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	return -abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

def manhatan_with_all_bonus(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1))
	return bonus_amount - abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

def manhatan_with_bonus_and_cost(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1) - abs(i - state[0][0]) + abs(j - state[0][1]))
	return bonus_amount - abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

def manhatan_with_bonus_and_optimized_cost(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	tmp_items = [[2**100] * size[0]] * size[1]
	for i in range(size[0]):
		for j in range(size[1]):
			pass
	return abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1]) + bonus_amount
	pass
