
# Hàm đánh giá sử dụng khoảng cách Manhatan
def manhatan(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	return -abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

# Hàm đánh giá sử dụng khoảng cách Manhatan và tổng số bonus còn lại hiện tại
def manhatan_with_all_bonus(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1))
	return bonus_amount - abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

# Hàm đánh giá sử dụng khoảng cách Manhatan, tổng số bonus hiện tại cùng chi phí để ăn được các bonus đó
def manhatan_with_bonus_and_cost(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1) - abs(i - state[0][0]) - abs(j - state[0][1]))
	return bonus_amount - abs(destination[0] - state[0][0]) - abs(destination[1] - state[0][1])
	pass

# Tương tự như phương pháp trên nhưng tối ưu chi phí để ăn các bonus
def manhatan_with_bonus_and_optimized_cost1(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	tmp_items = [[2**100] * size[0]] * size[1]
	for i in range(size[0]):
		for j in range(size[1]):
			pass
	return abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1]) + bonus_amount
	pass

# Tương tự như phương pháp trên nhưng tối ưu hoá chi phí đánh giá (độ phức tạp của hàm đánh giá)
def manhatan_with_bonus_and_optimized_cost2(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	queue = [state[0]]
	layer = [0]
	first = 0
	last = 0
	delta = [[-1, 0], [0, 1], [1, 0], [0, -1]]
	lastlayer = 0
	while first <= last:
		pos = queue[first]
		first += 1
		for i in range(4):
			d = delta[i]
			r = pos[0] + d[0]
			c = pos[1] + d[1]
			if (r < 0 or c < 0 or r >= size[0] or c >= size[1] or (r, c) in queue):
				continue
			if ((walls[r][c] >> i) & 1):
				continue
			last += 1
			queue.append((r, c))
			layer.append(layer[first-1] + 1)
			if (state[1][r] & (1 << c)):
				cost = lastlayer - layer[last]
				lastlayer = layer[last]
				if (items[r][c] + cost > 0):
					bonus_amount += items[r][c] + cost
	return abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1]) + bonus_amount
	pass