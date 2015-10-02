CREATE TABLE IF NOT EXISTS `AutoAuctionResource`(
	`id` int(11) NOT NULL,
	`item_id` int(11) NOT NULL,
	`auctionseller_id` int(11) NOT NULL,
	`price` bigint(20) NOT NULL,
	`secroute_apply` tinyint(4) unsigned NOT NULL,
	`local_flag` int(11) NOT NULL,
	`auction_enrollment_time` datetime NOT NULL,
	`repeat_apply` tinyint(4) unsigned NOT NULL,
	`repeat_term` int(11) NOT NULL,
	`auctiontime_type` smallint(6) NOT NULL
)ENGINE=MyISAM;

REPLACE INTO `AutoAuctionResource` VALUES ('1', '102501', '180000001', '5000000', '1', '16383', '2007-11-15 17:00:00.000', '0', '0', '1');
REPLACE INTO `AutoAuctionResource` VALUES ('2', '101201', '180000002', '1000', '0', '16383', '2007-11-15 17:00:00.000', '0', '0', '2');
REPLACE INTO `AutoAuctionResource` VALUES ('3', '101202', '180000003', '300', '1', '16383', '2007-11-15 18:00:00.000', '0', '0', '1');
REPLACE INTO `AutoAuctionResource` VALUES ('4', '101203', '180000004', '2000', '0', '16383', '2008-12-05 10:15:00.000', '0', '0', '1');
REPLACE INTO `AutoAuctionResource` VALUES ('5', '540010', '180000005', '100000', '0', '16382', '2008-12-04 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('6', '540013', '180000006', '100000', '0', '16382', '2008-12-05 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('7', '540036', '180000007', '100000', '0', '16382', '2008-12-06 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('8', '540037', '180000008', '100000', '0', '16382', '2008-12-07 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('9', '540010', '180000009', '100000', '0', '16382', '2008-12-08 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('10', '540013', '180000010', '100000', '0', '16382', '2008-12-09 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('11', '540036', '180000011', '100000', '0', '16382', '2008-12-10 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('12', '540037', '180000012', '100000', '0', '16382', '2008-12-11 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('13', '540010', '180000013', '100000', '0', '16382', '2008-12-12 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('14', '540013', '180000014', '100000', '0', '16382', '2008-12-13 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('15', '540036', '180000015', '100000', '0', '16382', '2008-12-14 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('16', '540037', '180000016', '100000', '0', '16382', '2008-12-15 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('17', '540010', '180000017', '100000', '0', '16382', '2008-12-16 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('18', '540013', '180000018', '100000', '0', '16382', '2008-12-17 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('19', '540036', '180000019', '100000', '0', '16382', '2008-12-18 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('20', '540037', '180000020', '100000', '0', '16382', '2008-12-19 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('21', '540010', '180000021', '100000', '0', '16382', '2008-12-20 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('22', '540013', '180000022', '100000', '0', '16382', '2008-12-21 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('23', '540036', '180000023', '100000', '0', '16382', '2008-12-22 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('24', '540037', '180000024', '100000', '0', '16382', '2008-12-23 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('25', '540010', '180000025', '100000', '0', '16382', '2008-12-24 21:00:00.000', '0', '0', '3');
REPLACE INTO `AutoAuctionResource` VALUES ('26', '540013', '180000026', '100000', '0', '16382', '2008-12-25 21:00:00.000', '0', '0', '3');