// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.Content;
using Game.Players.Structures;
using Game.Network.Packets;

namespace Game.Network
{
    /// <summary>
    /// Packets exchanged between Client and Auth
    /// </summary>
    public class ClientPackets
    {
        public static readonly ClientPackets Instance = new ClientPackets();

        private delegate void PacketAction(Player client, PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;

        public ClientPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0001, CS_Login);
            PacketsDb.Add(0x0005, CS_MoveRequest);
            PacketsDb.Add(0x0017, CS_ReturnLobby);
            PacketsDb.Add(0x0019, CS_RequestReturnLobby);
            PacketsDb.Add(0x001A, CS_RequestLogout);
            PacketsDb.Add(0x001B, CS_Logout);
            PacketsDb.Add(0x0032, CS_Version);
            PacketsDb.Add(0x00C8, CS_PutOnItem);
            PacketsDb.Add(0x00C9, CS_PutOffItem);
            PacketsDb.Add(0x07D1, CS_CharacterList);
            PacketsDb.Add(0x07D2, CS_CreateCharacter);
            PacketsDb.Add(0x07D3, CS_DeleteCharacter);
            PacketsDb.Add(0x07D5, CS_AccountWithAuth);
            PacketsDb.Add(0x07D6, CS_CheckCharacterName);
            PacketsDb.Add(0x1F40, CS_SystemSpecs);

            PacketsDb.Add(0x270F, CA_Unknown);
            #endregion
        }

        /// <summary>
        /// Called whenever a packet is received from a game client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        public void PacketReceived(Player client, PacketStream stream)
        {
            // Is it a known packet ID
            if (!PacketsDb.ContainsKey(stream.GetId()))
            {
                ConsoleUtils.ShowWarning("Unknown packet Id: {0}", stream.GetId());
                return;
            }

            // Calls this packet parsing function
            Task.Factory.StartNew(() => { PacketsDb[stream.GetId()].Invoke(client, stream); });
        }

        #region Client Packets
        #region Unused Packets
        /// <summary>
        /// SFrame version (not used officially)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_Version(Player client, PacketStream stream)
        {
            //string version = stream.ReadString(20);
        }

        /// <summary>
        /// System specs (official name not found)
        /// not used
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_SystemSpecs(Player client, PacketStream stream) { }

        /// <summary>
        /// Unknown packet, maybe user to keep connection
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CA_Unknown(Player client, PacketStream stream) { }
        
        #endregion

        #region Lobby
        /// <summary>
        /// Connection from auth
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_AccountWithAuth(Player client, PacketStream stream)
        {
            string userId = stream.ReadString(61);
            ulong key = stream.ReadUInt64();

            Server.Instance.OnUserJoin(client, userId, key);
        }

        /// <summary>
        /// Request the character list
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_CharacterList(Player client, PacketStream stream)
        {
            //string userId = stream.ReadString(61);

            client.GetCharacterList();
        }

        /// <summary>
        /// Checks if character name is in use
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_CheckCharacterName(Player client, PacketStream stream)
        {
            string name = stream.ReadString(19);

            client.CheckCharacterName(name);
        }

        /// <summary>
        /// Request to create a character
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_CreateCharacter(Player client, PacketStream stream)
        {
            LobbyCharacterInfo charInfo = new LobbyCharacterInfo();

            charInfo.ModelInfo.Sex = stream.ReadInt32();
            charInfo.ModelInfo.Race = stream.ReadInt32();

            for (int i = 0; i < 5; i++)
                charInfo.ModelInfo.ModelId[i] = stream.ReadInt32();

            charInfo.ModelInfo.TextureId = stream.ReadInt32();

            for (int i = 0; i < 24; i++)
                charInfo.ModelInfo.WearInfo[i] = stream.ReadInt32();

            charInfo.Level = stream.ReadInt32();
            charInfo.Job = stream.ReadInt32();
            charInfo.JobLevel = stream.ReadInt32();
            charInfo.ExpPercentage = stream.ReadInt32();
            charInfo.Hp = stream.ReadInt32();
            charInfo.Mp = stream.ReadInt32();
            charInfo.Permission = stream.ReadInt32();
            charInfo.IsBanned = stream.ReadBool();
            charInfo.Name = stream.ReadString(19);
            charInfo.SkinColor = stream.ReadUInt32();
            charInfo.CreateTime = stream.ReadString(30);
            charInfo.DeleteTime = stream.ReadString(30);
            for (int i = 0; i < 24; i++)
                charInfo.WearItemEnhanceInfo[i] = stream.ReadInt32();
            for (int i = 0; i < 24; i++)
                charInfo.WearItemLevelInfo[i] = stream.ReadInt32();
            for (int i = 0; i < 24; i++)
                charInfo.WearItemElementalType[i] = stream.ReadByte();

            client.CreateCharacter(charInfo);
        }

