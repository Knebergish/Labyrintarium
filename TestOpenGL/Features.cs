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
        
        // Уклонение сущности.
        int evasion;
        
        ////////////////////////////////////////////////////////////////////////////////////////


        public Features(Being owner, int stamina, int power, int agility)
        {
            this.owner = owner;

            // Сила = 0
            listFeatures.Add(power);
            // Выносливость = 1
            listFeatures.Add(stamina);
            // Ловкость = 2
            listFeatures.Add(agility);

            Recalculate();
            this.currentHealth = this.maxHealth;
            this.actionPoints = 1;
        }
        public Features(Being owner)
            : this(owner, 1, 1, 1)
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
                if (actionPoints < 1)
                    owner.eventsBeing.BeingEndActionPoints();
            }
        }

        public double IncreaseActionPoints
        {
            get { return increaseActionPoints; }
            //set { increaseActionPoints = value; }
        }

        public int Evasion
        { get { return evasion; } }
        /////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// Пересчёт параметров сущности.
        /// </summary>
        public void Recalculate()
        {
            this.maxHealth = this.listFeatures[(int)Feature.Stamina] * 10;
            this.regeneration = this.maxHealth / 100;
            this.evasion = this.listFeatures[(int)Feature.Agility];

            this.increaseActionPoints = 1 + (double)(this.listFeatures[(int)Feature.Agility] - 1) / 4;
        }
    }
}
