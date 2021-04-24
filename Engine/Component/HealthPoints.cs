namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Gives HP to the Player or an Enemy.
    /// </summary>
    public class HealthPoints : Component
    {
        private int currenHealtPoints;
        private int maxHealthPoints;
        private bool isDeadFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPoints"/> class.
        /// </summary>
        /// <param name="maxHP">MaxHP mus be creater then currHP</param>
        /// <param name="currHP">CurrHP must be less then maxHP, else CurrHP = maxHP</param>
        public HealthPoints(int maxHP, int currHP)
        {
            this.maxHealthPoints = maxHP;
            if (currHP > maxHP)
            {
                this.currenHealtPoints = maxHP;
            }
            else
            {
                this.currenHealtPoints = currHP;
            }

            if (this.currenHealtPoints > 0)
            {
                this.isDeadFlag = false;
            }
            else
            {
                this.isDeadFlag = true;
            }
        }

        /// <summary>
        /// Get the MaxHP.
        /// </summary>
        /// <returns>The MaxhHP.</returns>
        public int GetMaxHP()
        {
            return this.maxHealthPoints;
        }

        /// <summary>
        /// Get the currentHP.
        /// </summary>
        /// <returns>The currenHP.</returns>
        public int GetCurrHP()
        {
            return this.currenHealtPoints;
        }

        /// <summary>
        /// Get the isDeadFlag.
        /// </summary>
        /// <returns>The isDeadFlag.</returns>
        public bool GetIsDeadFlag()
        {
            return this.isDeadFlag;
        }

        /// <summary>
        /// Set the maxHP to a new value.
        /// </summary>
        /// <param name="maxHP">New maxHP.</param>
        /// <returns>The isDead Flag.</returns>
        public bool SetMaxHP(int maxHP)
        {
            if (maxHP > 0)
            {
                this.maxHealthPoints = maxHP;
                if (this.currenHealtPoints > this.maxHealthPoints)
                {
                    this.currenHealtPoints = this.maxHealthPoints;
                }
            }
            else
            {
                this.maxHealthPoints = 0;
                this.currenHealtPoints = 0;
                this.isDeadFlag = true;
            }

            return this.isDeadFlag;
        }

        /// <summary>
        /// Set HP to new Value.
        /// </summary>
        /// <param name="hp">New HP value.</param>
        /// <returns>The isDeadFlag.</returns>
        public bool SetHP(int hp)
        {
            if (hp > this.maxHealthPoints)
            {
                this.currenHealtPoints = this.maxHealthPoints;
                return this.isDeadFlag;
            }

            this.currenHealtPoints = hp;

            if (this.currenHealtPoints <= 0)
            {
                this.isDeadFlag = true;
            }

            return this.isDeadFlag;
        }

        /// <summary>
        /// Add HP.
        /// </summary>
        /// <param name="hp">HP to be added.</param>
        /// <returns>The isDeadFlag.</returns>
        public bool AddHP(int hp)
        {
            this.currenHealtPoints += hp;

            if (this.currenHealtPoints > this.maxHealthPoints)
            {
                this.currenHealtPoints = this.maxHealthPoints;
            }

            if (this.currenHealtPoints <= 0)
            {
                this.isDeadFlag = true;
            }

            return this.isDeadFlag;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            return;
        }
    }
}