        /// <summary>
        /// Requests to delete a character
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_DeleteCharacter(Player client, PacketStream stream)
        {
            string name = stream.ReadString(19);
            //string securityCode = stream.ReadString(19);

            client.DeleteCharacter(name);
        }

        /// <summary>
        /// Login to Game World
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_Login(Player client, PacketStream stream)
        {
            string name = stream.ReadString(19);
            byte race = stream.ReadByte();

            client.Login(name, race);
        }
        #endregion

        #region Logout
        /// <summary>
        /// Checks if can logout (quit game)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_RequestLogout(Player client, PacketStream stream)
        {
            // TODO : Proper check
            Result(client, 0x001A, 0);
        }

        /// <summary>
        /// Logout (quit)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_Logout(Player client, PacketStream stream)
        {
            // TODO : Function call
        }

        /// <summary>
        /// Checks if can return to lobby
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_RequestReturnLobby(Player client, PacketStream stream)
        {
            // TODO : Proper check
            Result(client, 0x0019, 0);
        }

        /// <summary>
        /// Return to lobby
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_ReturnLobby(Player client, PacketStream stream)
        {
            // TODO : Proper checks
            Result(client, 0x0017, 0);
        }
        #endregion

        #region Movement

        private void CS_MoveRequest(Player client, PacketStream stream)
        {
            Packets.CS.MoveRequest moveRequest = new Packets.CS.MoveRequest();
            List<Packets.CS.MoveRequest.MoveInfo> movePoints = new List<Packets.CS.MoveRequest.MoveInfo>();

            moveRequest.Handle = stream.ReadUInt32();
            moveRequest.X = stream.ReadSingle();
            moveRequest.Y = stream.ReadSingle();
            moveRequest.CurrentTime = stream.ReadUInt32();
            moveRequest.SpeedSync = stream.ReadBool();
            moveRequest.Count = stream.ReadUInt16();
            for (int i = 0; i < moveRequest.Count; ++i)
            {
                Packets.CS.MoveRequest.MoveInfo moveInfo = new Packets.CS.MoveRequest.MoveInfo();
                moveInfo.ToX = stream.ReadSingle();
                moveInfo.ToY = stream.ReadSingle();
                movePoints.Add(moveInfo);
            }

            moveRequest.Points = movePoints.ToArray();
            client.MoveRequest(moveRequest);
        }

        #endregion

        #region Inventory

        /// <summary>
        /// User wants to equip an item on self or a target
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_PutOnItem(Player client, PacketStream stream)
        {
            byte position = stream.ReadByte();
            uint itemHandle = stream.ReadUInt32();
            uint targetHandle = stream.ReadUInt32();

            if (client.Equip(itemHandle, position, targetHandle))
                this.Result(client, 0x00C8, 0, 0);
            else
                this.Result(client, 0x00C8, 3, 0); // TODO : Confirm this error code
        }

        /// <summary>
        /// User wants to unequip an item from self or a target
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_PutOffItem(Player client, PacketStream stream)
        {
            byte position = stream.ReadByte();
            uint targetHandle = stream.ReadUInt32();

            if (client.Unequip(position, targetHandle))
                client.Unequip(position, targetHandle);
            else
                this.Result(client, 0x00C9, 3, 0); // TODO : Confirm this error code
        }

        #endregion

        #endregion

        #region Server Packets
        /// <summary>
        /// Sends the result of a packet
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packetId"></param>
        /// <param name="response"></param>
        /// <param name="value"></param>
        public void Result(Player client, ushort packetId, ushort response = 0, int value = 0)
        {
            PacketStream stream = new PacketStream(0x0000);

            stream.WriteUInt16(packetId);
            stream.WriteUInt16(response);
            stream.WriteInt32(value);

            ClientManager.Instance.Send(client, stream, BroadcastArea.Self);
        }

