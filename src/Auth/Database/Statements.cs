using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Auth.Database
{
    public class Statements
    {
        public static void Init()
        {
            Dictionary<int, string> auth = new Dictionary<int, string>();
            auth.Add(0, "SELECT * FROM login WHERE userid = @id AND password = @pass");
            auth.Add(1, "SELECT * FROM login WHERE userid = @id");
            auth.Add(2, "SELECT * FROM otp WHERE account_id = @acc");
            auth.Add(3, "DELETE FROM otp WHERE account_id = @acc");
            auth.Add(4, "UPDATE login SET last_serverid = @sid WHERE account_id = @acc");

            DBManager.SetStatements(auth, null, null);
        }
    }
}
