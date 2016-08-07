using System.Collections.Generic;

using TestOpenGL.PhisicalObjects;

namespace TestOpenGL.BeingContents
{
    /// <summary>
    /// Класс описания характеристик сущности.
    /// </summary>
    /*
    class Features
    {
        // Ссылка на хозяина характеристик.
        public Being owner;

        // Словарь с характеристиками.
        Dictionary<Feature, int> dictionaryFeatures;

        // Максимальное здоровье сущности.
        int maxHealth;
        // Текущее здоровье сущности.
        int currentHealth;
        // Регенерация здоровья в ход.
        int increaceHealth;
        
        // Очки действия сущности.
        double actionPoints;
        // Восстановление очков действия в ход.
        double increaseActionPoints;

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
        //-------------


        public Features(Being owner, int power, int coordination, int mmr, int stamina, int agility, int sense)
        {
            this.owner = owner;
            dictionaryFeatures = new Dictionary<Feature, int>();

            dictionaryFeatures.Add(Feature.Power, power);
            dictionaryFeatures.Add(Feature.Coordination, coordination);
            dictionaryFeatures.Add(Feature.MMR, mmr);
            dictionaryFeatures.Add(Feature.Stamina, stamina);
            dictionaryFeatures.Add(Feature.Agility, agility);
            dictionaryFeatures.Add(Feature.Sense, sense);

            currentHealth = 0;

            actionPoints = 0;

            currentExperience = 0;
            currentLevel = 0;
            nextLevelExperience = 10;
            nextLevelModifier = 2;
            freeFeaturesPoints = 0;

            Recalculate();

            currentHealth = maxHealth;
        }
        public Features(Being owner)
            : this(owner, 1, 1, 1, 1, 1, 1)
        { }

        public int this[Feature F]
        {
            get { return dictionaryFeatures[F]; }
        }

        public int MaxHealth
        { get { return maxHealth; } }

        public int IncreaceHealth
        { get { return increaceHealth; } }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;
                owner.EventsBeing.BeingChangeHealth();
                if (currentHealth <= 0)
                    owner.Despawn();
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;
            }
        }

        public double ActionPoints
        {
            get { return actionPoints; }
            set 
            {
                actionPoints = value;
                owner.EventsBeing.BeingChangeActionPoints();

                if(actionPoints < 1)
                    owner.EventsBeing.BeingEndActionPoints();
            }
        }

        public double IncreaseActionPoints
        { get { return increaseActionPoints; } }

        public double CurrentExperience
        {
            get {  return currentExperience; }
            set
            {
                currentExperience = value > currentExperience ? value : currentExperience;
                Recalculate();
            }
        }

        public double NextLevelExperience
        { get { return nextLevelExperience; } }

        public int FreeFeaturesPoints
        {
            get { return freeFeaturesPoints; }
            set { freeFeaturesPoints = value > freeFeaturesPoints ? value : freeFeaturesPoints; }
        }

        public int CurrentLevel
        { get { return currentLevel; } }
        //=============


        /// <summary>
        /// Пересчёт параметров сущности.
        /// </summary>
        public void Recalculate()
        {
            maxHealth = this[Feature.Stamina] * 10;
            //TODO: оно ж интовское, как ты туда запихнёшь одну сотую?..
            increaceHealth = maxHealth / 100;

            increaseActionPoints = 1 + (double)(this[Feature.Agility] - 1) / 4;

            while(currentExperience >= nextLevelExperience)
            {
                currentLevel++;
                freeFeaturesPoints++;
                nextLevelExperience *= nextLevelModifier;
            }
        }

        public bool AdditionFeature(Feature feature)
        {
            if(freeFeaturesPoints>=1)
            {
                freeFeaturesPoints--;
                dictionaryFeatures[feature]++;
                Recalculate();
                return true;
            }
            return false;
        }
    }
    */
}
