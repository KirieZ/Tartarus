CREATE TABLE IF NOT EXISTS `Characters` (
  `char_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL default '0',
  `slot` int(11) NOT NULL default '0',
  `name` varchar(20) NOT NULL default '0',
  `party_id` int(11) NOT NULL default '0',
  `guild_id` int(11) NOT NULL default '0',
  `prev_guild_id` int(11) NOT NULL default '0',
  `x` int(11) NOT NULL default '0',
  `y` int(11) NOT NULL default '0',
  `z` int(11) NOT NULL default '0',
  `layer` tinyint(4) unsigned NOT NULL default '0',
  `race` tinyint(4) unsigned NOT NULL default '0',
  `sex` int(11) NOT NULL default '0',
  `level` int(11) NOT NULL default '0',
  `max_reached_level` int(11) NOT NULL default '0',
  `exp` bigint(20) NOT NULL default '0',
  `last_decreased_exp` bigint(20) NOT NULL default '0',
  `hp` int(11) NOT NULL default '0',
  `mp` int(11) NOT NULL default '0',
  `stamina` int(11) NOT NULL default '0',
  `havoc` int(11) NOT NULL default '0',
  `job` smallint(5) NOT NULL default '0',
  `job_depth` tinyint(4) unsigned NOT NULL default '0',
  `job_level` int(11) NOT NULL default '0',
  `jp` int(11) NOT NULL default '0',
  `total_jp` int(11) NOT NULL default '0',
  `job_0` int(11) NOT NULL default '0',
  `job0_level` int(11) NOT NULL default '0',
  `job_1` int(11) NOT NULL default '0',
  `job1_level` int(11) NOT NULL default '0',
  `job_2` int(11) NOT NULL default '0',
  `job2_level` int(11) NOT NULL default '0',
  `immoral_point` decimal(18, 4) NOT NULL default '0',
  `cha` int(11) NOT NULL default '0',
  `pkc` int(11) NOT NULL default '0',
  `dkc` int(11) NOT NULL default '0',
  `huntaholic_point` int(11) NOT NULL default '0',
  `huntaholic_enter_count` int(11) NOT NULL default '0',
  `gold` bigint(20) NOT NULL default '0',
  `chaos` int(11) NOT NULL default '0',
  `skin_color` int(11) unsigned NOT NULL default '0',
  `hair_id` int(11) NOT NULL default '0',
  `face_id` int(11) NOT NULL default '0',
  `body_id` int(11) NOT NULL default '0',
  `hands_id` int(11) NOT NULL default '0',
  `feet_id` int(11) NOT NULL default '0',
  `texture_id` int(11) NOT NULL default '0',
  `belt_00` bigint(20) NOT NULL default '0',
  `belt_01` bigint(20) NOT NULL default '0',
  `belt_02` bigint(20) NOT NULL default '0',
  `belt_03` bigint(20) NOT NULL default '0',
  `belt_04` bigint(20) NOT NULL default '0',
  `belt_05` bigint(20) NOT NULL default '0',
  `summon_0` int(11) NOT NULL default '0',
  `summon_1` int(11) NOT NULL default '0',
  `summon_2` int(11) NOT NULL default '0',
  `summon_3` int(11) NOT NULL default '0',
  `summon_4` int(11) NOT NULL default '0',
  `summon_5` int(11) NOT NULL default '0',
  `main_summon` int(11) NOT NULL default '0',
  `sub_summon` int(11) NOT NULL default '0',
  `remain_summon_time` int(11) NOT NULL default '0',
  `pet` int(11) NOT NULL default '0',
  `create_date` datetime NOT NULL default 0,
  `delete_date` datetime NOT NULL default '9999-12-31 00:00:00',
  `login_time` datetime NOT NULL default '0000-00-00 00:00:00',
  `login_count` int(11) NOT NULL default '0',
  `logout_time` datetime NOT NULL default '0000-00-00 00:00:00',
  `play_time` int(11) NOT NULL default '0',
  `chat_block_time` int(11) NOT NULL default '0',
  `adv_chat_count` int(11) NOT NULL default '0',
  `name_changed` int(11) NOT NULL default '0',
  `auto_used` int(11) NOT NULL default '0',
  `guild_block_time` int(11) NOT NULL default '0',
  `pkmode` tinyint(4) unsigned NOT NULL default '0',
  `otp_value` int(11) NOT NULL default '0',
  `otp_date` datetime NOT NULL default '0000-00-00 00:00:00',
  `client_info` varchar(4096) NOT NULL default '0',
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;



CREATE TABLE IF NOT EXISTS `Item` (
	`sid` bigint(20) NOT NULL  AUTO_INCREMENT,
	`owner_id` int(11) NOT NULL,
	`account_id` int(11) NOT NULL,
	`summon_id` int(11) NOT NULL,
	`auction_id` int(11) NOT NULL,
	`keeping_id` int(11) NOT NULL,
	`idx` int(11) NOT NULL,
	`code` int(11) NOT NULL,
	`cnt` bigint(20) NOT NULL,
	`level` int(11) NOT NULL,
	`enhance` int(11) NOT NULL,
	`ethereal_durability` int(11) NOT NULL,
	`endurance` int(11) NOT NULL,
	`flag` int(11) NOT NULL,
	`gcode` int(11) NOT NULL,
	`wear_info` int(11) NULL,
	`socket_0` int(11) NOT NULL,
	`socket_1` int(11) NOT NULL,
	`socket_2` int(11) NOT NULL,
	`socket_3` int(11) NOT NULL,
	`remain_time` int(11) NOT NULL,
	`elemental_effect_type` tinyint(4) NOT NULL,
	`elemental_effect_expire_time` datetime NOT NULL,
	`elemental_effect_attack_point` int(11) NOT NULL,
	`elemental_effect_magic_point` int(11) NOT NULL,
	`create_time` datetime NOT NULL,
	`update_time` datetime NOT NULL,
	PRIMARY KEY (`sid`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;