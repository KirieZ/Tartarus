CREATE TABLE IF NOT EXISTS `login` (
  `account_id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(60) NOT NULL,
  `password` varchar(32) NOT NULL,
  `permission` tinyint(4) NOT NULL,
  PRIMARY KEY (`account_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;

CREATE TABLE IF NOT EXISTS `otp` (
  `account_id` int(11) NOT NULL,
  `otp` varchar(25) NOT NULL
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;