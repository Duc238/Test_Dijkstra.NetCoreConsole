using System.Text;
//class DijkstraAlgorithm
//{
//    private int V; // Số đỉnh trong đồ thị
//    private List<int>[] adjacencyList; // Danh sách kề

//    public DijkstraAlgorithm(int v)
//    {
//        V = v;
//        adjacencyList = new List<int>[v];
//        for (int i = 0; i < v; ++i)
//        {
//            adjacencyList[i] = new List<int>();
//        }
//    }

//    // Thêm cạnh vào đồ thị
//    public void AddEdge(int source, int destination, int weight)
//    {
//        adjacencyList[source].Add(destination);
//        adjacencyList[destination].Add(source);
//    }

//    // Tìm đường đi ngắn nhất từ nguồn đến đích
//    public void Dijkstra(int source, int destination)
//    {
//        int[] distance = new int[V];
//        bool[] visited = new bool[V];

//        for (int i = 0; i < V; ++i)
//        {
//            distance[i] = int.MaxValue;
//            visited[i] = false;
//        }

//        distance[source] = 0;

//        for (int count = 0; count < V - 1; ++count)
//        {
//            int u = MinDistance(distance, visited);
//            visited[u] = true;

//            foreach (int v in adjacencyList[u])
//            {
//                if (!visited[v] && distance[u] != int.MaxValue && distance[u] + 1 < distance[v])
//                {
//                    distance[v] = distance[u] + 1;
//                }
//            }
//        }
//        Console.OutputEncoding = Encoding.UTF8;
//        Console.WriteLine($"Đường đi ngắn nhất từ {source} đến {destination} là: {distance[destination]}");
//    }

//    // Hàm hỗ trợ để tìm đỉnh có khoảng cách ngắn nhất chưa được xử lý
//    private int MinDistance(int[] distance, bool[] visited)
//    {
//        int min = int.MaxValue, minIndex = -1;

//        for (int v = 0; v < V; ++v)
//        {
//            if (!visited[v] && distance[v] <= min)
//            {
//                min = distance[v];
//                minIndex = v;
//            }
//        }

//        return minIndex;
//    }

//    public static void Main(string[] args)
//    {
//        DijkstraAlgorithm graph = new DijkstraAlgorithm(6);

//        graph.AddEdge(0, 1, 1);
//        graph.AddEdge(0, 2, 1);
//        graph.AddEdge(1, 3, 1);
//        graph.AddEdge(2, 4, 1);
//        graph.AddEdge(3, 5, 1);
//        graph.AddEdge(4, 5, 1);

//        int source = 0, destination = 5;
//        graph.Dijkstra(source, destination);
//    }
//}
//class DijkstraApp
//{
//    private static int V = 6; // Số đỉnh trong đồ thị

//    private static void Dijkstra(int[,] graph, int source, int destination)
//    {
//        int[] distance = new int[V];
//        bool[] visited = new bool[V];

//        for (int i = 0; i < V; ++i)
//        {
//            distance[i] = int.MaxValue;
//            visited[i] = false;
//        }

//        distance[source] = 0;

//        for (int count = 0; count < V - 1; ++count)
//        {
//            int u = MinDistance(distance, visited);
//            visited[u] = true;

//            for (int v = 0; v < V; ++v)
//            {
//                if (!visited[v] && graph[u, v] != 0 && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
//                {
//                    distance[v] = distance[u] + graph[u, v];
//                }
//            }
//        }

//        Console.WriteLine($"Đường đi ngắn nhất từ {source} đến {destination} là: {distance[destination]}");
//    }

//    private static int MinDistance(int[] distance, bool[] visited)
//    {
//        int min = int.MaxValue, minIndex = -1;

//        for (int v = 0; v < V; ++v)
//        {
//            if (!visited[v] && distance[v] <= min)
//            {
//                min = distance[v];
//                minIndex = v;
//            }
//        }

//        return minIndex;
//    }

