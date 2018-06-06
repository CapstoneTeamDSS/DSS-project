using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wisky.Data.Utility;
using Wisky.Models.LoginMenu;
using static Wisky.Models.LoginMenu.LoginMenuEnums;

namespace Wisky.Ultility
{
    public class GenerateLoginMenu
    {
        public static LoginMenuModel PrepareLoginMenuModel(double x, double y, double xMobile, double yMobile, double seismicIntensity, double width = 35, int selectedLanguage = 0, string backgroundColor = "rgba(255,255,255,0.85)", double widthMobile = 100)
        {
            LoginMenuModel loginMenu = new LoginMenuModel()
            {
                Id = "general-form",
                InlineCss =
                        string.Format("background-color: {0}", backgroundColor),
                SelectedLanguage = selectedLanguage,

                X = x,
                Y = y,

                XMobile = xMobile,
                YMobile = yMobile,

                Width = width,
                WidthMobile = widthMobile,

                SeismicIntensity = seismicIntensity,
                GroupItemsType = MenuType.Vertical,
                CssClass = "general-form",
                CssPosition = "position-bottom",
                GroupItems = new List<GroupItem>(),
            };
            return loginMenu;
        }
        /// <summary>
        /// Generate socical buttons as Google, Facebook, ClickThrough to Login Menu
        /// </summary>
        /// <param name="loginMenu"></param>
        /// <param name="prepareFormModel"></param>
        /// <returns></returns>
        public static LoginMenuModel GenerateClickThroughSocialButtons(LoginMenuModel loginMenu, PrepareFormModel prepareFormModel)
        {
            ControlItem btnClickThrough = CreateControlButton(
                "onSubmit(8)",
                MenuControlType.Button,
                "btnClickThrough",
                "Tiếp tục",
                "fas fa-arrow-alt-circle-right",
                "btn btn-info btn-block");
            ControlItem btnFacebook = CreateControlButton(
               "loginFacebook()",
               MenuControlType.Button,
               "btnFacebook",
               "Facebook",
               "fab fa-facebook-square",
               "btn btn-info btn-facebook btn-block");
            ControlItem btnGooglePlus = CreateControlButton(
               "loginGoogleApp()",
               MenuControlType.Button,
               "btnGoogle",
               "Google Plus",
               "fab fa-google-plus-square",
               "btn btn-info btn-google btn-block");

            GroupItem clickThrough = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.ClickThrough,
                ControlItems = new List<ControlItem>()
                {
                    btnClickThrough
                }
            };

            GroupItem faceBook = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.Facebook,
                ControlItems =
                new List<ControlItem>()
                {
                    btnFacebook
                }
            };

