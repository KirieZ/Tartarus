using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game.Database
{
    public class Statements
    {
        public static void Init()
        {
            Dictionary<int,string> Game = new Dictionary<int, string>();
            Game.Add(0, "SELECT * FROM AuctionCategoryResource ORDER BY category_id, sub_category_id");
            Game.Add(1, "SELECT * FROM AutoAuctionResource");
            Game.Add(2, "SELECT * FROM CreatureLevelBonus");
            Game.Add(3, "SELECT * FROM DropGroupResource");
            Game.Add(4, "SELECT * FROM EventAreaResource");
            Game.Add(5, "SELECT * FROM ItemResource");
            Game.Add(6, "SELECT * FROM ItemEffectResource ORDER BY id, ordinal_id");
            Game.Add(7, "SELECT * FROM JobLevelBonus");
            Game.Add(8, "SELECT * FROM JobResource");
            Game.Add(9, "SELECT * FROM LevelResource");
            Game.Add(10, "SELECT * FROM MarketResource");
            Game.Add(11, "SELECT * FROM MonsterResource");
            Game.Add(12, "SELECT * FROM MonsterDropTableResource ORDER BY id, sub_id");
            Game.Add(13, "SELECT * FROM MonsterSkillResource ORDER BY id, sub_id");
            Game.Add(14, "SELECT * FROM QuestLinkResource");
            Game.Add(15, "SELECT * FROM QuestResource");
            Game.Add(16, "SELECT * FROM RandomPoolResource");
            Game.Add(17, "SELECT * FROM SetItemEffectResource");
            Game.Add(18, "SELECT * FROM SkillResource");
            Game.Add(19, "SELECT * FROM SkillJPResource");
            Game.Add(20, "SELECT * FROM SkillTreeResource");
            Game.Add(21, "SELECT * FROM StatResource");
            Game.Add(22, "SELECT * FROM SummonDefaultNameResource");
            Game.Add(23, "SELECT * FROM SummonLevelResource");
            Game.Add(24, "SELECT * FROM SummonUniqueNameResource");
            Game.Add(25, "SELECT * FROM StringResource");
            Game.Add(26, "SELECT * FROM SummonResource");
            Game.Add(27, "SELECT * FROM StateResource");

            Dictionary<int, string>  User = new Dictionary<int, string>();
            // Lobby
            User.Add(0, "SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now ORDER BY char_id ASC LIMIT "+Globals.MaxCharacters);
            User.Add(1, "SELECT char_id FROM Characters WHERE name = @name AND delete_date > @now");
            if (Settings.SqlEngine == 1) // MySQL
                User.Add(2, "INSERT INTO Characters (account_id, name, race, sex, job, level, x, y, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color) VALUES (@accId, @name, @race, @sex, @job, @level, @x, @y, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor); SELECT last_insert_id()");
            else // SqlSrv
                User.Add(2, "INSERT INTO Characters (account_id, name, race, sex, job, level, x, y, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color) VALUES (@accId, @name, @race, @sex, @job, @level, @x, @y, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor)");
            User.Add(3, "UPDATE Characters SET delete_date = @now WHERE account_id = @accId AND name = @name");
            User.Add(4, "DELETE FROM Characters WHERE account_id = @accId AND name = @name");
            User.Add(5, "SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now AND name = @name");
            User.Add(6, "SELECT wear_info, code, enhance, level, elemental_effect_type FROM Item WHERE owner_id = @charId AND wear_info >=  0");
            User.Add(7, "UPDATE Characters SET login_time = NOW(), login_count = login_count + 1 WHERE char_id = @cid");

            // Item
            User.Add(20, "SELECT * FROM Item WHERE owner_id = @charId");
            User.Add(21, "INSERT INTO Item (owner_id, idx, code, cnt, level, enhance, durability, endurance, flag, gcode, wear_info, socket_0, socket_1, socket_2, socket_3, remain_time, elemental_effect_type, elemental_effect_expire_time, elemental_effect_attack_point, elemental_effect_magic_point, create_time) VALUES (@owner_id, @idx, @code, @cnt, @level, @enhance, @durability, @endurance, @flag, @gcode, @wear_info, @socket_0, @socket_1, @socket_2, @socket_3, @remain_time, @elemental_effect_type, @elemental_effect_expire_time, @elemental_effect_attack_point, @elemental_effect_magic_point, @create_time)");

            DBManager.SetStatements(null, Game, User);
        }
    }
}
