// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using Game.Content;
using Common;
using Game.Network;
using System.Data.Common;
using Game.Database.Structures;

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
                                item.WearInfo = (Wear)reader[off++];
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
            //short position = (short)Arcadia.ItemResource.Find(obj => obj.id == item.Code).wear_type;
            short position = 0;

            if (player.WearInfo[position] > 0)
            {
                // Has an item equipped in this slot, unequip it!
                Unequip(player, position, false);
            }
            
            // Equip the item (if key exists)
            player.WearInfo[position] = item.Handle;
            item.WearInfo = (Wear)position;

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
            item.WearInfo = Wear.None;
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

        internal static void InsertItem(int charId, int itemCode, bool equip)
        {
            //DB_Item item = Arcadia.ItemResource.Find(obj => obj.id == itemCode);
            DB_Item item = new DB_Item();
            using (DBManager dbManager = new DBManager(Databases.User))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(21))
                {
                    dbManager.CreateInParameter(dbCmd, "owner_id", System.Data.DbType.Int64, charId);
                    dbManager.CreateInParameter(dbCmd, "idx", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "code", System.Data.DbType.Int32, itemCode);
                    dbManager.CreateInParameter(dbCmd, "cnt", System.Data.DbType.Int64, 1);
                    dbManager.CreateInParameter(dbCmd, "level", System.Data.DbType.Int32, item.level);
                    dbManager.CreateInParameter(dbCmd, "enhance", System.Data.DbType.Int32, item.enhance);
                    dbManager.CreateInParameter(dbCmd, "durability", System.Data.DbType.Int32, item.ethereal_durability);
                    dbManager.CreateInParameter(dbCmd, "endurance", System.Data.DbType.Int32, item.endurance);
                    dbManager.CreateInParameter(dbCmd, "flag", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "gcode", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "wear_info", System.Data.DbType.Int32, (equip ? item.wear_type : -1));
                    dbManager.CreateInParameter(dbCmd, "socket_0", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "socket_1", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "socket_2", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "socket_3", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "remain_time", System.Data.DbType.Int32, item.available_period);
                    dbManager.CreateInParameter(dbCmd, "elemental_effect_type", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "elemental_effect_expire_time", System.Data.DbType.DateTime, new DateTime(0));
                    dbManager.CreateInParameter(dbCmd, "elemental_effect_attack_point", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "elemental_effect_magic_point", System.Data.DbType.Int32, 0);
                    dbManager.CreateInParameter(dbCmd, "create_time", System.Data.DbType.DateTime, DateTime.UtcNow);

                    try
                    {
                        dbCmd.Connection.Open();
                        dbCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
                    finally { dbCmd.Connection.Close(); }
                }
            }
        }
    }
}
