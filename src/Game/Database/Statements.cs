using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Database
{
    public class Statements
    {
        public static Dictionary<int, string> Arcadia;
        public static Dictionary<int, string> Player;

        public static void Init()
        {
            Arcadia = new Dictionary<int, string>();
            Arcadia.Add(0, "SELECT * FROM AuctionCategoryResource ORDER BY category_id, sub_category_id");
            Arcadia.Add(1, "SELECT * FROM AutoAuctionResource");
            Arcadia.Add(2, "SELECT * FROM CreatureLevelBonus");
            Arcadia.Add(3, "SELECT * FROM DropGroupResource");
            Arcadia.Add(4, "SELECT * FROM EventAreaResource");
            Arcadia.Add(5, "SELECT * FROM ItemResource");
            Arcadia.Add(6, "SELECT * FROM ItemEffectResource ORDER BY id, ordinal_id");
            Arcadia.Add(7, "SELECT * FROM JobLevelBonus");
            Arcadia.Add(8, "SELECT * FROM JobResource");
            Arcadia.Add(9, "SELECT * FROM LevelResource");
            Arcadia.Add(10, "SELECT * FROM MarketResource");
            Arcadia.Add(11, "SELECT * FROM MonsterResource");
            Arcadia.Add(12, "SELECT * FROM MonsterDropTableResource ORDER BY id, sub_id");
            Arcadia.Add(13, "SELECT * FROM MonsterSkillResource ORDER BY id, sub_id");
            Arcadia.Add(14, "SELECT * FROM QuestLinkResource");
            Arcadia.Add(15, "SELECT * FROM QuestResource");
            Arcadia.Add(16, "SELECT * FROM RandomPoolResource");
            Arcadia.Add(17, "SELECT * FROM SetItemEffectResource");
            Arcadia.Add(18, "SELECT * FROM SkillResource");
            Arcadia.Add(19, "SELECT * FROM SkillJPResource");
            Arcadia.Add(20, "SELECT * FROM SkillTreeResource");
            Arcadia.Add(21, "SELECT * FROM StatResource");
            Arcadia.Add(22, "SELECT * FROM SummonDefaultNameResource");
            Arcadia.Add(23, "SELECT * FROM SummonLevelResource");
            Arcadia.Add(24, "SELECT * FROM SummonUniqueNameResource");
            Arcadia.Add(25, "SELECT * FROM StringResource");
            Arcadia.Add(26, "SELECT * FROM SummonResource");
        }
    }
}
