CREATE TABLE [login] (
  [account_id] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [userid] [nvarchar](60) NOT NULL,
  [password] [nvarchar](32) NOT NULL,
  [permission] [tinyint] NOT NULL
);

CREATE TABLE [dbo].[otp](
 [account_id] [int] NOT NULL,
 [otp] [nvarchar](25) NOT NULL
) ON [PRIMARY]