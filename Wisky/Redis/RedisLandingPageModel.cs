using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Redis
{
    class RedisLandingPageModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PageTitle { get; set; }
        public string LoginText { get; set; }
        public string WelcomeText { get; set; }
        public string LogoUrl { get; set; }
        public string BackgroundMobileUrl { get; set; }
        public string BackgroundUrl { get; set; }
        public Nullable<int> LoginMode { get; set; }
        public int AdMode { get; set; }
        public bool IsEnableSuccessPage { get; set; }
        public string SuccessPageUrl { get; set; }
        public int TemplateId { get; set; }
        public string TemplateFolderUrl { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string AuthorizePassword { get; set; }
        public int SessionDuration { get; set; }
        public int BandwidthClickThrough { get; set; }
        public int BandwidthPassword { get; set; }
        public int BandwidthGoogle { get; set; }
        public int BandwidthFacebook { get; set; }
        public int BandwidthUserInfo { get; set; }
        public int BandwidthUserNamePassword { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DayParting { get; set; }
        public Nullable<int> HourParting { get; set; }
        public Nullable<int> DeviceFilter { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> IsEnableReturnLogin { get; set; }
        public Nullable<int> ReturnLoginMode { get; set; }
        public string ReturnLoginText { get; set; }
        public Nullable<int> ReturnBandwidthClickThrough { get; set; }
        public Nullable<int> ReturnBandwidthPassword { get; set; }
        public Nullable<int> ReturnBandwidthFacebook { get; set; }
        public Nullable<int> ReturnBandwidthGoogle { get; set; }
        public string ReturnAuthorizePassword { get; set; }
        public bool Active { get; set; }
        public string UserPassJson { get; set; }
        public Nullable<int> EnableDefaultPassword { get; set; }
        public Nullable<int> LanguageFilter { get; set; }
        public string ReturnWelcomeText { get; set; }
        public Nullable<int> RegisterMode { get; set; }
        public Nullable<int> BanMode { get; set; }
        public Nullable<int> MembershipFieldFilter { get; set; }
        public Nullable<int> FixAdvertisingId { get; set; }
        public Nullable<int> SkipToLandingPageId { get; set; }
        public Nullable<int> HourSkipping { get; set; }
    }
}
