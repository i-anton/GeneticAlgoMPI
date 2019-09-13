using System.Text;

namespace GeneticAlgo
{
    public static class StringUtil
    {
        public static string ArrayToString(double[] arr)
        {
            var sb = new StringBuilder();
            sb.Append('[');
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i]);
                if (i != arr.Length - 1) sb.Append(',');
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