        #region Lobby
        /// <summary>
        /// Sends the list of characters
        /// </summary>
        /// <param name="client"></param>
        /// <param name="charList"></param>
        public void CharacterList(Player client, LobbyCharacterInfo[] charList, ushort lastLoginIndex)
        {
            PacketStream stream = new PacketStream(0x07D4);

            stream.WriteUInt32(0); // currentSvTime
            stream.WriteUInt16(lastLoginIndex);
            stream.WriteUInt16((ushort)charList.Length);
            for (int i = 0; i < charList.Length; i++)
            {
                stream.WriteInt32(charList[i].ModelInfo.Sex);
                stream.WriteInt32(charList[i].ModelInfo.Race);
                for (int j = 0; j < 5; j++)
                    stream.WriteInt32(charList[i].ModelInfo.ModelId[j]);
                stream.WriteInt32(charList[i].ModelInfo.TextureId);
                for (int j = 0; j < 24; j++)
                    stream.WriteInt32(charList[i].ModelInfo.WearInfo[j]);
                stream.WriteInt32(charList[i].Level);
                stream.WriteInt32(charList[i].Job);
                stream.WriteInt32(charList[i].JobLevel);
                stream.WriteInt32(charList[i].ExpPercentage);
                stream.WriteInt32(charList[i].Hp);
                stream.WriteInt32(charList[i].Mp);
                stream.WriteInt32(charList[i].Permission);
                stream.WriteBool(charList[i].IsBanned);
                stream.WriteString(charList[i].Name, 19);
                stream.WriteUInt32(charList[i].SkinColor);
                stream.WriteString(charList[i].CreateTime, 30);
                stream.WriteString(charList[i].DeleteTime, 30);
                for (int j = 0; j < 24; j++)
                    stream.WriteInt32(charList[i].WearItemEnhanceInfo[j]);
                for (int j = 0; j < 24; j++)
                    stream.WriteInt32(charList[i].WearItemLevelInfo[j]);
                for (int j = 0; j < 24; j++)
                    stream.WriteByte(charList[i].WearItemElementalType[j]);
            }

            ClientManager.Instance.Send(client, stream, BroadcastArea.Self);
        }
        #endregion

