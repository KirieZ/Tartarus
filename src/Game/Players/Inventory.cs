// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using Game.Content;
using Common;
using Game.Network;
using System.Data.Common;

namespace Game.Players
{
    public static class Inventory
    {
        internal static void Load(Player player)
        {
            using (DBManager dbManager = new DBManager(Databases.User))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(20))
                {
                    dbManager.CreateInParameter(dbCmd, "charId", System.Data.DbType.Int64, player.CharacterId);

                    #region Character Inventory load

                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int off = 0;

                                Item item = new Item((long)reader[off++]);
                                off += 6;
                                item.Code = (int)reader[off++];
                                item.Count = (long)reader[off++];
                                item.Level = (int)reader[off++];
                                item.Enhance = (int)reader[off++];
                                item.Durability = (int)reader[off++];
                                item.Endurance = (uint)(int)reader[off++];
                                item.Flag = (int)reader[off++];
                                item.GCode = (int)reader[off++];
                                item.WearInfo = (int)reader[off++];
                                item.Socket[0] = (int)reader[off++];
                                item.Socket[1] = (int)reader[off++];
                                item.Socket[2] = (int)reader[off++];
                                item.Socket[3] = (int)reader[off++];
                                item.RemainTime = (int)reader[off++];
                                // TODO : Elemental data

                                if (item.WearInfo >= 0)
                                    Equip(player, item, false);

                                player.Inventory.Add(item.Handle);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ConsoleUtils.ShowError("Failed to load inventory. Error: {0}", e.Message);
                    }

                    #endregion
                }
            }
        }

        internal static void Equip(Player player, Item item, bool sendUpdate)
        {
            uint equippedHandle;
            int position = Arcadia.ItemResource.Find(obj => obj.id == item.Code).wear_type;

            if (player.WearInfo.TryGetValue(position, out equippedHandle))
            {
                if (equippedHandle > 0)
                {
                    // Has an item equipped in this slot, unequip it!
                    Unequip(player, position, false);
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

                ClientPackets.Instance.Property(player, "max_havoc", player.MaxHavoc, true);
                ClientPackets.Instance.Property(player, "max_chaos", player.MaxChaos, true);
                ClientPackets.Instance.Property(player, "max_stamina", player.MaxStamina, true);
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

                ClientPackets.Instance.Property(player, "max_havoc", player.MaxHavoc, true);
                ClientPackets.Instance.Property(player, "max_chaos", player.MaxChaos, true);
                ClientPackets.Instance.Property(player, "max_stamina", player.MaxStamina, true);
            }
        }
    }
}
