
# Hàm đánh giá sử dụng khoảng cách Manhatan
def manhatan(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	distance = abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1])
	return distance
	pass

# Hàm đánh giá sử dụng khoảng cách Manhatan và tổng số các bonus dương còn lại hiện tại
def manhatan_with_all_bonus(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1))
	distance = abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1])
	return bonus_amount - distance
	pass

# Hàm đánh giá sử dụng khoảng cách Manhatan, tổng số các bonus dương hiện tại cùng chi phí để ăn được các bonus đó
def manhatan_with_bonus_and_cost(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	for i in range(size[0]):
		for j in range(size[1]):
			bonus_amount += max(0, items[i][j] * ((state[1][i] >> j) & 1) - abs(i - state[0][0]) - abs(j - state[0][1]))
	distance = abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1])
	return bonus_amount - distance
	pass

# Tối ưu chi phí để ăn các bonus, đảm bảo giá trị ước lượng luôn >= giá trị thực tế
def manhatan_with_bonus_and_optimized_cost(size, state, destination, items, walls):
	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
		return 0
	bonus_amount = 0
	tmp_items = []
	for i in range(size[0]):
		for j in range(size[1]):
			if items[i][j] != 0 and state[1][i] & (1 << j):
				tmp_items.append([i, j, items[i][j]])
	mind = [0]*len(tmp_items)
	distance = abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1])
	for i in range(len(tmp_items)):
		distance = min(distance, abs(destination[0] - tmp_items[i][0]) + abs(destination[1] - tmp_items[i][1]))
		mind[i] = abs(tmp_items[i][0] - state[0][0]) + abs(tmp_items[i][1] - state[0][1])
		for j in range(len(tmp_items)):
			if i != j:
				mind[i] = min(mind[i], abs(tmp_items[i][0] - tmp_items[j][0]) + abs(tmp_items[i][1] - tmp_items[j][1]))
		bonus_amount += max(tmp_items[i][2] - mind[i], 0)
	return bonus_amount - distance
	pass

# Tương tự như phương pháp trên nhưng tối ưu hoá chi phí đánh giá (độ phức tạp của hàm đánh giá)
# def manhatan_with_bonus_and_optimized_cost2(size, state, destination, items, walls):
# 	if (state[0][0] == destination[0] and state[0][1] == destination[1]):
# 		return 0
# 	bonus_amount = 0
# 	queue = [state[0]]
# 	itemlevel = [0]
# 	layers = {0: (0, 0, 0, 0)}		#sum, cost, min, count
# 	last_layer = 0
# 	first = 0
# 	last = 0
# 	delta = [[-1, 0], [0, 1], [1, 0], [0, -1]]
# 	while first <= last:
# 		pos = queue[first]
# 		for i in range(4):
# 			d = delta[i]
# 			r = pos[0] + d[0]
# 			c = pos[1] + d[1]
# 			if (r < 0 or c < 0 or r >= size[0] or c >= size[1] or (r, c) in queue):
# 				continue
# 			if ((walls[r][c] >> i) & 1):
# 				continue
# 			last += 1
# 			queue.append((r, c))
# 			this_layer = itemlevel[first] + 1
# 			itemlevel.append(this_layer)
# 			if (state[1][r] & (1 << c) and items[r][c] > 0):
# 				if this_layer in layers:
# 					new_value = (layers[this_layer][0] + items[r][c],
# 						layers[this_layer][1],
# 						min(layers[this_layer][2], items[r][c]),
# 						layers[this_layer][3] + 1)
# 					layers[this_layer] = new_value
# 				else:
# 					layers[this_layer] = (items[r][c], this_layer-last_layer, items[r][c], 1)
# 					last_layer = this_layer
# 		first += 1
# 	for i in layers:
# 		bonus_amount += layers[i][0] - layers[i][3] - layers[i][2] + max(0, layers[i][2] - layers[i][1])
# 	distance = abs(destination[0] - state[0][0]) + abs(destination[1] - state[0][1])
# 	for i in range(size[0]):
# 		for j in range(size[1]):
# 			if ((walls[r][c] >> i) & 1): 
# 				distance = min(distance, abs(destination[0] - i) + abs(destination[1] - j))
# 	return bonus_amount - distance
# 	pass