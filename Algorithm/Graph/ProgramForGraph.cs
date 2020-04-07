using System;
using System.Collections.Generic;

namespace Graph
{
    class ProgramForGraph
    {
        static void MainForGraph(string[] args)
        {
            AdjacencyMatrix adjcencyMatrix=new AdjacencyMatrix("../../../g2.txt");
            AdjacencyList adjacencyList=new AdjacencyList("../../../g2.txt");
            Console.WriteLine(adjcencyMatrix);
            Console.WriteLine(adjacencyList);

            DfsGraph dfsMatrix=new DfsGraph(adjcencyMatrix);
            dfsMatrix.PreOrder.ForEach(Console.WriteLine);
            Console.WriteLine("----------------华丽的分割线----------------");
            dfsMatrix.PostOrder.ForEach(Console.WriteLine);
            Console.WriteLine(dfsMatrix.ConnectedComponentCount);
            PrintListArray(dfsMatrix.ConnectedComponents());

            Console.WriteLine("----------------华丽的分割线----------------");

            DfsGraph dfsList=new DfsGraph(adjacencyList);
            dfsList.PreOrder.ForEach(Console.WriteLine);
            Console.WriteLine("----------------华丽的分割线----------------");
            dfsList.PostOrder.ForEach(Console.WriteLine);
            Console.WriteLine(dfsList.ConnectedComponentCount);
            PrintListArray(dfsList.ConnectedComponents());

            //Console.WriteLine("----------------华丽的分割线----------------");
            //SingleSourcePath singleSourcePath=new SingleSourcePath(adjcencyMatrix,0,6);
            //foreach (var i in singleSourcePath.Path())
            //{
            //    Console.Write(i+" ");
            //}

            Console.WriteLine();
            Console.WriteLine("----------------华丽的分割线----------------");
            CircleDetection circleDetection=new CircleDetection(adjcencyMatrix);
            Console.WriteLine(circleDetection.IsHaveCircle);

            Console.WriteLine("----------------华丽的分割线----------------");
            BinaryPartitionDetection binaryPartitionDetection=new BinaryPartitionDetection(adjcencyMatrix);
            Console.WriteLine(binaryPartitionDetection.IsBinaryPartition);

            BfsGraph bfsGraph=new BfsGraph(adjcencyMatrix);
            bfsGraph.Order.ForEach(Console.WriteLine);

            //Console.WriteLine("----------------华丽的分割线----------------");
            //SingleSourcePathUseBfs singleSourcePathUseBfs=new SingleSourcePathUseBfs(adjcencyMatrix,0);
            //foreach (var i in singleSourcePathUseBfs.Path(6))
            //{
            //    Console.Write(i+" ");
            //}

            //Console.WriteLine();
            //Console.WriteLine(singleSourcePathUseBfs.MinDistance(6));


            //Console.WriteLine("----------------华丽的分割线----------------");
            //int[][] grid = new int[4][];//[[1,1,0,0,0],[1,1,0,0,0],[0,0,0,1,1],[0,0,0,1,1]]
            //grid[0] =new int[]{ 1, 1, 0, 0, 0 };
            //grid[1]=new int[]{ 1, 1, 0, 0, 0 };
            //grid[2] = new int[] { 0, 0, 0, 1,1 };
            //grid[3] = new int[] { 0, 0, 0, 1, 1 };
            //Solution solution=new Solution();
            //Console.WriteLine(solution.MaxAreaOfIsland(grid));

            //Console.WriteLine("----------------华丽的分割线----------------");
            //int[][] grid = new int[4][];//[[1,1,0,0,0],[1,1,0,0,0],[0,0,0,1,1],[0,0,0,1,1]]
            //grid[0] = new int[] { 1, 1, 0, 0, 0 };
            //grid[1] = new int[] { 1, 1, 0, 0, 0 };
            //grid[2] = new int[] { 0, 0, 0, 1, 1 };
            //grid[3] = new int[] { 0, 0, 0, 1, 1 };
            //Solution1 solution = new Solution1();
            //Console.WriteLine(solution.MaxAreaOfIsland(grid));

            Console.WriteLine("----------------华丽的分割线----------------");
            Solution3 solution3=new Solution3();
            int[][] grid=new int[3][]
            {
                new int[3]{0,0,0},
                new int[3]{1,1,0},
                new int[3]{1,1,0}
            };
            Console.WriteLine(solution3.ShortestPathBinaryMatrix(grid));

            Console.WriteLine("----------------华丽的分割线----------------");
            Bulket bulket=new Bulket();
            bulket.WaterPullze().Path().ForEach(t=>Console.WriteLine(t[0]+","+t[1]));

            Console.WriteLine("----------------华丽的分割线----------------");
            FarmerAcrossTheRiver farmerAcrossTheRiver=new FarmerAcrossTheRiver();
            farmerAcrossTheRiver.AcrossTheRiver().ForEach(_ =>
            {
                Console.WriteLine(_[0]+","+_[1] + ","+ _[2] + ","+ _[3]);
            });


            Console.WriteLine("----------------华丽的分割线----------------");
            Bridge bridge=new Bridge();
            bridge.FindBridge(adjcencyMatrix);
            bridge.BridgeEdge.ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            CutPoint cutPoint=new CutPoint();
            cutPoint.FindCutPoint(adjcencyMatrix);
            foreach (var cutPointCutPoint in cutPoint.CutPoints)
            {
                Console.Write(cutPointCutPoint+" ");
            }


            Console.WriteLine();
            Console.WriteLine("----------------华丽的分割线----------------");
            HamiltonLoop hamiltonLoop=new HamiltonLoop();
            hamiltonLoop.FindHamiltonPath(adjcencyMatrix);
            hamiltonLoop.Path().ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine("----------------华丽的分割线----------------");
            HamiltonPath hamiltonPath=new HamiltonPath(adjcencyMatrix,0);
            foreach (var i in hamiltonPath.Path())
            {
                Console.Write(i+" ");
            }
            Console.WriteLine();

            Console.WriteLine("----------------华丽的分割线----------------");
            // [[1,0,0,0],[0,0,0,0],[0,0,2,-1]]
            DiferencePath diferencePath=new DiferencePath();
            int[][] s=new int[3][]{new int[]{ 1, 0, 0, 0 },new int[]{ 0, 0, 0, 0 }, new int[]{ 0, 0, 2, -1 } };
            Console.WriteLine(diferencePath.UniquePathsIII(s));

            Console.WriteLine("----------------华丽的分割线----------------");
            EulerLoop eulerLoop=new EulerLoop(adjcencyMatrix);
            eulerLoop.EulerLoopPath().ForEach(Console.WriteLine);


            Console.WriteLine("----------------华丽的分割线----------------");
            WeightGraph weightGraph=new WeightGraph("../../../mincreatetree.txt");
            MinCreateTreeUseKruskal minCreateTreeUseKruskal=new MinCreateTreeUseKruskal(weightGraph);
            minCreateTreeUseKruskal.Result().ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            MinCreateTreeUsePrim minCreateTreeUsePrim=new MinCreateTreeUsePrim(weightGraph);
            minCreateTreeUsePrim.Result().ForEach(Console.WriteLine);


            Console.WriteLine("----------------华丽的分割线----------------");
            WeightGraph weightGraph1=new WeightGraph("../../../dijkstra.txt");
            Dijkstra dijkstra=new Dijkstra(weightGraph1,0);
            Console.WriteLine(dijkstra.Distance(4));
            dijkstra.Path(4).ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            Bellman_Ford bellmanFord=new Bellman_Ford(weightGraph1,0);
            Console.WriteLine(bellmanFord.Distance(4));
            bellmanFord.Path(4).ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            Floyed floyed=new Floyed(weightGraph1);
            Console.WriteLine(floyed.ShortedLength(0,4));
            floyed.Path(0,4).ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            IAdjacency adjacency=new AdjacencyList("../../../directed.txt",true);
            DirectedCircleDectection directedCircleDectection=new DirectedCircleDectection(adjacency);
            Console.WriteLine(directedCircleDectection.IsHaveCircle);

            Console.WriteLine("----------------华丽的分割线----------------");
            ToopSort toopSort=new ToopSort(adjacency);
            Console.WriteLine(toopSort.IsHaveCircle);
            toopSort.Result.ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            ToopSortByDfs toopSortByDfs=new ToopSortByDfs(adjacency);
            toopSortByDfs.Result.ForEach(Console.WriteLine);

            Console.WriteLine("----------------华丽的分割线----------------");
            IAdjacency stronglyTest=new AdjacencyList("../../../stronglyconnectedweight.txt",true);
            StronglyConnectedWeight stronglyConnectedWeight=new StronglyConnectedWeight(stronglyTest);
            Console.WriteLine(stronglyConnectedWeight.StronglyConnectedWeightCount);
            for (int i = 0; i < stronglyConnectedWeight.StronglyConnectedComponent.Length; i++)
            {
                Console.Write(i+":");
                foreach (var v in stronglyConnectedWeight.StronglyConnectedComponent[i])
                {
                    Console.Write(v+" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------华丽的分割线----------------");
            WeightGraph edmondsGraph=new WeightGraph("../../../edmondskarp.txt", true);
            Edmonds_Karp edmondsKarp=new Edmonds_Karp(edmondsGraph,0,3);
            Console.WriteLine(edmondsKarp.MaxFlow);
            Console.WriteLine(edmondsKarp.FlowInTwoVertex(0,1));

            Console.WriteLine("----------------华丽的分割线----------------");
            AdjacencyList twoPartite=new AdjacencyList("../../../twopartitegraphmatch.txt"); 
            BinaryPartGraphMatch binaryPartGraphMatch=new BinaryPartGraphMatch(twoPartite);
            Console.WriteLine(binaryPartGraphMatch.MaxMatch);
            Console.WriteLine(binaryPartGraphMatch.IsPerfectMatch);

            Console.WriteLine("----------------华丽的分割线----------------");
            Hungarain hungarain=new Hungarain(twoPartite);
            Console.WriteLine(hungarain.MaxMatch);

            Console.WriteLine("----------------华丽的分割线----------------");
            Lcp4 lcp4=new Lcp4();
            int[][] test=new int[2][]{new int[]{1,0},new int[]{1,1} };
            Console.WriteLine(lcp4.domino(2,3,test));
            Console.WriteLine(lcp4.domino(3,3,new int[0][]));
        }

        public static void PrintListArray(List<int>[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                Console.Write(i+": ");
                foreach (var v in x[i])
                {
                    Console.Write(v+" ");
                }
                Console.WriteLine();
            }
        }
    }
}
