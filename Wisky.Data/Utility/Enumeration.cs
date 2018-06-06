using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wisky.Data.Utility
{
    public enum LoginModeEnum
    {
        ClickThrough = 8,

        #region Password
        Password = 1, //FixedPassword     
        OneTimePassword = 2048, // Password by location
        RandomPassword = 32, // Password random by 
        #endregion

        #region Open Authentication
        Facebook = 2,//Facebook Login
        GooglePlus = 4,
        #endregion

        #region  Smart Account

        AccountWithPass = 16,
        RegisterInformation = 32768,//2^15- Nhap username va pass- check trong database- Auto detect user by Mac Address

        #endregion


        //EnterPhoneNumber = 8192, // 2^13
        // EnterEmail = 16384, // 2^14

        // Membership = 64,//Nhập username và pass - check trong database

        SMS = 128, //Do SMS
        // Email = 256,

        ByPassLoginPage = 512, //Bỏ qua trang Login Page
        ByPassAll = 1024 //Chỉ lưu thông tin đăng nhập và đi thẳng qua hệ thống
    }


    public enum AdFormatEnum
    {
        //AppInstall = 7,
        //AdTextArr get về chỉ có 6 string, trong đó AppInstall ở vị trí 0, nhưng trong enum lại là 7?
        AppInstall = 0,
        ClickWebsite = 1,
        LikeFacebook = 2,
        GetOffer = 3,
        DoSurvey = 4,
        Video = 5,
        //TODO: 6 Adtype,
        PageInject = 7,
        RealTimeBidding = 8,
    }
    public enum DeviceOsEnum
    {
        Unknown = 0,
        Windows = 1,
        Linux = 2,
        Mac = 4,
        Ipad = 8,
        Iphone = 16,
        Android = 32

    }

    public enum LandingAdMode
    {
        NoAdvertising,//Khong hien thi quang cao
        Required, //Bat buoc phai nhan nut Action o  Step 2 (Chi hien 1 nut)
        Optional, //O Step2  hien 2 nut (1 nut bat buoc, 1 nut optional)
    }

    //public enum OperatingSystemEnum
    //{
    //    Android = 0,
    //    iOS = 1,//Mac, Ipad, IPhone
    //    Windows = 2,
    //    Linux = 3,
    //    Unknown = 4
    //}
    public enum DeviceTypeEnum
    {
        PC = 1,
        Mobile = 2,
        All = 3
    }

    public enum AccessPointTypeEnum
    {
        Mikrotik = 1,
        Unifi = 2,
        IgniteNet = 3,
        Other = 4,
    }

    public enum AdStatusEnum
    {
        New = 1,
        Approved = 2,
        Pending = 4,
        Running = 8,
        Canceled = 16,
        Finished = 32
    }

    public enum IdentifiedAdStatusEnum
    {
        New = 1,
        Used = 2
    }
    public enum BrowserEnum
    {
        Firefox = 0,
        Chrome = 1,
        IE = 2,
        Safari = 3,
        Opera = 4,
        Edge = 5,
        Unkown = 6
    }

    public enum GenderEnum
    {
        Female = 0,
        Male = 1,
        Other = 2,
    }

    public enum DayOfWeekEnum
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
    }
    public enum PriorityEnum
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Special = 3,
        APG = 4,
        Constract = 5,
    }

    public enum SocialLoginType
    {
        None = 0,
        Facebook = 1,
        GooglePlus = 2
    }

    public enum HourPartingEnum
    {
        From0To2 = 0,
        From2To4 = 1,
        From4To6 = 2,
        From6To8 = 3,
        From8To10 = 4,
        From10To12 = 5,
        From12To14 = 6,
        From14To16 = 7,
        From16To18 = 8,
        From18To20 = 9,
        From20To22 = 10,
        From22To24 = 11
    }

    //public enum AdModeEnum
    //{
    //    NoAdvertising = 0,
    //    Optional = 1
    //}

    public enum BrandTypeEnum
    {
        Single = 1,
        Chain = 2,
        Event = 3
    }

    public enum UserAccountTypeEnum
    {
        Facebook = 0,
        Gmail = 1,
        MacAddress = 2,
        //User được cấp tài khoản
        NormalUser = 3
    }

    public enum ActivityTypeEnum
    {
        Text = 0,
        Widget = 1
    }

    public enum WidgetTypeEnum
    {
        SurveySingleChoice = 0,
        SurveyMultipleChoice = 1,
        Collection = 2,
        Hyperlink = 3,
        SlideShow = 4,
        Video = 5
    }

    public enum DefaultPasswordEnum
    {
        [Display(Name = "True")]
        True = 1,
        [Display(Name = "False")]
        False = 2
    }

    public enum TemplateTypeEnum
    {
        [Display(Name = "Interactive Background")]
        Default1 = 1,
        [Display(Name = "Bottom Form")]
        BottomForm = 25,
        [Display(Name = "Static Background")]
        Default4 = 21,
        [Display(Name = "Custom Form")]
        CustomForm = 26,
        [Display(Name = "General Form")]
        GeneralForm = 27,
        [Display(Name = "Static Background English")]
        StaticBackgroundEnglish = 28,
        [Display(Name = "Name A Bank Form")]
        NamABank = 29,
        [Display(Name = "Final Material Form")]
        FinalMaterialForm = 30,
    }


    public enum LanguageCodeEnum
    {
        English = 0,
        Vietnam = 1,
        Russian = 2,
        Chinese = 3,
        Japanese = 4
    }

    public enum RegisterModeEnum
    {
        NoRegister = 0,
        RegisterWithoutApprove = 1,
        RegisterWithApprove = 2
    }

    public enum RegisterAttrEnum
    {
        RegisterName = 1,
        RegisterEmail = 2,
        RegisterPhone = 4,
        RegisterOrganization = 8,
        RegisterJob = 16,
        RegisterPosition = 32,
        RegisterAttribute1 = 64,
        RegisterAttribute2 = 128,
        RegisterAttribute3 = 256,
        RegisterAttribute4 = 512,
        RegisterAttribute5 = 1024,
        RegisterBirthDay = 2048,
        RegisterAddress = 4096
    }

    public enum MembershipFieldEnum
    {
        Username = 1,
        Phone = 2,
        Email = 4,
        Fullname = 8,
        Password = 16
    }

    public enum GetPasswordTypeEnum
    {
        [Display(Name = "SMS")]
        SMS = 1,
        [Display(Name = "Email")]
        Email = 2,
        [Display(Name = "Manual")]
        Manual = 3
    }

    public enum ManageMode
    {
        [Display(Name = "View Report")]
        ViewReport = 0,
        [Display(Name = "Full Control")]
        FullControl = 1
    }

    //Do uu tien khi hien thi select work category
    public enum WorkOrder
    {
        [Display(Name = "High")]
        High = 1,
        [Display(Name = "Moderate")]
        Moderate = 2,
        [Display(Name = "Low")]
        Low = 3
    }

    //Tinh trang hoan thanh cong viec cua employee
    public enum WorkingStatus
    {
        [Display(Name = "Done")]
        Done = 0,
        [Display(Name = "Working")]
        Working = 1,
        [Display(Name = "Canceled")]
        Canceled = 2
    }

    public enum CustomerFilterEnum
    {
        Male = 1,
        Female = 2,
        Others = 4,
        ClickWebsite = 8,
        Video = 16,
        PageInject = 32
    }

    public enum MikActionTask
    {
        ProxyAction = 1,
        WifiMarketingAction = 2,
        ChangePassAction = 3,
        HotspotAction = 4
    }

    public enum UploadCDNEnum
    {
        [Display(Name = "Blob")]
        Blob = 1,
        [Display(Name = "CDN Wisky")]
        CDNWiksy = 2
    }

    #region Email Marketing
    public enum CampaignMarketingStatus
    {
        [Display(Name = "Created")]
        Created = 0,
        [Display(Name = "Sending")]
        Sending = 1,
        [Display(Name = "Complete")]
        Complete = 2,
        [Display(Name = "FirstTime")]
        FirstTime = 3,
        [Display(Name = "RepeatCustomer")]
        RepeatCustomer = 4,
        [Display(Name = "LostCustomer")]
        LostCustomer = 5,
        [Display(Name = "LoyalCustomer")]
        LoyalCustomer = 6,
        [Display(Name = "Birthday")]
        Birthday = 7,
        [Display(Name = "Aniversary")]
        Aniversary = 8,
    }

    public enum SendModeCampaign
    {
        [Display(Name = "Manual")]
        Manual = 0,
        [Display(Name = "Auto")]
        Auto = 1,
    }

    public enum UserAccessStatus
    {
        [Display(Name = "Waiting")]
        Waiting = 0,
        [Display(Name = "Sent")]
        Sent = 1,
    }

    public enum TemplateEmailStatus
    {
        [Display(Name = "Create")]
        Create = 0,
        [Display(Name = "Update")]
        Update = 1,
        [Display(Name = "Complete")]
        Complete = 2,
    }

    #endregion Email Marketing
}