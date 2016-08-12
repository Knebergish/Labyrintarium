using System;
using System.Collections.Generic;
using System.Linq;


namespace TestOpenGL
{
    class TestFeatures : IFeatureble
    {
        Dictionary<Feature, int> featuresDictionary;

        // Текущий опыт сущности.
        double currentExperience;
        // Текущий уровень сущности.
        int currentLevel;
        // Необходимый для поднятия уровня опыт.
        double nextLevelExperience;
        // Модификатор увеличения требуемого для поднятия уровня количества опыта.
        double nextLevelModifier;
        // Свободные для распределения очки характеристик.
        int freeFeaturesPoints;

        event TEventDelegate<IFeatureble> changeFeaturesEvent;
        //-------------


        public TestFeatures()
        {
            featuresDictionary = new Dictionary<Feature, int>();
            int countFeatures = Enum.GetNames(typeof(Feature)).Length;
            for (int i = 0; i < countFeatures; i++)
                featuresDictionary.Add((Feature)i, 0);

            currentExperience = 0;
            currentLevel = 0;
            nextLevelExperience = 10;
            nextLevelModifier = 2;
            freeFeaturesPoints = 0;
        }
        public TestFeatures(params int[] args)
            : this()
        {
            if (args.Length != featuresDictionary.Count)
            {
                string s = "";
                foreach (int i in args)
                    s += " " + i.ToString();
                ExceptionAssistant.NewException(new ArgumentException($"Неверное количество значений характеристик передано в конструктор! \nПараметры: {s}"));
            }

            for (int i = 0; i < args.Length; i++)
                featuresDictionary[(Feature)i] = args[i];
        }

        int IFeatureble.this[Feature feature]
        { get { return featuresDictionary[feature]; } }
        double IFeatureble.CurrentExperience
        { get { return currentExperience; } }
        int IFeatureble.CurrentLevel
        { get { return currentLevel; } }
        double IFeatureble.NextLevelExperience
        { get { return nextLevelExperience; } }
        int IFeatureble.FreeFeaturesPoints
        {
            get { return freeFeaturesPoints; }
            //set { freeFeaturesPoints = value > freeFeaturesPoints ? value : freeFeaturesPoints; }
        }

        event TEventDelegate<IFeatureble> IFeatureble.ChangeFeaturesEvent
        {
            add { changeFeaturesEvent += value; }
            remove { changeFeaturesEvent -= value; }
        }
        //=============


        void Recalculate()
        {
            while (currentExperience >= nextLevelExperience)
            {
                currentLevel++;
                freeFeaturesPoints++;
                nextLevelExperience *= nextLevelModifier;
            }
        }

        List<int> IFeatureble.GetAllFeatures()
        {
            return featuresDictionary.Values.ToList();
        }
        void IFeatureble.SetFeature(Feature feature, int value)
        {
            if (value < 0)
                ExceptionAssistant.NewException(new ArgumentOutOfRangeException("Значение характеристики не может быть меньше 0."));

            featuresDictionary[feature] = value;
            changeFeaturesEvent?.Invoke(this);
        }
        bool IFeatureble.AdditionFeature(Feature feature)
        {
            if (freeFeaturesPoints >= 1)
            {
                freeFeaturesPoints--;
                featuresDictionary[feature]++;
                Recalculate();
                return true;
            }
            return false;
        }
        void IFeatureble.AddExperience(int value)
        {
            if (value < 0)
                ExceptionAssistant.NewException(new ArgumentOutOfRangeException("Нельзя отнимать опыт."));

            currentExperience += value;
            Recalculate();
        }
    }
}
