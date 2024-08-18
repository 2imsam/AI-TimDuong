using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_Group5
{
    class Help
    {
        int[] dr = { -1, 0, 1, 0 };
        int[] dc = { 0, 1, 0, -1 };
        int[,] Maze;
        public Help(int[,] Maze)
        {
            this.Maze = Maze;
        }
        public List<Tuple<int, int>> FindPath(int StartX, int StartY, int EndX, int EndY)
        {
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            bool[,] visited = new bool[Maze.GetLength(0), Maze.GetLength(1)];

            if (DFS(StartX, StartY, EndX, EndY, path, visited))
            {
                return path;
            }
            return null;
        }
        private bool DFS(int StartX, int StartY, int EndX, int EndY, List<Tuple<int, int>> path, bool[,] visited)
        {
            if (StartX < 0 || StartX >= Maze.GetLength(0) || StartY < 0 || StartY >= Maze.GetLength(1) || visited[StartX, StartY] || Maze[StartX, StartY] == 1)
            {
                return false;
            }

            visited[StartX, StartY] = true;
            path.Add(new Tuple<int, int>(StartX, StartY));

            if (StartX == EndX && StartY == EndY)
            {
                return true;
            }

            // Di chuyển theo tất cả các hướng có thể (lên, xuống, trái, phải)
            if (DFS(StartX - 1, StartY, EndX, EndY, path, visited) ||
                DFS(StartX + 1, StartY, EndX, EndY, path, visited) ||
                DFS(StartX, StartY - 1, EndX, EndY, path, visited) ||
                DFS(StartX, StartY + 1, EndX, EndY, path, visited))
            {
                return true;
            }

            // Nếu không tìm thấy đường đi, quay lui
            path.RemoveAt(path.Count - 1);
            return false;
        }
        private bool IsValid(int row, int col)
        {
            // Kiểm tra xem ô có tọa độ (row, col) có nằm trong biên của mê cung không
            bool withinBounds = row >= 0 && row < Maze.GetLength(0) && col >= 0 && col < Maze.GetLength(1);

            // Kiểm tra xem ô có phải là ô cấm không (ví dụ: Maze[row, col] == 1)
            bool notBlocked = withinBounds && Maze[row, col] != 1;

            return withinBounds && notBlocked;
        }

        public bool HillClimbing(ref List<Tuple<int, int>> path)
        {
            bool improve = false;
            List<List<Tuple<int, int>>> allPaths = FindAllPaths(path[1].Item1, path[1].Item2, path[path.Count - 1].Item1, path[path.Count - 1].Item2);

            foreach (List<Tuple<int, int>> neighborPath in GetNeighbors(path))
            {
                if (neighborPath.Count < path.Count)
                {
                    path = neighborPath;
                    improve = true;
                    break; // Chỉ cập nhật một lần nếu tìm thấy đường đi ngắn hơn
                }
            }
            return improve;
        }
        private List<List<Tuple<int, int>>> GetNeighbors(List<Tuple<int, int>> path)
        {
            Random random = new Random();
            // Trả về danh sách các đường đi con (hàng xóm) của đường đi hiện tại
            List<List<Tuple<int, int>>> neighbors = new List<List<Tuple<int, int>>>();

            for (int i = 1; i < path.Count - 1; i++)
            {
                List<Tuple<int, int>> neighborPath = new List<Tuple<int, int>>(path);
                // Thay đổi một phần tử của đường đi để tạo ra một đường đi con mới
                int newX = random.Next(Maze.GetLength(0), Maze.GetLength(1));
                int newY = random.Next(Maze.GetLength(0), Maze.GetLength(1));
                neighborPath[i] = new Tuple<int, int>(newX, newY);
                neighbors.Add(neighborPath);
            }

            return neighbors;
        }
        private int RouteAssessment(List<Tuple<int, int>> path)
        {
            //Đánh giá chất lượng đường đi
            return path.Count;
        }
        public List<List<Tuple<int, int>>> FindAllPaths(int startX, int startY, int endX, int endY)
        {
            //Trả về danh sách tất cả các đường đi
            List<List<Tuple<int, int>>> allPaths = new List<List<Tuple<int, int>>>();
            List<Tuple<int, int>> currentPath = new List<Tuple<int, int>>();
            bool[,] visited = new bool[Maze.GetLength(0), Maze.GetLength(1)];

            FindAllPathsDFS(startX, startY, endX, endY, currentPath, allPaths, visited);

            return allPaths;
        }

        private void FindAllPathsDFS(int currentX, int currentY, int endX, int endY, List<Tuple<int, int>> currentPath, List<List<Tuple<int, int>>> allPaths, bool[,] visited)
        {
            if (currentX < 0 || currentX >= Maze.GetLength(0) || currentY < 0 || currentY >= Maze.GetLength(1) || visited[currentX, currentY] || Maze[currentX, currentY] == 1)
            {
                return;
            }

            visited[currentX, currentY] = true;
            currentPath.Add(new Tuple<int, int>(currentX, currentY));

            if (currentX == endX && currentY == endY)
            {
                // Nếu đến được điểm đích, thêm một bản sao của đường đi vào danh sách các đường đi
                allPaths.Add(new List<Tuple<int, int>>(currentPath));
            }
            else
            {
                // Nếu chưa đến được điểm đích, tiếp tục tìm kiếm các đường đi
                FindAllPathsDFS(currentX - 1, currentY, endX, endY, currentPath, allPaths, visited);
                FindAllPathsDFS(currentX + 1, currentY, endX, endY, currentPath, allPaths, visited);
                FindAllPathsDFS(currentX, currentY - 1, endX, endY, currentPath, allPaths, visited);
                FindAllPathsDFS(currentX, currentY + 1, endX, endY, currentPath, allPaths, visited);
            }

            // Quay lui
            visited[currentX, currentY] = false;
            currentPath.RemoveAt(currentPath.Count - 1);
        }
        public List<Tuple<int, int>> bestPath(ref List<Tuple<int, int>> path)
        {
            //Tìm ra đường đi tốt nhất trong danh sách các đường đi
            List <List<Tuple<int, int>>> allPaths = FindAllPaths(path[1].Item1, path[1].Item2, path[path.Count - 1].Item1, path[path.Count - 1].Item2);
            foreach(List<Tuple<int, int>> item in allPaths)
            {
                if(path.Count > item.Count)
                {
                    path = item;
                }
            }
            return path;
        }
    }
}
