namespace stratis
{
    using Stratis.Bitcoin;
    using Stratis.Bitcoin.Builder;
    using Stratis.Bitcoin.Configuration;
    using Stratis.Bitcoin.Features.BlockStore;
    using Stratis.Bitcoin.Features.Consensus;
    using Stratis.Bitcoin.Features.MemoryPool;
    using Stratis.Bitcoin.Features.Miner;
    using Stratis.Bitcoin.Features.RPC;
    using Stratis.Bitcoin.Features.Wallet;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            NodeSettings nodeSettings;
            IFullNodeBuilder fullNodeBuilder;
            IFullNode fullNode;

            try
            {
                nodeSettings = new NodeSettings().LoadArguments(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine("There was a problem initializing node settings. Details: '{0}'", exception.Message);
                Console.ReadLine();
                return;
            }

            try
            {
                fullNodeBuilder = new FullNodeBuilder()
                    .UseNodeSettings(nodeSettings)
                    .UseConsensus()
                    .UseBlockStore()
                    .UseMempool()
                    .AddMining()
                    .AddRPC()
                    .UseWallet();

                fullNode = fullNodeBuilder.Build();
            }
            catch (Exception exception)
            {
                Console.WriteLine("There was a problem initializing the node. Details: '{0}'", exception.Message);
                Console.ReadLine();
                return;
            }

            try
            {
                fullNode.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine("There was a problem starting the node. Details: '{0}'", exception.Message);
                Console.ReadLine();
                return;
            }
        }
    }
}