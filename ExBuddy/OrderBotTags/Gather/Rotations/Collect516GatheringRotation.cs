﻿namespace ExBuddy.OrderBotTags.Gather.Rotations
{
    using Attributes;
    using ff14bot;
    using ff14bot.Managers;
    using Helpers;
    using Interfaces;
    using System.Threading.Tasks;

    [GatheringRotation("Collect516", 35, 600)]
    public sealed class Collect516GatheringRotation : CollectableGatheringRotation, IGetOverridePriority
    {
        #region IGetOverridePriority Members

        int IGetOverridePriority.GetOverridePriority(ExGatherTag tag)
        {
            // if we have a collectable && the collectable value is greater than or equal to 515: Priority 515
            if (tag.CollectableItem != null && tag.CollectableItem.Value >= 516)
            {
                return 516;
            }
            return -1;
        }

        #endregion IGetOverridePriority Members

        public override async Task<bool> ExecuteRotation(ExGatherTag tag)
        {
            if (tag.Node.IsUnspoiled())
            {
                await DiscerningMethodical(tag);
                await DiscerningMethodical(tag);
                await DiscerningMethodical(tag);
                await IncreaseChance(tag);
            }
            else
            {
                if (Core.Player.CurrentGP >= 600)
                {
                    if (GatheringManager.SwingsRemaining > 4)
                    {
                        await DiscerningMethodical(tag);
                        await DiscerningMethodical(tag);
                        await DiscerningMethodical(tag);
                        await IncreaseChance(tag);
                    }
                    else
                    {
                        await Methodical(tag);
                        await Methodical(tag);
                        await Methodical(tag);
                        await Methodical(tag);
                    }
                }
                else
                {
                    await Impulsive(tag);
                    await Impulsive(tag);
                    await Instinctual(tag);
                }
            }

            return true;
        }
    }
}