using System;

namespace Game.Database.Structures
{
	public class DB_Monster
	{
		public int id { get; set; }
		public int monster_group { get; set; }
		public int name_id { get; set; }
		public int location_id { get; set; }
		public string model { get; set; }
		public int motion_file_id { get; set; }
		public int transform_level { get; set; }
		public byte walk_type { get; set; }
		public byte slant_type { get; set; }
		public decimal size { get; set; }
		public decimal scale { get; set; }
		public decimal target_fx_size { get; set; }
		public int camera_x { get; set; }
		public int camera_y { get; set; }
		public int camera_z { get; set; }
		public decimal target_x { get; set; }
		public decimal target_y { get; set; }
		public decimal target_z { get; set; }
		public int level { get; set; }
		public int grp { get; set; }
		public int magic_type { get; set; }
		public int race { get; set; }
		public int visible_range { get; set; }
		public int chase_range { get; set; }
		public byte f_first_attack { get; set; }
		public byte f_group_first_attack { get; set; }
		public byte f_response_casting { get; set; }
		public byte f_response_race { get; set; }
		public byte f_response_battle { get; set; }
		public byte monster_type { get; set; }
		public int stat_id { get; set; }
		public int fight_type { get; set; }
		public int monster_skill_link_id { get; set; }
		public int material { get; set; }
		public int weapon_type { get; set; }
		public int attack_motion_speed { get; set; }
		public int ability { get; set; }
		public int standard_walk_speed { get; set; }
		public int standard_run_speed { get; set; }
		public int walk_speed { get; set; }
		public int run_speed { get; set; }
		public decimal attack_range { get; set; }
		public decimal hidesense_range { get; set; }
		public int hp { get; set; }
		public int mp { get; set; }
		public int attack_point { get; set; }
		public int magic_point { get; set; }
		public int defence { get; set; }
		public int magic_defence { get; set; }
		public int attack_speed { get; set; }
		public int magic_speed { get; set; }
		public int accuracy { get; set; }
		public int avoid { get; set; }
		public int magic_accuracy { get; set; }
		public int magic_avoid { get; set; }
		public int taming_id { get; set; }
		public decimal taming_percentage { get; set; }
		public decimal taming_exp_mod { get; set; }
		public int exp { get; set; }
		public int jp { get; set; }
		public int gold_drop_percentage { get; set; }
		public int gold_min { get; set; }
		public int gold_max { get; set; }
		public int chaos_drop_percentage { get; set; }
		public int chaos_min { get; set; }
		public int chaos_max { get; set; }
		public int exp_2 { get; set; }
		public int jp_2 { get; set; }
		public int gold_min_2 { get; set; }
		public int gold_max_2 { get; set; }
		public int chaos_min_2 { get; set; }
		public int chaos_max_2 { get; set; }
		public int drop_table_link_id { get; set; }
		public int texture_group { get; set; }
		public int local_flag { get; set; }
		public string script_on_dead { get; set; }
	}
}