using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ListVsArray>();
        }
    }



    [ClrJob(isBaseline: true)] //, CoreRtJob, MonoJob, CoreJob
    [RPlotExporter, RankColumn]
    public class ListVsArray
    {
        //private SHA256 sha256 = SHA256.Create();
        //private MD5 md5 = MD5.Create();
        //private byte[] data;
        private IEnumerable<int> _enumerable;

        [Params(10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _enumerable = Enumerable.Range(0, N);
            //data = new byte[N];
            //new Random(42).NextBytes(data);
        }

        [Benchmark]
        public List<int> List() => _enumerable.ToList();

        [Benchmark]
        public int[] Array() => _enumerable.ToArray();
    }
}
