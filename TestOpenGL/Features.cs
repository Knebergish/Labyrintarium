using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    /// <summary>
    /// Класс описания характеристик сущности.
    /// </summary>
    class Features
    {
        // Ссылка на хозяина характеристик.
        public Being owner;

        // Ассоциативный массив с характеристиками.
        private List<int> listFeatures = new List<int>();

        // Максимальное здоровье сущности.
        int maxHealth;
        // Текущее здоровье сущности.
        int currentHealth;
        // регенерация здоровья в ход
        int regeneration;
        
        // Очки действия сущности
        double actionPoints;
        // Восстановление очков действия в ход
        double increaseActionPoints;
        
        ////////////////////////////////////////////////////////////////////////////////////////


        public Features(Being owner, int power, int coordination, int mmr, int stamina, int agility, int sense)
        {
            this.owner = owner;

            listFeatures.Add(power);
            listFeatures.Add(coordination);
            listFeatures.Add(mmr);
            listFeatures.Add(stamina);
            listFeatures.Add(agility);
            listFeatures.Add(sense);

            Recalculate();
            this.currentHealth = this.maxHealth;
            this.actionPoints = 0;
        }
        public Features(Being owner)
            : this(owner, 1, 1, 1, 1, 1, 1)
        { }


        public int this[Feature F]
        {
            get { return listFeatures[(int)F]; }
        }

        public int MaxHealth
        { get { return maxHealth; } }

        public int Regeneration
        {
            get { return regeneration; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                    owner.Death();
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
                owner.eventsBeing.BeingEndActionPoint();
            }
        }

        public double IncreaseActionPoints
        {
            get { return increaseActionPoints; }
        }
        /////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// Пересчёт параметров сущности.
        /// </summary>
        public void Recalculate()
        {
            this.maxHealth = this.listFeatures[(int)Feature.Stamina] * 10;
            this.regeneration = this.maxHealth / 100;

            this.increaseActionPoints = 1 + (double)(this.listFeatures[(int)Feature.Agility] - 1) / 4;
        }
    }
}
