using Autofac;
using ConfAsker.Core;
using ConfAsker.Core.Interfaces;
using ConfAsker.Core.QueryProcessing;
using ConfAsker.Core.Units;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CommandRunner>().As<ICommandRunner>();
            builder.RegisterType<QueryParser>().As<IQueryParser>();
            builder.RegisterType<QueryValidator>().As<IQueryValidator>();

            LogFactory logFactory = new LogFactory();
            Logger logger = logFactory.GetCurrentClassLogger();

            IContainer diContainer = builder.Build();
            using (var scope = diContainer.BeginLifetimeScope())
            {
                ICommandRunner commandRunner = scope.Resolve<ICommandRunner>();
                IQueryValidator queryValidator = scope.Resolve<IQueryValidator>();
                IQueryParser queryParser = scope
                    .Resolve<IQueryParser>(new TypedParameter(typeof(IQueryValidator), 
                        queryValidator));
                QueryProcessor queryProcessor = new QueryProcessor(commandRunner,
                    queryParser);

                OperationResult queryRunningResult = queryProcessor.RunQuery(args);
                Console.WriteLine(queryRunningResult.Successful);

                if (!queryRunningResult.Successful)
                {
                    string logString = String.Format("\"{0}\". {1}",
                        String.Join(" ", args),
                        queryRunningResult.Description);

                    logger.Info(logString);
                }

                Console.ReadLine();
            }
        }
    }
}
