using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'twoPluses' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING_ARRAY grid as parameter.
     */

    public static int twoPluses(List<string> grid)
    {
    int n = grid.Count;
    int m = grid[0].Length;

    char[][] G = grid.Select(row => row.ToCharArray()).ToArray();

    bool IsValid(int r, int c, int k)
    {
        if (r - k < 0 || r + k >= n || c - k < 0 || c + k >= m)
            return false;

        for (int i = -k; i <= k; i++)
        {
            if (G[r + i][c] != 'G' || G[r][c + i] != 'G')
                return false;
        }
        return true;
    }
    var pluses = new List<HashSet<(int, int)>>();

    for (int r = 0; r < n; r++)
    {
        for (int c = 0; c < m; c++)
        {
            int k = 0;
            while (IsValid(r, c, k))
            {
                var cells = new HashSet<(int, int)>();
                cells.Add((r, c));
                for (int i = 1; i <= k; i++)
                {
                    cells.Add((r + i, c));
                    cells.Add((r - i, c));
                    cells.Add((r, c + i));
                    cells.Add((r, c - i));
                }
                pluses.Add(cells);
                k++;
            }
        }
    }

    int maxProduct = 0;

    for (int i = 0; i < pluses.Count; i++)
    {
        for (int j = i + 1; j < pluses.Count; j++)
        {
            if (!pluses[i].Overlaps(pluses[j]))
            {
                int area1 = pluses[i].Count;
                int area2 = pluses[j].Count;
                maxProduct = Math.Max(maxProduct, area1 * area2);
            }
        }
    }

    return maxProduct;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<string> grid = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        int result = Result.twoPluses(grid);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
