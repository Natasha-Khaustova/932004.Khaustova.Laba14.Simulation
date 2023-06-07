using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImModelLab14_Last
{
    public static class Model
    {
        private static List<Agent> agents = new List<Agent>();
        private static System system;
        private static ArrivalProcess arrivalProcess;
        private static double T = 0;
        public static double Time { get { return T; } }
        private static Agent activeAgent;
        private static int CustomersAmount;
        public static int AllCustomersAmount { get { return CustomersAmount; } }
        private static double LambdaArrival = 0;
        public static double LArr { get { return LambdaArrival; } }
        private static double LambdaOperator = 0;
        private static double LambdaOperatorNonPr = 0;
        public static double LOper { get { return LambdaOperator; } }
        public static double LOperNonPr { get { return LambdaOperatorNonPr; } }
        private static int OperatorsAmount = 0;
        public static int OpsAmount { get { return OperatorsAmount; } }

        public static void Run(double LambdaArr, double lambdaOper, int opsAmount)
        {
            LambdaArrival = LambdaArr;
            LambdaOperator = lambdaOper;
            OperatorsAmount = opsAmount;
            system = new System();
            arrivalProcess = new ArrivalProcess(system);
            agents.Add(system);
            agents.Add(arrivalProcess);
            T = 0;
        }
        public static bool Iter()
        {
            if (continueCondition(T, activeAgent))
            {
                double tMin = double.MaxValue;
                activeAgent = null;
                foreach (Agent agent in agents)
                {
                    double tAgent = agent.getNextEventTime();
                    if (tAgent < tMin)
                    {
                        tMin = tAgent;
                        activeAgent = agent;
                    }
                }
                T = tMin;
                if (activeAgent != null) activeAgent.processEvent();
                CustomersAmount++;
                return true;
            }
            return false;
        }
        public static bool continueCondition(double t, Agent activeAgent)
        {
            return (t < 100);
        }
        public static int queueSize()
        {
            return system.getQueueSize();
        }
        public static int queueSizeNonPr()
        {
            return system.getQueueSizeNonPr();
        }
        public static int getLeftCustom()
        {
            return system.leaveCustom;
        }
        public static int getBusyOperatorsSize()
        {
            return system.getBusyOperatorsSize();
        }
    }
}
