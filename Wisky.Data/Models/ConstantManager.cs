using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Data.Models
{
    public static class ConstantDataManager
    {
        public const int STT_SUCCESS = 0;
        public const int STT_FAIL = 1;
        public const int STT_MISSING_PARAM = 2;
        public const int STT_UNAUTHORIZED = 3;

        public const string MES_LOGIN_SUCCESS = "Login successfully";
        public const string MES_LOGIN_FAIL = "Login fail";
        public const string MES_MISSING_PARAM = "Missing parameter";
        public const string MES_UNVALID_TOKEN = "Token is unvalid or expired";
        //check role khi xem store
        public const string MES_STORE_UNAUTHENTICATED = "You do not have permission to see report of this store";
        // check role khi nhan token
        public const string MES_ROLE_UNAUTHENTICATED = "You do not have permission";
        public const string MES_LOAD_REPORT_SUCCESS = "Load report success";

        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_MANAGER = "BrandManager";


        public const int DefaultLimitBandwidth = -1; //Max Bandwidth
        public const int LimitBandwidthLevel1 = -1; //Max Bandwidth
        public const int LimitBandwidthLevel2 = -1; //Max Bandwidth

    }
}