//    public static void Main()
//    {
//        Console.OutputEncoding = Encoding.UTF8;
//        int[,] graph = {
//            {0, 1, 1, 0, 0, 0},
//            {1, 0, 0, 1, 0, 0},
//            {1, 0, 0, 1, 1, 0},
//            {0, 1, 1, 0, 0, 1},
//            {0, 0, 1, 0, 0, 1},
//            {0, 0, 0, 1, 1, 0}
//        };

//        int source = 0, destination = 5;
//        Dijkstra(graph, source, destination);
//    }
//}
class GPSNavigationSystem
{
    private Dictionary<string, Dictionary<string, int>> graph;

    public GPSNavigationSystem()
    {
        // Khởi tạo đồ thị
        graph = new Dictionary<string, Dictionary<string, int>>();
    }

    // Thêm cạnh vào đồ thị
    public void AddEdge(string source, string destination, int distance)
    {
        if (!graph.ContainsKey(source))
            graph[source] = new Dictionary<string, int>();

        if (!graph.ContainsKey(destination))
            graph[destination] = new Dictionary<string, int>();

        graph[source][destination] = distance;
        graph[destination][source] = distance; // Đồ thị vô hướng
    }

    /// <summary>
    /// Tìm đường đi ngắn nhất sử dụng thuật toán Dijkstra
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination">Đỉnh đích</param>
    public void FindShortestPath(string source, string destination)
    {
        Dictionary<string, int> distance = new Dictionary<string, int>();
        Dictionary<string, string> previous = new Dictionary<string, string>();
        HashSet<string> visited = new HashSet<string>();

        foreach (var node in graph.Keys)
        {
            distance[node] = int.MaxValue;
            previous[node] = null;
        }

        distance[source] = 0;

        while (visited.Count < graph.Count)
        {
            string u = MinDistanceNode(distance, visited);
            visited.Add(u);

            foreach (var v in graph[u])
            {
                int alt = distance[u] + v.Value; //v.Value: Trọng số giữa 1 đỉnh đến tất cả các đỉnh, distance[u]: Khoảng cách đến đỉnh u
                if (alt < distance[v.Key])//distance[v.Key]: Khoảng cách đến đỉnh v
                {
                    distance[v.Key] = alt;
                    previous[v.Key] = u;
                }
            }
        }

        PrintPath(source, destination, previous);
    }

    // Hàm hỗ trợ để tìm đỉnh có khoảng cách ngắn nhất chưa được xử lý
    private string MinDistanceNode(Dictionary<string, int> distance, HashSet<string> visited)
    {
        int min = int.MaxValue;
        string minNode = null;

        foreach (var node in graph.Keys)
        {
            if (!visited.Contains(node) && distance[node] < min)
            {
                min = distance[node];
                minNode = node;
            }
        }

        return minNode;
    }

    // In đường đi từ nguồn đến đích
    private void PrintPath(string source, string destination, Dictionary<string, string> previous)
    {
        Console.Write($"Đường đi ngắn nhất từ {source} đến {destination}: ");
        List<string> path = new List<string>();

        for (string at = destination; at != null; at = previous[at])
        {
            path.Add(at);
        }

        path.Reverse();
        Console.WriteLine(string.Join(" -> ", path));
    }

    static void Main()
    {
        Console.OutputEncoding=Encoding.UTF8;
        GPSNavigationSystem navigationSystem = new GPSNavigationSystem();

        // Thêm các cạnh của đồ thị
        navigationSystem.AddEdge("Gò Vấp", "Trung Quốc", 5);
        navigationSystem.AddEdge("Gò Vấp", "Quận 1", 2);
        navigationSystem.AddEdge("Trung Quốc", "Quận 3", 4);
        navigationSystem.AddEdge("Quận 1", "Quận 3", 7);
        navigationSystem.AddEdge("Quận 1", "Thủ Đức", 1);
        navigationSystem.AddEdge("Quận 3", "Thủ Đức", 3);

        // Tìm đường đi ngắn nhất
        navigationSystem.FindShortestPath("Gò Vấp", "Quận 3");
    }
}