using System;
using System.Collections.Generic;
using System.Linq;


namespace TestOpenGL
{
    class States : IStateble
    {
        Dictionary<State, double> statesDictionary;
        //-------------


        public States()
        {
            statesDictionary = new Dictionary<State, double>();
            int countStates = Enum.GetNames(typeof(State)).Length;
            for (int i = 0; i < countStates; i++)
                statesDictionary.Add((State)i, 0);

            statesDictionary[State.IncreaseActionPoints] = 1;
        }
        public States(params double[] args)
            : this()
        {
            if (args.Length != statesDictionary.Count)
            {
                string s = "";
                foreach (int i in args)
                    s += " " + i.ToString();
                ExceptionAssistant.NewException(new ArgumentException($"Неверное количество значений статистик передано в конструктор! \nПараметры: {s}"));
            }

            for (int i = 0; i < args.Length; i++)
                statesDictionary[(State)i] = args[i];
        }

        double IStateble.this[State state]
        { get { return statesDictionary[state]; } }
        //=============


        List<double> IStateble.GetAllStates()
        {
            return statesDictionary.Values.ToList();
        }
    }
}