        /// <summary>
        /// Sends a list of URLs
        /// </summary>
        /// <param name="client"></param>
        public void UrlList(Player player)
        {
            PacketStream stream = new PacketStream(0x2329);

            stream.WriteUInt16((ushort)Server.UrlList.Length);
            stream.WriteString(Server.UrlList);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        /// <summary>
        /// Updates game object stats and attributes
        /// </summary>
        /// <param name="player">the target</param>
        /// <param name="stat">stats</param>
        /// <param name="attribute">attributes</param>
        /// <param name="isBonus">are these bonus stats (green)</param>
        internal void StatInfo(Player player, CreatureStat stat, CreatureAttribute attribute, bool isBonus)
        {
            PacketStream stream = new PacketStream(0x03E8);

            stream.WriteUInt32(player.Handle);

            // Creature Stat
            stream.WriteInt16(stat.StatId);
            stream.WriteInt16(stat.STR);
            stream.WriteInt16(stat.VIT);
            stream.WriteInt16(stat.DEX);
            stream.WriteInt16(stat.AGI);
            stream.WriteInt16(stat.INT);
            stream.WriteInt16(stat.MEN);
            stream.WriteInt16(stat.LUK);

            // Creature Attributes
            stream.WriteInt16(attribute.Critical);
            stream.WriteInt16(attribute.CriticalPower);
            stream.WriteInt16(attribute.PAttackRight);
            stream.WriteInt16(attribute.PAttackLeft);
            stream.WriteInt16(attribute.Defense);
            stream.WriteInt16(attribute.BlockDefense);
            stream.WriteInt16(attribute.MAttack);
            stream.WriteInt16(attribute.MDefense);
            stream.WriteInt16(attribute.AccuracyRight);
            stream.WriteInt16(attribute.AccuracyLeft);
            stream.WriteInt16(attribute.MagicAccuracy);
            stream.WriteInt16(attribute.Evasion);
            stream.WriteInt16(attribute.MagicEvasion);
            stream.WriteInt16(attribute.BlockChance);
            stream.WriteInt16(attribute.MoveSpeed);
            stream.WriteInt16(attribute.AttackSpeed);
            stream.WriteInt16(attribute.AttackRange);
            stream.WriteInt16(attribute.MaxWeight);
            stream.WriteInt16(attribute.CastingSpeed);
            stream.WriteInt16(attribute.CoolTimeSpeed);
            stream.WriteInt16(attribute.ItemChance);
            stream.WriteInt16(attribute.HPRegenPercentage);
            stream.WriteInt16(attribute.HPRegenPoint);
            stream.WriteInt16(attribute.MPRegenPercentage);
            stream.WriteInt16(attribute.MPRegenPoint);

            stream.WriteBool(isBonus);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void LoginResult(Player player)
        {
            PacketStream stream = new PacketStream(0x0004);

            stream.WriteByte(1);
            stream.WriteUInt32(player.Handle);
            stream.WriteSingle(168344f);//player.Position.X);
            stream.WriteSingle(55400f);//player.Position.Y);
            stream.WriteSingle(player.Position.Z);
            stream.WriteByte(1);//player.Position.Layer);
            stream.WriteSingle(0f); // TODO : face_direction
            stream.WriteInt32(Globals.RegionSize);
            stream.WriteInt32(100);//player.Hp);
            stream.WriteInt16(100);//player.Mp);
            stream.WriteInt32(100);//player.MaxHp);
            stream.WriteInt16(100);// player.MaxMp);
            stream.WriteInt32(player.Havoc);
            stream.WriteInt32(Globals.MaxHavoc); // TODO : Is this constant?
            stream.WriteInt32(player.Sex);
            stream.WriteInt32(player.Race);
            stream.WriteUInt32(player.SkinColor);
            stream.WriteInt32(player.FaceId);
            stream.WriteInt32(player.HairId);
            stream.WriteString(player.Name, 19);
            stream.WriteInt32(Globals.CellSize);
            stream.WriteInt32(player.GuildId);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        /// <summary>
        /// Sets a property
        /// </summary>
        /// <param name="player">target player</param>
        /// <param name="name">property name</param>
        /// <param name="value">value</param>
        /// <param name="isInt">send as int?</param>
        internal void Property(Player player, string name, object value, bool isInt)
        {
            PacketStream stream = new PacketStream(0x01FB);

            stream.WriteUInt32(player.Handle);
            stream.WriteBool(isInt);
            stream.WriteString(name, 16);
            if (isInt)
            {
                stream.WriteInt64(Convert.ToInt64(value));
            }
            else
            {
                stream.WriteInt64(0);
                string val = (string)value;
                stream.WriteString(val, val.Length + 1);
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void WearInfo(Player player, uint[] wearInfo)
        {
            PacketStream stream = new PacketStream(0x00CA);

            int maxWear = (int)Wear.Max;
            List<int> enhance = new List<int>(maxWear);
            List<int> level = new List<int>(maxWear);
            List<byte> type = new List<byte>(maxWear);

            stream.WriteUInt32(player.Handle);

            for (int i = 0; i < maxWear; i++)
            {
                Item item = (Item)GObjectManager.Instance.Get(ObjectType.Item, wearInfo[i]);

                if (item != null)
                {
                    // If there's an item equipped in this slot
                    enhance.Add(item.Enhance);
                    level.Add(item.Level);
                    type.Add((byte)item.ElementalEffectType);
                    stream.WriteInt32(item.Code);
                }
                else
                {
                    enhance.Add(0);
                    level.Add(0);
                    type.Add(0);
                    // If nothing is equipped in the slot, and slot is armor, 
                    // hands or feet, sends body part id
                    switch (i)
                    {
                        case 2: stream.WriteInt32(player.BodyId); break;
                        case 3: stream.WriteInt32(player.HandsId); break;
                        case 4: stream.WriteInt32(player.FeetId); break;
                        default: stream.WriteInt32(0); break;
                    }
                }
            }

            for (int i = 0; i < maxWear; i++)
                stream.WriteInt32(enhance[i]);
            for (int i = 0; i < maxWear; i++)
                stream.WriteInt32(level[i]);
            for (int i = 0; i < maxWear; i++)
                stream.WriteByte(type[i]);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        /// <summary>
        /// Informs about changes in what target is wearing
        /// </summary>
        /// <param name="itemHandle"></param>
        /// <param name="position"></param>
        /// <param name="targetHandle"></param>
        /// <param name="enhance"></param>
        /// <param name="elementalEffectType"></param>
        internal void ItemWearInfo(Player player, uint itemHandle, short position, uint targetHandle, int enhance, short elementalEffectType)
        {
            PacketStream stream = new PacketStream(0x011f);

            stream.WriteUInt32(itemHandle);
            stream.WriteInt16(position);
            stream.WriteUInt32(targetHandle);
            stream.WriteInt32(enhance);
            stream.WriteByte((byte)elementalEffectType);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void InventoryList(Player player, List<uint> inventory)
        {
            PacketStream stream = new PacketStream(0x00CF);

            stream.WriteUInt16((ushort)inventory.Count);
            for (int i = 0; i < inventory.Count; i++)
            {
                Item item = (Item) GObjectManager.Instance.Get(ObjectType.Item, inventory[i]);

                // TS_ITEM_BASE_INFO
                stream.WriteUInt32(item.Handle);
                stream.WriteInt32(item.Code);
                stream.WriteInt64(item.UId);
                stream.WriteInt64(item.Count);
                stream.WriteInt32(item.Durability);
                stream.WriteUInt32(item.Endurance);
                stream.WriteByte((byte)item.Enhance);
                stream.WriteByte((byte)item.Level);
                stream.WriteInt32(item.Flag);
                stream.WriteInt32(item.Socket[0]);
                stream.WriteInt32(item.Socket[1]);
                stream.WriteInt32(item.Socket[2]);
                stream.WriteInt32(item.Socket[3]);
                stream.WriteInt32(item.RemainTime);
                stream.WriteByte((byte)item.ElementalEffectType);
                stream.WriteInt32(0); // TODO : elemental_effect_remain_time
                stream.WriteInt32(item.ElementalEffectAttackPoint);
                stream.WriteInt32(item.ElementalEffectMagicPoint);

                // TS_ITEM_INFO
                stream.WriteInt16((short)item.WearInfo);
                stream.WriteUInt32(0); // TODO : own_summon_handle
                stream.WriteInt32(i); // TODO : index
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void EquipSummon(Player player, SummonData[] summon, bool openDialog)
        {
            PacketStream stream = new PacketStream(0x012F);

            stream.WriteBool(openDialog);
            for (int i = 0; i < 6; i++)
            {
                stream.WriteUInt32(summon[i].CardHandle);
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void GoldUpdate(Player player, long gold, int chaos)
        {
            PacketStream stream = new PacketStream(0x03E9);

            stream.WriteInt64(gold);
            stream.WriteInt32(chaos);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void LevelUpdate(Player player, int level, int jobLevel)
        {
            PacketStream stream = new PacketStream(0x03EA);

            stream.WriteUInt32(player.Handle);
            stream.WriteInt32(level);
            stream.WriteInt32(jobLevel);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void ExpUpdate(Player player, long exp, int jp)
        {
            PacketStream stream = new PacketStream(0x03EB);

            stream.WriteUInt32(player.Handle);
            stream.WriteInt64(exp);
            stream.WriteInt32(jp);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void BeltSlotInfo(Player player, BeltSlotData[] belt)
        {
            PacketStream stream = new PacketStream(0x00D8);

            for (int i = 0; i < 6; i++)
            {
                stream.WriteUInt32(belt[i].Handle);
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        internal void StatusChange(Player player, uint handle, uint status)
        {
            PacketStream stream = new PacketStream(0x01F4);

            stream.WriteUInt32(handle);
            stream.WriteUInt32(status);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        public void QuestList(Player player)
        {
            // TODO  : Quest List
            PacketStream stream = new PacketStream(0x0258);

            stream.WriteUInt16(0); // count_active
            stream.WriteUInt16(0); // count_pending

            for (int i = 0; i < 1; i++) // count_active
            {
                stream.WriteInt32(0); // code
                for (int j = 0; i < 6; i++) // nRandomValue
                {
                    stream.WriteInt32(0);
                }
                for (int j = 0; i < 6; i++) // nStatus
                {
                    stream.WriteInt32(0);
                }
                stream.WriteByte(0); // nProgress
                stream.WriteUInt32(0); // nTimeLimit
            }

            for (int i = 0; i < 1; i++) // count_pending
            {
                stream.WriteInt32(0); // code
                stream.WriteInt32(0); // nStartID
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        /* TODO : add struct
// 0x0258
struct TS_SC_QUEST_LIST
{
TS_MESSAGE baseclass_0;
unsigned __int16 count_active;
unsigned __int16 count_pending;
//TS_SC_QUEST_LIST::TS_QUEST_INFO actives[count_active];
//TS_SC_QUEST_LIST::TS_PENDING_QUEST_INFO pendings[count_pending];
};

struct TS_SC_QUEST_LIST::TS_QUEST_INFO
{
int code;
int nStartID;
int nRandomValue[6];
int nStatus[6];
char nProgress;
unsigned int nTimeLimit;
};

struct TS_SC_QUEST_LIST::TS_PENDING_QUEST_INFO
{
int code;
int nStartID;
};*/

        internal void Chat(Player player, string sender, byte type, string message)
        {
            PacketStream stream = new PacketStream(0x0016);

            stream.WriteString(sender, 21);
            stream.WriteUInt16((ushort)message.Length);
            stream.WriteByte(type);
            stream.WriteString(message, message.Length+1);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        public void ChangeLocation(Player player, int prevLocation, int newLocation)
        {
            PacketStream stream = new PacketStream(0x0385);

            stream.WriteInt32(prevLocation);
            stream.WriteInt32(newLocation);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }

        public void WeatherInfo(Player player, uint regionId, ushort weatherId)
        {
            PacketStream stream = new PacketStream(0x0386);

            stream.WriteUInt32(regionId);
            stream.WriteUInt16(weatherId);

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }


        internal void Move(Player player)
        {
            PacketStream stream = new PacketStream(0x0008);

            stream.WriteUInt32(Globals.GetTime());
            stream.WriteUInt32(player.Handle);
            stream.WriteByte(player.Position.Layer);
            stream.WriteByte(11); // speed
            stream.WriteUInt16((ushort)player.PositionsToMove.Count);
            for (int i = 0; i < player.PositionsToMove.Count; ++i)
            {
                stream.WriteSingle(player.PositionsToMove[i].X);
                stream.WriteSingle(player.PositionsToMove[i].Y);
            }

            ClientManager.Instance.Send(player, stream, BroadcastArea.Self);
        }
        #endregion

        // Login Result placeholder
        internal static void send_Login_pre2(Player player)
        { // x0002
            Send(player, "0B 00 00 00 02 00 00 2E 3A 01 00 ");
        }

        internal static void send_Login_pre3(Player player)
        {
            Send(player, "13 00 00 00 4D 04 00 3E 3A 01 00 14 3C 21 56 00 00 00 00 ");
        }

        internal static void send_Login_pre4(Player player)
        {
            Send(player, "0B 00 00 00 13 27 00 00 00 00 00 ");
        }

        internal static void send_Login(Player player)
		{
            // URL List - 0x2329
            //Send(player, "2A 01 00 00 29 23 00 21 01 67 75 69 6C 64 2E 75 72 6C 7C 68 74 74 70 3A 2F 2F 67 75 69 6C 64 2E 74 65 61 6C 73 6B 69 65 73 2E 75 73 2F 63 6C 69 65 6E 74 2F 67 75 69 6C 64 2F 6C 6F 67 69 6E 2E 61 73 70 78 7C 67 75 69 6C 64 5F 74 65 73 74 5F 64 6F 77 6E 6C 6F 61 64 2E 75 72 6C 7C 75 70 6C 6F 61 64 2F 7C 77 65 62 5F 64 6F 77 6E 6C 6F 61 64 7C 67 75 69 6C 64 2E 74 65 61 6C 73 6B 69 65 73 2E 75 73 7C 77 65 62 5F 64 6F 77 6E 6C 6F 61 64 5F 70 6F 72 74 7C 30 7C 73 68 6F 70 2E 75 72 6C 7C 68 74 74 70 3A 2F 2F 36 37 2E 32 30 35 2E 31 31 32 2E 31 35 32 3A 37 37 36 36 2F 6B 68 72 6F 6F 73 7C 67 68 65 6C 70 5F 75 72 6C 7C 68 74 74 70 3A 2F 2F 72 61 70 70 65 6C 7A 2E 77 69 6B 69 61 2E 63 6F 6D 7C 67 75 69 6C 64 5F 69 63 6F 6E 5F 75 70 6C 6F 61 64 2E 69 70 7C 67 75 69 6C 64 2E 74 65 61 6C 73 6B 69 65 73 2E 75 73 7C 67 75 69 6C 64 5F 69 63 6F 6E 5F 75 70 6C 6F 61 64 2E 70 6F 72 74 7C 34 36 31 37 ");
            // Update Status - 0x03E8
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 2C 01 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 05 00 50 00 15 00 00 00 15 00 00 00 15 00 15 00 06 00 00 00 02 00 06 00 06 00 00 00 78 00 64 00 32 00 6E 00 64 00 64 00 02 00 05 00 32 00 05 00 32 00 00 ");
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 ");
            // Property - 0x01FB
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 68 61 76 6F 63 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 63 68 61 6F 73 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 73 74 61 6D 69 6E 61 00 00 00 00 00 20 A1 07 00 00 00 00 00 ");
            // Update Status
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 2C 01 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 05 00 50 00 2A 00 00 00 1E 00 00 00 2C 00 1F 00 06 00 00 00 02 00 06 00 06 00 00 00 78 00 69 00 2C 00 3E 08 64 00 64 00 02 00 05 00 32 00 05 00 32 00 00 ");
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 ");
            //// Property
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 68 61 76 6F 63 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 63 68 61 6F 73 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 73 74 61 6D 69 6E 61 00 00 00 00 00 20 A1 07 00 00 00 00 00 ");
            //// Update Status
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 2C 01 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 05 00 50 00 2A 00 00 00 1E 00 00 00 2C 00 1F 00 06 00 00 00 02 00 06 00 06 00 00 00 78 00 69 00 2C 00 3E 08 64 00 64 00 02 00 05 00 32 00 05 00 32 00 00 ");
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 ");
            //// Property
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 68 61 76 6F 63 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 63 68 61 6F 73 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 73 74 61 6D 69 6E 61 00 00 00 00 00 20 A1 07 00 00 00 00 00 ");
            //// x0002
            //Send(player, "0B 00 00 00 02 00 00 2E 3A 01 00 ");
            // Login Result - 0x0004
            //Send(player, "64 00 00 00 04 00 00 01 00 06 00 80 00 66 24 48 00 68 58 47 00 00 00 00 01 00 00 00 00 B4 00 00 00 40 01 00 00 40 01 40 01 00 00 40 01 00 00 00 00 00 00 00 00 02 00 00 00 05 00 00 00 80 80 81 00 D2 00 00 00 C0 02 00 00 54 61 72 74 61 72 75 73 00 00 00 00 00 00 00 00 00 00 00 06 00 00 00 00 00 00 00 ");

            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 2C 01 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 0A 00 05 00 50 00 2A 00 00 00 1E 00 00 00 2C 00 1F 00 06 00 00 00 02 00 06 00 06 00 00 00 78 00 69 00 2C 00 3E 08 64 00 64 00 02 00 05 00 32 00 05 00 32 00 00 ");
            //Send(player, "4E 00 00 00 E8 03 00 00 06 00 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 ");

            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 68 61 76 6F 63 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 63 68 61 6F 73 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 73 74 61 6D 69 6E 61 00 00 00 00 00 20 A1 07 00 00 00 00 00 ");

            // Inventory List - 0x00CF
            //Send(player, "FC 00 00 00 CF 00 00 03 00 03 40 06 00 BC 92 01 00 12 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 04 40 06 00 DD 82 03 00 13 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 00 02 00 00 00 05 40 06 00 11 7A 07 00 14 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 17 00 00 00 00 00 03 00 00 00 ");
            // Equip Summon - 0x012F
            //Send(player, "20 00 00 00 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // Wear Info - 0x00CA
            //Send(player, "43 01 00 00 CA 00 00 00 06 00 80 BC 92 01 00 00 00 00 00 DD 82 03 00 00 00 00 00 91 01 00 00 F5 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 11 7A 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // TS_SC_GOLD_UPDATE - 0x03E9
            //Send(player, "13 00 00 00 E9 03 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // Property - chaos
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 63 68 61 6F 73 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // TS_SC_LEVEL_UPDATE - 0x03EA
            //Send(player, "13 00 00 00 EA 03 00 00 06 00 80 01 00 00 00 01 00 00 00 ");
            // Update EXP - 0x03EB
            //Send(player, "17 00 00 00 EB 03 00 00 06 00 80 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // Property
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6F 62 00 00 00 00 00 00 00 00 00 00 00 00 00 2C 01 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6F 62 5F 6C 65 76 65 6C 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6F 62 5F 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6C 76 5F 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6F 62 5F 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6C 76 5F 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6F 62 5F 32 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6A 6C 76 5F 32 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // Belt Slot Info - 0x00D8
            //Send(player, "1F 00 00 00 D8 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            // gAME tIME - 0X044D
            //Send(player, "13 00 00 00 4D 04 00 3E 3A 01 00 14 3C 21 56 00 00 00 00 ");
            // PROPERTY
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 68 75 6E 74 61 68 6F 6C 69 63 5F 65 6E 74 00 00 0C 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 64 6B 5F 63 6F 75 6E 74 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 70 6B 5F 63 6F 75 6E 74 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 69 6D 6D 6F 72 61 6C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 73 74 61 6D 69 6E 61 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 6D 61 78 5F 73 74 61 6D 69 6E 61 00 00 00 00 00 20 A1 07 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 63 68 61 6E 6E 65 6C 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 ");

            // TS_SC_STATUS_CHANGE
            //Send(player, "0F 00 00 00 F4 01 00 00 06 00 80 00 00 00 00 ");

            // QUEST LIST - 0X258
            //Send(player, "0B 00 00 00 58 02 00 00 00 00 00 ");
            // SCRIPTS - 0X0016
            //Send(player, "26 00 00 00 16 00 00 40 46 52 49 45 4E 44 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 8C 46 4C 49 53 54 7C 00 ");
            //Send(player, "26 00 00 00 16 00 00 40 46 52 49 45 4E 44 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 8C 44 4C 49 53 54 7C 00 ");
            // PROPERTY
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 70 6C 61 79 74 69 6D 65 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 70 6C 61 79 74 69 6D 65 5F 6C 69 6D 69 74 31 00 C0 7A 10 00 00 00 00 00 ");
            //Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 70 6C 61 79 74 69 6D 65 5F 6C 69 6D 69 74 32 00 40 77 1B 00 00 00 00 00 ");
            // TS_SC_CHANGE_LOCATION - 0X0385
            //Send(player, "0F 00 00 00 85 03 00 00 00 00 00 CE 87 01 00 ");
			// WEATHER INFO - 0X0386
			//Send(player, "0D 00 00 00 86 03 00 CE 87 01 00 01 00 ");
			// PROPERTY - ??
			//Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 70 6C 61 79 74 69 6D 65 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ");
			// UNKNOWN - 0X2713
//			Send(player, "0B 00 00 00 13 27 00 00 00 00 00 ");
			// PROPERTY
			//Send(player, "47 00 00 00 FB 01 00 00 06 00 80 00 63 6C 69 65 6E 74 5F 69 6E 66 6F 00 00 00 00 00 00 00 00 00 00 00 00 00 51 53 3D 30 2C 30 2C 32 2C 30 7C 51 53 3D 30 2C 31 2C 32 2C 32 7C 51 53 3D 30 2C 31 31 2C 32 2C 31 7C 00 ");
			//Send(player, "24 00 00 00 FB 01 00 00 06 00 80 01 73 74 61 6D 69 6E 61 5F 72 65 67 65 6E 00 00 00 1E 00 00 00 00 00 00 00 ");
			// 0X0002
			//Send(player, "0B 00 00 00 02 00 00 11 3B 01 00 ");

		}

		/// <summary>
		/// Function to send place-holder packets (bytes from string)
		/// </summary>
		/// <param name="player"></param>
		/// <param name="p"></param>
		private static void Send(Player player, string p)
		{
			String[] arr = p.Trim().Split(' ');
			byte[] buffer = new byte[arr.Length];
			for (int i = 0; i < arr.Length; i++) buffer[i] = Convert.ToByte(arr[i], 16);

			ClientManager.Instance.Send(player, new PacketStream(buffer), BroadcastArea.Self);
		}
	}
}