            GroupItem googlePlus = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.GooglePlus,
                ControlItems =
                new List<ControlItem>()
                {
                    btnGooglePlus
                }
            };

            loginMenu.GroupItems.Add(clickThrough);
            loginMenu.GroupItems.Add(faceBook);
            loginMenu.GroupItems.Add(googlePlus);
            return loginMenu;
        }
        public static LoginMenuModel GeneratePasswordButtons(LoginMenuModel loginMenu)
        {

            ControlItem txtFixedPassword = CreateControlTextBox("FixedPassword", "PasswordField", "Mật khẩu", MenuControlType.Password, "", true);
            ControlItem btnFixedPassword = CreateControlButton(
               "validatePassword(\'" + LoginModeEnum.Password + "\', \'FixedPassword\')",
               MenuControlType.Button,
               "btnFixedPassword",
               "Đăng nhập",
               "fas fa-key",
               "btn btn-success btn-block");

            ControlItem txtOneTimePassword = CreateControlTextBox("OneTimePassword", "PasswordField", "Mật khẩu", MenuControlType.Password, "", true);
            ControlItem btnOneTimePassword = CreateControlButton(
               "validatePassword(\'" + LoginModeEnum.OneTimePassword + "\', \'OneTimePassword\')",
               MenuControlType.Button,
               "btnOneTimePassword",
               "Đăng nhập",
               "fas fa-key",
               "btn btn-success btn-block");

            ControlItem txtRandomPassword = CreateControlTextBox("PasswordRandom", "PasswordField", "Mật khẩu", MenuControlType.Password, "", true);
            ControlItem btnRandomPassword = CreateControlButton(
               "validatePassword(\'" + LoginModeEnum.RandomPassword + "\', \'PasswordRandom\')",
               MenuControlType.Button,
               "btnPasswordRandom",
               "Đăng nhập",
               "fas fa-key",
               "btn btn-success btn-block");


            GroupItem passwordButton = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.Password,
                ControlItems = new List<ControlItem>()
                {
                    txtFixedPassword,
                    btnFixedPassword
                }
            };
            loginMenu.GroupItems.Add(passwordButton);
            GroupItem oneTimePasswordButton = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.OneTimePassword,
                ControlItems = new List<ControlItem>()
                {
                    txtOneTimePassword,
                    btnOneTimePassword
                }
            };
            loginMenu.GroupItems.Add(oneTimePasswordButton);
            GroupItem randomPasswordButton = new GroupItem()
            {
                CssClass = "btn-group-vertical",
                Css = "",
                LoginModeCode = (int)LoginModeEnum.RandomPassword,
                ControlItems = new List<ControlItem>()
                {
                    txtRandomPassword,
                    btnRandomPassword
                }
            };
            loginMenu.GroupItems.Add(randomPasswordButton);
            return loginMenu;
        }
        public static LoginMenuModel GenerateAWPAndAWoutP(LoginMenuModel loginMenu, int? registerAttr, PrepareFormModel prepareFormModel)
        {
            #region Login Group Item
            ControlItem txtUserName = CreateControlTextBox("UserName", "UserName", "Tên tài khoản", MenuControlType.TextBox, "", true);
            ControlItem txtPassword = CreateControlTextBox("PasswordJson", "PasswordJson", "Mật khẩu", MenuControlType.TextBox, "", true);
            ControlItem btnLoginAccountWithPass = CreateControlButton(
                "loginAccountWithPass()",
                MenuControlType.Button,
                "btnLoginAccountWithPass",
                "Đăng nhập",
                "fas fa-sign-in-alt",
                "btn btn-info");
            ControlItem btnLinkRegister = CreateControlButton(
                "openRegisterForm()",
                MenuControlType.Href,
                "btnRegisterNav",
                "Đăng ký",
                "color: cornflowerblue;",
                "control-item-vertical login-buttons-vertical col-md-12");


            GroupItem loginGroupItem = new GroupItem()
            {
                Css = "",
                LoginModeCode = (int)LoginModeEnum.AccountWithPass,
                CssClass = "btn-group-vertical login-box",
                ControlItems = new List<ControlItem>()
                {
                    txtUserName,
                    txtPassword,
                    btnLoginAccountWithPass,
                    btnLinkRegister
                },
            };
            #endregion
            #region Register Group Item
            GroupItem registerGroupItem = new GroupItem()
            {
                Css = "",
                LoginModeCode = (int)LoginModeEnum.AccountWithPass,
                CssClass = "btn-group-vertical register-box-general-form",
            };

            List<ControlItem> registerControlItems = new List<ControlItem>();
            ControlItem txtRUserName = CreateControlTextBox("rUsername", "rUsername", "Tài khoản", MenuControlType.TextBox, "", true);
            ControlItem txtRPassword = CreateControlTextBox("rPassword", "rPassword", "Mật khẩu", MenuControlType.TextBox, "", true);
            ControlItem txtRConfirmPassword = CreateControlTextBox("rConfirmPassword", "rConfirmPassword", "Nhập lại mật khẩu", MenuControlType.TextBox, "", true);
            registerControlItems.Add(txtRUserName);
            registerControlItems.Add(txtRPassword);
            registerControlItems.Add(txtRConfirmPassword);
            PrepareOtherFields(registerAttr, registerControlItems, prepareFormModel);
      
            ControlItem btnRegister = CreateControlButton(
                "submitRegister()",
                MenuControlType.Button,
                "btnRegisterAccountWithPass",
                "Đăng ký",
                "fa fa-user-plus",
                "btn btn-info");
            ControlItem btnQuit = CreateControlButton(
                "cancelRegisterForm()",
                MenuControlType.Button,
                "btnClose",
                "Hủy",
                "",
                "btn btn-danger");
            registerControlItems.Add(btnRegister);
            registerControlItems.Add(btnQuit);

            registerGroupItem.ControlItems = registerControlItems;
            #endregion
            // Add login group and register group to login menu
            loginMenu.GroupItems.Add(loginGroupItem);
            loginMenu.GroupItems.Add(registerGroupItem);
            return loginMenu;
        }
        public static LoginMenuModel GenerateAWP(LoginMenuModel loginMenu)
        {

            ControlItem txtUserName = CreateControlTextBox("UserName", "UserName", "Tên tài khoản", MenuControlType.TextBox, "", true);
            ControlItem txtPassword = CreateControlTextBox("PasswordJson", "PasswordJson", "Mật khẩu", MenuControlType.TextBox, "", true);
            ControlItem btnLoginAccountWithPass = CreateControlButton(
                "loginAccountWithPass()",
                MenuControlType.Button,
                "btnLoginAccountWithPass",
                "Đăng nhập",
                "fas fa-sign-in-alt",
                "btn btn-info");

            #region Login Group Item
            GroupItem loginGroupItem = new GroupItem()
            {
                Css = "",
                LoginModeCode = (int)LoginModeEnum.AccountWithPass,
                CssClass = "btn-group-vertical login-box",
            };
            List<ControlItem> ControlItems = new List<ControlItem>()
            {
                txtUserName,
                txtPassword,
                btnLoginAccountWithPass
            };
            #endregion
            loginMenu.GroupItems.Add(loginGroupItem);
            return loginMenu;
        }
        public static LoginMenuModel GenerateAWoutP(LoginMenuModel loginMenu, int? registerAttr, PrepareFormModel prepareFormModel)
        {
            #region Register Group Item
            GroupItem registerGroupItem = new GroupItem()
            {
                Css = "",
                LoginModeCode = (int)LoginModeEnum.RegisterInformation,
                CssClass = "btn-group-vertical register-box-general-form",
            };

            List<ControlItem> registerControlItems = new List<ControlItem>();
            // Prepare other fields
            PrepareOtherFields(registerAttr, registerControlItems, prepareFormModel);
            ControlItem btnLoginAccountWithoutPass = CreateControlButton(
                "loginAccountWithoutPass()",
                MenuControlType.Button,
                "btnRegisterAccountWithoutPass",
                "Đồng ý",
                "fas fa-arrow-alt-circle-right",
                "btn btn-info btn-block");

            registerControlItems.Add(btnLoginAccountWithoutPass);
            registerGroupItem.ControlItems = registerControlItems;

            loginMenu.GroupItems.Add(registerGroupItem);
            #endregion
            return loginMenu;
        }

        #region Create Control Item Utils
        private static void PrepareOtherFields(int? registerAttr, List<ControlItem> registerControlItems, PrepareFormModel prepareFormModel)
        {
            // Prepare other fields
            if (registerAttr != null)
            {
                if ((registerAttr & (int)RegisterAttrEnum.RegisterName) != 0)
                {
                    //Prepare name field
                    ControlItem name = CreateControlTextBox("rName", "InputName", "Họ và tên", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(name);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterEmail) != 0)
                {
                    //Prepare mail field
                    ControlItem mail = CreateControlTextBox("rEmail", "EmailAddress", "Địa chỉ email", MenuControlType.Email, "", true);
                    registerControlItems.Add(mail);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterPhone) != 0)
                {
                    //Prepare phone number field
                    ControlItem phone = CreateControlTextBox("rPhone", "Phone", "Số điện thoại", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(phone);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterOrganization) != 0)
                {
                    //Prepare Organization field
                    ControlItem organization = CreateControlTextBox("rOrganization", "Organization", "Tổ chức", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(organization);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterJob) != 0)
                {
                    //Prepare job field
                    ControlItem job = CreateControlTextBox("rJob", "Job", "Nghề nghiệp", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(job);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterPosition) != 0)
                {
                    //Prepare position field
                    ControlItem position = CreateControlTextBox("rPosition", "Position", "Chức vụ", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(position);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterBirthDay) != 0)
                {
                    //Prepare position field
                    ControlItem birthDay = CreateControlTextBox("rBirthDay", "BirthDay", "Ngày sinh DD/MM/YYYY", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(birthDay);
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAddress) != 0)
                {
                    //Prepare position field
                    ControlItem address = CreateControlTextBox("rAddress", "Address", "Địa chỉ", MenuControlType.TextBox, "", true);
                    registerControlItems.Add(address);
                }
                //if (registerControlItems.Count % 2 != 0 && registerControlItems.Count > 0)
                //{
                //    ((ControlTextBox)registerControlItems.LastOrDefault()).CssClassInputLayer2 = "control-item-vertical input-item-vertical register-last-input-vertical col-md-12";
                //}
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAttribute1) != 0)
                {
                    string[] prepareTools = prepareFormModel.RegisterAttribute1Def.Split('_');
                    bool isRequired = prepareFormModel.RegisterAttribute1IsRequired == "true" ? true : false;
                    ControlItem rAttribute = MapAttrToControlItem("rAttribute1", "rAttribute1", isRequired, prepareTools);
                    if (rAttribute != null)
                    {
                        registerControlItems.Add(rAttribute);
                    }
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAttribute2) != 0)
                {
                    string[] prepareTools = prepareFormModel.RegisterAttribute2Def.Split('_');
                    bool isRequired = prepareFormModel.RegisterAttribute2IsRequired == "true" ? true : false;
                    ControlItem rAttribute = MapAttrToControlItem("rAttribute2", "rAttribute2", isRequired, prepareTools);
                    if (rAttribute != null)
                    {
                        registerControlItems.Add(rAttribute);
                    }
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAttribute3) != 0)
                {
                    string[] prepareTools = prepareFormModel.RegisterAttribute3Def.Split('_');
                    bool isRequired = prepareFormModel.RegisterAttribute3IsRequired == "true" ? true : false;
                    ControlItem rAttribute = MapAttrToControlItem("rAttribute3", "rAttribute3", isRequired, prepareTools);
                    if (rAttribute != null)
                    {
                        registerControlItems.Add(rAttribute);
                    }
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAttribute4) != 0)
                {
                    string[] prepareTools = prepareFormModel.RegisterAttribute4Def.Split('_');
                    bool isRequired = prepareFormModel.RegisterAttribute4IsRequired == "true" ? true : false;
                    ControlItem rAttribute = MapAttrToControlItem("rAttribute4", "rAttribute4", isRequired, prepareTools);
                    if (rAttribute != null)
                    {
                        registerControlItems.Add(rAttribute);
                    }
                }
                if ((registerAttr & (int)RegisterAttrEnum.RegisterAttribute5) != 0)
                {
                    string[] prepareTools = prepareFormModel.RegisterAttribute5Def.Split('_');
                    bool isRequired = prepareFormModel.RegisterAttribute5IsRequired == "true" ? true : false;
                    ControlItem rAttribute = MapAttrToControlItem("rAttribute5", "rAttribute5", isRequired, prepareTools);
                    if (rAttribute != null)
                    {
                        registerControlItems.Add(rAttribute);
                    }
                }
            }

            //if (registerControlItems.Count % 2 != 0 && registerControlItems.Count > 0)
            //{
            //    registerControlItems.LastOrDefault().CssClass = "control-item-vertical input-item-vertical register-last-input-vertical col-md-12";
            //}
        }

        private static ControlItem MapAttrToControlItem(
            string id,
            string name,
            bool isRequired,
            string[] prepareTools)
        {
            int typeOfControl = int.Parse(prepareTools[0]);
            string tmpTitle = prepareTools[1];
            string tmpValues = prepareTools[2];
            switch (typeOfControl)
            {
                case (int)MenuControlType.TextBox:
                    return CreateControlTextBox(id, name, tmpTitle, MenuControlType.TextBox, "", isRequired);
                case (int)MenuControlType.DropdownList:
                    return CreateControlDropdownList(id, tmpTitle, name, tmpValues, MenuControlType.DropdownList, isRequired);
                case (int)MenuControlType.Checkbox:
                    return CreateControlCheckBox(id, tmpTitle, name, tmpValues, MenuControlType.Checkbox);
                default:
                    return null;
            }
        }

        /// <summary>
        /// To create control Href
        /// </summary>
        /// <param name="onClickFunction">onClickFunction</param>
        /// <param name="type">type</param>
        /// <param name="id">id</param>
        /// <param name="title">title</param>
        /// <param name="cssClassALayer1">cssClassALayer1</param>
        /// <param name="CssInlineLayer1">CssInlineLayer1</param>
        /// <returns></returns>
        private static ControlHref CreateControlHref(
            string onClickFunction,
            MenuControlType type,
            string id,
            string title,
            string CssInlineLayer1 = "color: cornflowerblue;",
            string cssClassALayer1 = "control-item-vertical login-buttons-vertical col-md-12")
        {
            return new ControlHref()
            {   
                Onclick = onClickFunction,
                Type = type,
                Id = id,
                Title = title,
                CssClassALayer1 = cssClassALayer1,
                CssInlineLayer1 = CssInlineLayer1
            };
        }


        /// <summary>
        /// To create control button
        /// </summary>
        /// <param name="onClickFunction"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="cssIcon"></param>
        /// <param name="cssMainClass"></param>
        /// <returns></returns>
        private static ControlButton CreateControlButton(
            string onClickFunction,
            MenuControlType type,
            string id,
            string title,
            string cssIcon,
            string cssMainClass)
        {
            return new ControlButton()
            {
                Onclick = onClickFunction,
                Type = type,
                Id = id,
                Title = title,
                CssIcon = cssIcon,
                CssMainClass = cssMainClass
            };
        }

        /// <summary>
        /// To create control textbox
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <param name="title">title</param>
        /// <param name="type">type</param>
        /// <param name="placeHolder">placeHolder</param>
        /// <param name="isRequired">isRequired</param>
        /// <param name="cssInlineLayer1">cssInlineLayer1</param>
        /// <param name="cssClassSpanLayer1">cssClassSpanLayer1</param>
        /// <param name="cssClassInputLayer2">cssClassInputLayer2</param>
        /// <param name="cssClassLabelLayer2">cssClassLabelLayer2</param>
        /// <param name="cssClassSpanLayer3">cssClassSpanLayer3</param>
        /// <returns></returns>
        private static ControlTextBox CreateControlTextBox(
            string id,
            string name,
            string title,
            MenuControlType type,
            string placeHolder,
            bool isRequired)
        {
            return new ControlTextBox()
            {
                Id = id,
                Name = name,
                Title = title,
                Type = type,
                PlaceHolder = placeHolder,
                IsRequired = isRequired,
            };
        }

        /// <summary>
        /// To create ControlDropdownList
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="cssInlineLayer1"></param>
        /// <param name="cssClassDivLayer1"></param>
        /// <param name="cssClassLabelLayer2"></param>
        /// <param name="cssClassSelectLayer2"></param>
        /// <param name="cssClassOptionLayer3"></param>
        /// <returns></returns>
        private static ControlDropdownList CreateControlDropdownList(
            string id,
            string title,
            string name,
            string values,
            MenuControlType type,
            bool isRequired)
        {
            return new ControlDropdownList()
            {
                Id = id,
                Title = title,
                Name = name,
                Values = values,
                Type = type,
                IsRequired = isRequired
            };
        }

        /// <summary>
        /// To create ControlCheckBox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="cssInlineLayer1"></param>
        /// <param name="cssClassDivLayer1"></param>
        /// <param name="cssClassLabelLayer2"></param>
        /// <param name="cssClassInputLayer2"></param>
        /// <param name="cssClassPLayer2"></param>
        /// <param name="cssClassLabelLayer3"></param>
        /// <param name="cssClassInputLayer3"></param>
        /// <returns></returns>
        private static ControlCheckBox CreateControlCheckBox(
            string id,
            string title,
            string name,
            string values,
            MenuControlType type,
            string cssInlineLayer1 = "",
            string cssClassDivLayer1 = "col-md-12 formWelcome",
            string cssClassLabelLayer2 = "label-item-vertical col-md-12",
            string cssClassInputLayer2 = "",
            string cssClassPLayer2 = "",
            string cssClassLabelLayer3 = "",
            string cssClassInputLayer3 = "")
        {

            return new ControlCheckBox()
            {
                Id = id,
                Title = title,
                Name = name,
                Values = values,
                Type = type,
                CssInlineLayer1 = cssInlineLayer1,
                CssClassDivLayer1 = cssClassDivLayer1,
                CssClassLabelLayer2 = cssClassLabelLayer2,
                CssClassInputLayer2 = cssClassInputLayer2,
                CssClassPLayer2 = cssClassPLayer2,
                CssClassLabelLayer3 = cssClassLabelLayer3,
                CssClassInputLayer3 = cssClassInputLayer3
            };
        } 
        #endregion
    }
}