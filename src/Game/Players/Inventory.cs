// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Content;
using Common;
using Game.Network;

namespace Game.Players
{
    public static class Inventory
    {
        internal static void Equip(Player player, Item item, bool sendUpdate)
        {
            uint equippedHandle;
            int position = Arcadia.ItemResource.Find(obj => obj.id == item.Code).wear_type;

            if (player.WearInfo.TryGetValue(position, out equippedHandle))
            {
                if (equippedHandle > 0)
                {
                    // Has an item equipped in this slot, unequip it!
                    player.Unequip(position, false);
                }

                // Equip the item (if key exists)
                player.WearInfo[position] = item.Handle;
            }
            else
            {
                // Equip the item (if key doesn't exists);
                player.WearInfo.Add(position, item.Handle);
            }

            item.WearInfo = position;

            player.Attributes.Add(item);

            if (sendUpdate)
            {
                ClientPackets.Instance.StatInfo(player, player.Stats, player.Attributes, false);
                ClientPackets.Instance.StatInfo(player, player.BonusStats, player.BonusAttributes, true);
            }
        }

        internal static void Unequip(Player player, int position, bool sendUpdate)
        {
            Item item = (Item)GObjectManager.Get(ObjectType.Item, player.WearInfo[position]);

            player.Attributes.Remove(item);
            item.WearInfo = -1;
            player.WearInfo[position] = 0;

            if (sendUpdate)
            {
                ClientPackets.Instance.StatInfo(player, player.Stats, player.Attributes, false);
                ClientPackets.Instance.StatInfo(player, player.BonusStats, player.BonusAttributes, true);
            }
        }
    }
}
