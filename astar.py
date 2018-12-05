# Lựa chọn hàm đánh giá
from astar_heuristic import manhatan_with_bonus_and_optimized_cost2 as evaluate

#Khai báo biến toàn cục (các thông tin input)
size = []
start = []
destination = []
walls = []
items = []

output = open("mov.txt", "w")

# Hàm đọc dữ liệu đầu vào
def read_input(input_file):
	with open(input_file, "r") as f:
		global size, start, destination, walls, items

		#Đọc các thông tin về kích thước bản đồ, vị trí xuất phát và đích đến
		tmp = [int(x) for x in f.readline().split()]
		size = [tmp[0], tmp[1]]
		start = [tmp[2], tmp[3]]
		destination = [tmp[4], tmp[5]]

		#Đọc thông tin về tường trong mê cung
		for i in range(size[0]):
			line = f.readline()
			walls.append([int(x) for x in line.split()])
		#Đọc thông tin về vật phẩm trong mê cung
		items = []
		for i in range(size[0]):
			line = f.readline()
			items.append([int(x) for x in line.split()])
	
# Gọi hàm đọc file
input_file = "MazeGen/input.txt"
read_input(input_file)

# state thể hiện trạng thái các vật phẩm (đã ăn hay chưa).
# Mỗi phần tử trong state ứng với một hàng
# Mỗi bit ứng với một ô trong ma trận
state = [0]*size[0]

# Khởi tạo cho state
for i in range(size[0]):
	for j in range(size[1]):
		if (items[i][j] != 0):
			state[i] |= 1 << j
state[start[0]] &= ~(1 << start[1])

# Thêm thông tin về vị trí vào state
# Thêm thông tin về số điểm khởi tạo (bao gồm điểm hiện tại và điểm ước lượng)
state = (tuple(start), tuple(state),)
score = (items[start[0]][start[1]], evaluate(size, state, destination, items, walls))

# Khởi tạo hàng đợi với một phần tử là trạng thái và điểm số ban đầu
openSet = {state: score}
closedSet = {}
trace = {state: state}

# Độ lệch toạ độ với các ô hàng xóm
delta = [[-1, 0], [0, 1], [1, 0], [0, -1]]

# Khởi tạo kết quả tốt nhất đã tìm được là âm vô cùng
result_score = -(10**1000)
result_state = []

# Thực hiện giải thuật A*
while (0==0):
	# Hàng đợi rỗng
	if (len(openSet) == 0):
		break
	else:
		pass

	# Chuỗi trạng thái
	strState = 'P '

	# Lấy phần tử đầu tiên trong hàng đợi và đưa vào closedSet.
	# Nếu phần tử này có ước lượng không vượt quá giá trị tốt nhất đã tìm được, dừng thuật toán
	state_max, score_max = max(openSet.items(), key=lambda x: x[1][0]+x[1][1])
	if (result_score >= score_max[0]+score_max[1]):
		break
	del openSet[state_max]
	closedSet[state_max] = score_max

	# Thêm thông tin về toạ độ của trạng thái hiện tại
	strState += ' '.join([str(state_max[0][0]), str(state_max[0][1])]) + ' '
	strState += str(score_max[0]) + ' '

	for i in range(0,4):
		d = delta[i]

		# Convert tuple -> list để thực hiện các phép gán
		current_state_list = list(list(i) for i in state_max)
		current_score_list = list(score_max)

		# Kiểm tra có thể đi đến ô hàng xóm được không
		# Nếu có tường bỏ qua ô này
		if ((walls[state_max[0][0]][state_max[0][1]] >> i) & 1):
			strState += 'F '
			continue

		# Thử đi đến các ô hàng xóm
		# Gán toạ độ mới và convert lại thành tuple
		# Gán số điểm mới bằng số điểm ở trạng thái trước đó - 1 + giá trị vật phẩm ở ô vừa đến (nếu còn)
		# Gán lại số điểm đánh giá cho trạng thái mới
		r = state_max[0][0] + d[0]
		c = state_max[0][1] + d[1]
		current_state_list[0][0] = r
		current_state_list[0][1] = c

		# Kiểm tra ô hàng xóm có hợp lệ (nằm trong ma trận) không
		if (current_state_list[0][0] not in range (0, size[0]) or current_state_list[0][1] not in range (0, size[1])):
			strState += 'F '
			continue

		# Robot di chuyển đến ô hàng xóm, tốn một chi phí bằng 1
		# Robot nhận được số điểm bằng item ô hàng xóm đang chứa
		# Item ở ô tương ứng biến mất
		current_score_list[0] = score_max[0] - 1
		if ((current_state_list[1][r] >> c) & 1):
			current_score_list[0] += items[r][c]
			current_state_list[1][r] &= ~(1 << c)

		#Chuyển đổi biến lưu trạng thái sang tuple
		current_state = tuple(tuple(i) for i in current_state_list)

		# Đánh giá lại giá trị ước lượng
		current_score_list[1] = evaluate(size, current_state, destination, items, walls)
		strState += str(current_score_list[0] + current_score_list[1]) + ' '

		# Chuyển đổi điểm số sang tuple để add vào hàng đợi
		current_score = tuple(current_score_list)

		# Kiểm tra trạng thái hiện tại có chứa vị trí đích
		# Nếu có, ta cập nhật lại kết quả tốt nhất và lưu vết đường đi
		if (r == destination[0] and c == destination[1]):
			if (result_score < current_score[0]):
				result_score = current_score[0]
				result_state = current_state
				trace[current_state] = state_max


		# Nếu trạng thái hiện tại có trong closedSet nhưng giá trị ước lượng vừa tìm được tốt hơn thì lấy trạng thái đó khỏi closedSet và thêm lại vào openSet
		if (current_state in closedSet):
			if (current_score[0] > closedSet[current_state][0]):
				del closedSet[current_state]
				openSet[current_state] = current_score
				trace[current_state] = state_max
			else:
				pass
		# Nếu trạng thái hiện tại đã có trong closedSet
		# Gán số điểm tốt nhất cho trạng thái này
		elif (current_state in openSet):
			if (openSet[current_state][0] < current_score[0]):
				openSet[current_state] = current_score
				trace[current_state] = state_max
		# Nếu trạng thái chưa có trong cả openSet và closedSet, thêm trạng thái
		else:
			openSet[current_state] = current_score
			trace[current_state] = state_max

	for i in range(size[0]):
		for j in range(size[1]):
			strState += str(items[i][j] * ((state_max[1][i] >> j) & 1)) + ' '
	output.write(strState[:-1] + '\n')

direct = ["S ", "A ", "W ", "D "]
movement = ""
stack = [[result_state, result_score]]
while (trace[result_state] != result_state):
	result_state = trace[result_state]
	stack.append([result_state, closedSet[result_state]])

while (len(stack) > 0):
	x = len(stack)-1
	# writeout(stack[x])
	for d in range(4):
		i = delta[d]
		if ([stack[x][0][0][0] - stack[x-1][0][0][0], stack[x][0][0][1] - stack[x-1][0][0][1]] == i):
			movement = movement + direct[d]
			break
	del stack[x]

output.write ('B ' + movement[:-1])

def writeout(s):
	str = "P " + ' '.join(s[0])
	# str += 