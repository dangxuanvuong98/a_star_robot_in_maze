# a_star_robot_in_maze

# Tổng quan về hệ thống
-	Hệ thống của nhóm chúng em bao gồm hai phần: phần một dùng để xử lý bài toán được viết bằng Python và phần hai dùng để hiển thị kết quả cũng như tạo dữ liệu đầu vào được viết bằng C# sử dụng framework Unity, phần 3 là một file sinh ngẫu nhiên ma trận để kiểm thử bằng C++.
-	Hai phần tương tác với nhau bằng hai file text: 
o	input.txt: nhập dữ liệu đầu vào, bao gồm hình dạng mê cung cũng như giá trị các điểm thưởng và phạt
o	movement.txt: dùng để hiện thị kết quả, biểu thị đường đi.

# Cài đặt giải thuật
Để cài đặt chương trình giải bài toán robot tìm đường trong ma trận, em sử dụng ngôn ngữ Python.
Mã nguồn được đặt trong hai file astar.py và file astar_heuristic.py. Trong đó, file astar.py chứa phần chương trình chính, bao gồm quá trình đọc dữ liệu đầu vào, thực thi giải thuật A*, gọi hàm đánh giá, hiển thị kết quả,... File astar_heuristic.py chứa các hàm đánh giá khác nhau, tương ứng với các phương pháp em trình bày phía trên.
Để thực hiện chương trình, đầu tiên, cần chuẩn bị file input.txt nằm bên trong thư mục MazeGen. File input chứa thông tin đầu vào với định dạng như sau:
-	Dòng đầu tiên chứa 6 số nguyên thể hiện: số hàng M, số cột của ma trận N, toạ độ hàng, cột vị trí xuất phát, toạ độ hàng, cột vị trí đích (đánh số từ 0)
-	M dòng tiếp theo, mỗi dòng chứa N số nguyên. Phần tử thứ j trên hàng i (đánh số từ 0) biểu diễn tường bao xung quanh ô (i, j). Trong đó, nếu bit 0, 1, 2, 3 được bật sẽ lần lượt tương ứng với vị trí phía trên, bên phải, phía dưới, bên trái của ô (i, j) có tường. Các viền bao luôn luôn có tường.
-	M dòng cuối cùng, mỗi dòng có N số nguyên thể hiện giá trị các vật phẩm có trong mê cung
Chạy chương trình bằng cách gọi lệnh “python astar.py” trong thư mục chứa file astar.py. Kết quả in ra màn hình bao gồm số điểm mà robot ghi được, thời gian thực hiện chương trình và kích thước không gian trạng thái. Ngoài ra, các thông tin về quá trình tìm kiếm của robot được ghi trong file MazeGen\movement.txt. Ngoài ra, ta có thể thay đổi tên hàm đánh giá được import vào tại dòng thứ 4 trong file astar.py để sử dụng một phương pháp đánh giá khác (đã được định nghĩa trong file astar_heuristic.py).
Cấu trúc file movement.txt:
-	T dòng đầu – tương ứng với T trạng thái đã được xét. Mỗi dòng bắt đầu bằng ký tự P, theo sau là 2 số thể hiện toạ độ của robot. Số tiếp theo là điểm của robot khi ở trạng thái đó. 4 số tiếp theo thể hiện giá trị ước lượng đối với 4 ô xung quanh (theo thứ tự phía trên, bên phải, phía dưới, bên trái) vị trí hiện tại của robot , nếu không thể đến ô tương ứng từ vị trí hiện tại, giá trị ước lượng bằng F. MxN số cuối cùng thể hiện trạng thái các vật phẩm còn lại trên ma trận
-	Dòng cuối cùng bắt đầu bằng chữ B. Theo sau là các ký tự W, A, S, D phân tách nhau bởi dấu cách, biểu diễn đường đi tốt nhất mà robot tìm được.
-	Để chọn các phương pháp khác nhau, mở file astar.py, thay đổi phần import ở phía trên cùng của file đó. Chúng em để các phương pháp ứng với các comment, chỉ cần xoá dấu comment ở phương pháp thích hợp.
File input.txt có thể được sinh ngẫu nhiên thông qua chương trình mazeGen.
Để demo chương trình trên giao diện đồ hoạ, cần cả 2 file input.txt và movement.txt.

# Ứng dụng mô phỏng kết quả của chúng em được đặt trong thư mục Visualizer (file Project AI Visualize.exe)

-	Khi vào chương trình, chúng ta sẽ đến giao diện chính với các tính năng như sau:
	Start: bắt đầu quá trình biểu diễn chạy thử.
	Create: bắt đầu quá trình sinh map bằng đồ họa, được dùng để sinh map theo ý muốn.
	Setting: Cài đặt đường dẫn cho 2 file input.txt và movement.txt.
	Exit: Thoát chương trình
-	Đầu tiên chúng ta sẽ cài đặt đường dẫn cho 2 file đầu vào là input.txt và movement.txt. Để thiết lập hai đường dẫn, chúng ta chỉ cần copy đường dẫn đến folder chứa hai file đầu vào trên (Ví dụ ở đây là E:\BTL AI), sau đó sẽ dán vào 2 ô trống. Phần mềm sẽ tự tìm hai file đó và điền thêm phần \input.txt hay \movement.txt vào.
-	Sau khi setting xong, ấn Back để quay lại màn hình Main Menu và từ lần sau sẽ không cần thiết lập lại đường dẫn.
-	Vào Start để bắt đầu quá trình chạy thử
	+	Space Bar: Chạy theo chế độ Step-by-step
	+	M: ấn vào đây để vào chế độ chạy thử. Ấn space thêm lần nữa để robot bắt đầu chạy
	+	Sau khi chạy thử xong, ấn R để quay lại trạng thái ban đầu.

# Hệ thống sinh ngẫu nhiên ma trận
Hệ thống sinh ngẫu nhiên ma trận có khả năng sinh ra một ma trận ngẫu nhiên với một số thông số được đặt cố định theo yêu cầu.
Trong thư mục MazeGen, chạy file mazeGen.exe  (hoặc (mazeGen-v2.exe) để sinh ma trận ngẫu nhiên, ma trận này sẽ được lưu vào file input.txt. Các tham số cần nhập vào gồm có kích thước ma trận và số lượng bonus. Ngoài ra, để chỉnh sửa thêm các thông số khác như giới hạn giá trị vật phẩm, tỉ lệ vật phẩm thưởng/ phạt,... ta có thể mở file mazeGen-v2.cpp và chỉnh sửa theo hướng dẫn, sau đó biên dịch lại và chạy chương trình mới.
