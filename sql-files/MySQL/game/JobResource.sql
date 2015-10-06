CREATE TABLE IF NOT EXISTS `JobResource` (
	`id` int(11) NOT NULL,
	`text_id` int(11) NOT NULL,
	`stati_id` int(11) NOT NULL,
	`job_class` int(11) NOT NULL,
	`job_depth` tinyint(4) unsigned NOT NULL,
	`up_lv` smallint(6) NOT NULL,
	`up_jlv` smallint(6) NOT NULL,
	`available_job_0` smallint(6) NOT NULL,
	`available_job_1` smallint(6) NOT NULL,
	`available_job_2` smallint(6) NOT NULL,
	`available_job_3` smallint(6) NOT NULL,
	`icon_id` int(11) NOT NULL,
	`icon_file_name` varchar(256) NOT NULL
) ENGINE=MyISAM;

REPLACE INTO `JobResource` VALUES ('100', '10100', '100', '1', '0', '10', '10', '101', '102', '103', '-1', '40000', 'icon_job_0018');
REPLACE INTO `JobResource` VALUES ('101', '10101', '101', '1', '1', '50', '40', '110', '111', '-1', '-1', '40000', 'icon_job_0019');
REPLACE INTO `JobResource` VALUES ('102', '10102', '102', '3', '1', '50', '40', '112', '113', '-1', '-1', '40000', 'icon_job_0020');
REPLACE INTO `JobResource` VALUES ('103', '10103', '103', '4', '1', '50', '40', '114', '-1', '-1', '-1', '40000', 'icon_job_0026');
REPLACE INTO `JobResource` VALUES ('110', '10110', '110', '1', '2', '100', '50', '120', '-1', '-1', '-1', '40000', 'icon_job_0022');
REPLACE INTO `JobResource` VALUES ('111', '10111', '111', '2', '2', '100', '50', '121', '-1', '-1', '-1', '40000', 'icon_job_0023');
REPLACE INTO `JobResource` VALUES ('112', '10112', '112', '3', '2', '100', '50', '122', '-1', '-1', '-1', '40000', 'icon_job_0024');
REPLACE INTO `JobResource` VALUES ('113', '10113', '113', '3', '2', '100', '50', '123', '-1', '-1', '-1', '40000', 'icon_job_0031');
REPLACE INTO `JobResource` VALUES ('114', '10114', '114', '4', '2', '100', '50', '124', '-1', '-1', '-1', '40000', 'icon_job_0032');
REPLACE INTO `JobResource` VALUES ('120', '10120', '120', '1', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0027');
REPLACE INTO `JobResource` VALUES ('121', '10121', '121', '2', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0029');
REPLACE INTO `JobResource` VALUES ('122', '10122', '122', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0028');
REPLACE INTO `JobResource` VALUES ('123', '10123', '123', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0030');
REPLACE INTO `JobResource` VALUES ('124', '10124', '124', '4', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0021');
REPLACE INTO `JobResource` VALUES ('200', '10200', '200', '1', '0', '10', '10', '201', '202', '203', '-1', '40000', 'icon_job_0033');
REPLACE INTO `JobResource` VALUES ('201', '10201', '201', '1', '1', '50', '40', '210', '211', '-1', '-1', '40001', 'icon_job_0002');
REPLACE INTO `JobResource` VALUES ('202', '10202', '202', '3', '1', '50', '40', '212', '213', '-1', '-1', '40002', 'icon_job_0003');
REPLACE INTO `JobResource` VALUES ('203', '10203', '203', '4', '1', '50', '40', '214', '-1', '-1', '-1', '40003', 'icon_job_0004');
REPLACE INTO `JobResource` VALUES ('210', '10210', '210', '1', '2', '100', '50', '220', '-1', '-1', '-1', '40004', 'icon_job_0034');
REPLACE INTO `JobResource` VALUES ('211', '10211', '211', '1', '2', '100', '50', '221', '-1', '-1', '-1', '40005', 'icon_job_0035');
REPLACE INTO `JobResource` VALUES ('212', '10212', '212', '3', '2', '100', '50', '222', '-1', '-1', '-1', '40006', 'icon_job_0006');
REPLACE INTO `JobResource` VALUES ('213', '10213', '213', '3', '2', '100', '50', '223', '-1', '-1', '-1', '40007', 'icon_job_0007');
REPLACE INTO `JobResource` VALUES ('214', '10214', '214', '4', '2', '100', '50', '224', '-1', '-1', '-1', '40008', 'icon_job_0009');
REPLACE INTO `JobResource` VALUES ('220', '10220', '220', '1', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40008', 'icon_job_0005');
REPLACE INTO `JobResource` VALUES ('221', '10221', '221', '1', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0038');
REPLACE INTO `JobResource` VALUES ('222', '10222', '222', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0008');
REPLACE INTO `JobResource` VALUES ('223', '10223', '223', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0037');
REPLACE INTO `JobResource` VALUES ('224', '10224', '224', '4', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0039');
REPLACE INTO `JobResource` VALUES ('300', '10300', '300', '1', '0', '10', '10', '301', '302', '303', '-1', '40000', 'icon_job_0040');
REPLACE INTO `JobResource` VALUES ('301', '10301', '301', '1', '1', '50', '40', '310', '311', '-1', '-1', '40009', 'icon_job_0010');
REPLACE INTO `JobResource` VALUES ('302', '10302', '302', '3', '1', '50', '40', '312', '313', '-1', '-1', '40010', 'icon_job_0011');
REPLACE INTO `JobResource` VALUES ('303', '10303', '303', '4', '1', '50', '40', '314', '-1', '-1', '-1', '40011', 'icon_job_0012');
REPLACE INTO `JobResource` VALUES ('310', '10310', '310', '1', '2', '100', '50', '320', '-1', '-1', '-1', '40012', 'icon_job_0013');
REPLACE INTO `JobResource` VALUES ('311', '10311', '311', '2', '2', '100', '50', '321', '-1', '-1', '-1', '40013', 'icon_job_0014');
REPLACE INTO `JobResource` VALUES ('312', '10312', '312', '3', '2', '100', '50', '322', '-1', '-1', '-1', '40014', 'icon_job_0015');
REPLACE INTO `JobResource` VALUES ('313', '10313', '313', '3', '2', '100', '50', '323', '-1', '-1', '-1', '40015', 'icon_job_0016');
REPLACE INTO `JobResource` VALUES ('314', '10314', '314', '4', '2', '100', '50', '324', '-1', '-1', '-1', '40016', 'icon_job_0017');
REPLACE INTO `JobResource` VALUES ('320', '10320', '320', '1', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40016', 'icon_job_0041');
REPLACE INTO `JobResource` VALUES ('321', '10321', '321', '2', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0042');
REPLACE INTO `JobResource` VALUES ('322', '10322', '322', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0043');
REPLACE INTO `JobResource` VALUES ('323', '10323', '323', '3', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0044');
REPLACE INTO `JobResource` VALUES ('324', '10324', '324', '4', '3', '-1', '-1', '-1', '-1', '-1', '-1', '40000', 'icon_job_0045');
REPLACE INTO `JobResource` VALUES ('204', '10204', '204', '1', '1', '50', '40', '215', '-1', '-1', '-1', '0', 'icon_job_0204');
REPLACE INTO `JobResource` VALUES ('215', '10215', '215', '2', '2', '100', '50', '-1', '-1', '-1', '-1', '0', 'icon_job_0215');
