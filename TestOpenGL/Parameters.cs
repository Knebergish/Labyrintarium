using System;
using System.Collections.Generic;
using System.Linq;

using TestOpenGL.PhisicalObjects;


namespace TestOpenGL
{
    class Parameters : IParameterable
    {
        IStateble primoStates;
        IStateble featuresStates;
        IStateble itemsStates;

        IFeatureble features;
        IEquipmentable equipment;

        double currentHealth;
        double currentActionPoints;

        event TEventDelegate<IParameterable> changeCurrentHealthEvent;
        event TEventDelegate<IParameterable> changeCurrentActionPointsEvent;
        event VoidEventDelegate endHealthEvent;
        event VoidEventDelegate endActionPointsEvent;
        event TEventDelegate<IStateble> changeStatesEvent;
        //-------------


        public Parameters(IEquipmentable equipment, IFeatureble features)
        {
            //TODO: нахрен?
            this.equipment = equipment;

            this.features = features ?? new TestFeatures();

            featuresStates = new States();
            itemsStates = new States();

            equipment.ChangeEquipmentEvent += UpdateItemsStates;
            UpdateItemsStates(equipment);

            //features.ChangeFeaturesEvent += UpdateFeaturesStates;
            UpdateFeaturesStates(features);
        }

        double IParameterable.CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value > primoStates[State.MaxHealth] ? primoStates[State.MaxHealth] : value;
                changeCurrentHealthEvent?.Invoke(this);
                if (currentHealth <= 0)
                    endHealthEvent?.Invoke();
            }
        }
        double IParameterable.CurrentActionPoints
        {
            get { return currentActionPoints; }
            set
            {
                currentActionPoints = value;
                changeCurrentActionPointsEvent?.Invoke(this);
                if (currentActionPoints <= 0)
                    endActionPointsEvent?.Invoke();
            }
        }

        int IFeatureble.this[Feature feature]
        { get { return features[feature]; } }
        double IFeatureble.CurrentExperience
        {
            get { return features.CurrentExperience; }
            set { features.CurrentExperience = value; }
        }
        int IFeatureble.CurrentLevel
        { get { return features.CurrentLevel; } }
        double IFeatureble.NextLevelExperience
        { get { return features.NextLevelExperience; } }
        int IFeatureble.FreeFeaturesPoints
        { get { return features.FreeFeaturesPoints; } }

        double IStateble.this[State state]
        { get { return primoStates[state]; } }

        event TEventDelegate<IParameterable> IParameterable.ChangeCurrentHealthEvent
        {
            add { changeCurrentHealthEvent += value; }
            remove { changeCurrentHealthEvent -= value; }
        }
        event TEventDelegate<IParameterable> IParameterable.ChangeCurrentActionPointsEvent
        {
            add { changeCurrentActionPointsEvent += value; }
            remove { changeCurrentActionPointsEvent -= value; }
        }
        event VoidEventDelegate IParameterable.EndHealthEvent
        {
            add { endHealthEvent += value; }
            remove { endHealthEvent -= value; }
        }
        event VoidEventDelegate IParameterable.EndActionPointsEvent
        {
            add { endActionPointsEvent += value; }
            remove { endActionPointsEvent -= value; }
        }
        event TEventDelegate<IStateble> IParameterable.ChangeStatesEvent
        {
            add { changeStatesEvent += value; }
            remove { changeStatesEvent -= value; }
        }
        event TEventDelegate<IFeatureble> IFeatureble.ChangeFeaturesEvent
        {
            add { features.ChangeFeaturesEvent += value; }
            remove { features.ChangeFeaturesEvent -= value; }
        }
        //=============


        IStateble SumStates(IStateble states1, IStateble states2)
        {
            double[] resultArray = states1.GetAllStates().ToArray();
            double[] additionalArray = states2.GetAllStates().ToArray();
            for (int i = 0; i < resultArray.Length; i++)
                resultArray[i] += additionalArray[i];

            return new States(resultArray);
        }

        void UpdatePrimoStates()
        {
            primoStates = SumStates(featuresStates, itemsStates);
            changeStatesEvent?.Invoke(this);
        }
        void UpdateItemsStates(IEquipmentable equipment)
        {
            IStateble states = new States();
            List<Item> li = equipment.GetAllEquipmentItems() ?? new List<Item>();
            
            foreach (Item item in li)
            {
                states = SumStates(states, item.States);
            }

            itemsStates = states;

            UpdatePrimoStates();
        }
        void UpdateFeaturesStates(IFeatureble features)
        {
            //
            //
            UpdatePrimoStates();
        }

        List<int> IFeatureble.GetAllFeatures()
        {
            return features.GetAllFeatures();
        }
        void IFeatureble.SetFeature(Feature feature, int value)
        {
            features.SetFeature(feature, value);
        }
        bool IFeatureble.AdditionFeature(Feature feature)
        {
            return features.AdditionFeature(feature);
        }

        List<double> IStateble.GetAllStates()
        {
            return primoStates.GetAllStates();
        }
    }
}
