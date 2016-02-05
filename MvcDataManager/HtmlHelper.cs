using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcDataManager.Model;

namespace MvcDataManager
{
    public static class HtmlControls
    {
        public static MvcHtmlString LoginControlForModel<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> userName,
            Expression<Func<TModel, TProperty>> password,
            string submitButtonText,
            string headerText = "",
            bool showHeader = false,
            bool enableFieldValidationMessage = true,
            bool enableValidationSummary = false)
        {
            var htmlString = String.Format(@"
                                <div class='loginPanel'>
                                    <div class='row'>
                                        <div class='col-lg-3 col-md-6 col-sm-12 col-xs-12'>
                                            <div class='panel panel-default'>
                                                " + (showHeader ? "<div class='panel-heading text-center' data-header>" + headerText + "</div>" : "") + @"
                                                <div class='panel-body' data-loginContent>
                                                    <form role='form'>
                                                        <div class='form-group'>    
                                                            " + helper.LabelFor(userName).ToHtmlString() + @"
                                                            " + helper.TextBoxFor(userName).ToHtmlString() + @"
                                                            " + (enableFieldValidationMessage ? "<br />" + helper.ValidationMessageFor(userName).ToHtmlString() : "") + @"
                                                        </div>
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(password).ToHtmlString() + @"
                                                            " + helper.PasswordFor(password).ToHtmlString() + @"
                                                            " + (enableFieldValidationMessage ? "<br />" + helper.ValidationMessageFor(password).ToHtmlString() : "") + @"
                                                        <div class='form-group text-center'>
                                                            <input type='checkbox' /> Remember Me
                                                        </div>
                                                        <div class='form-group'>
                                                            <button type='submit' class='form-control btn btn-default'>" + submitButtonText + @"</button>
                                                        </div>
                                                        " + (enableValidationSummary ? helper.ValidationSummary(false).ToHtmlString() : "") + @"
                                                    </form>
                                                </div>
                                                <div class='panel-footer'>
                                                    <div class='form-group text-center'>
                                                        <a href='ForgotPassword' title='Forgot Password'>Forgot Password</a> | 
                                                        <a href='Register' title='Sign Up'>Sign Up</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");

            return MvcHtmlString.Create(htmlString);
        }

        public static MvcHtmlString ForgotPasswordControlForModel<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> userName,
            Expression<Func<TModel, TProperty>> securityQuestionId,
            IEnumerable<SecurityQuestion> securityQuestions, 
            Expression<Func<TModel, TProperty>> answer,
            string anctionName,
            string controllerName)
        {
            var listItems = new List<SelectListItem> { new SelectListItem { Text = "Select", Value = "" } };
            if (securityQuestions != null)
                securityQuestions.ToList().ForEach(x => listItems.Add(new SelectListItem { Text = x.Question, Value = Convert.ToString(x.Id) }));

            var htmlString = String.Format(@"
                                <div class='forgotPwdPanel'>
                                    <div class='row'>
                                        <div class='col-lg-3 col-md-6 col-sm-12 col-xs-12'>
                                            <div class='panel panel-default'>
                                                <div class='panel-heading text-center' data-header>Forgot Password</div>
                                                <div class='panel-body' data-forgotPassword>
                                                    <form role='form'>
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(userName).ToHtmlString() + @"
                                                            " + helper.TextBoxFor(userName, null, new { @class = "form-control" }).ToHtmlString() + @"
                                                        </div>
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(securityQuestionId).ToHtmlString() + @"
                                                            " + helper.DropDownListFor(securityQuestionId, listItems, new { @class = "form-control" }).ToHtmlString() + @"
                                                        </div>
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(answer).ToHtmlString() + @"
                                                            " + helper.TextBoxFor(answer, null, new { @class = "form-control" }).ToHtmlString() + @"
                                                        </div>
                                                        <div class='form-group text-center'>
                                                            <button type='submit' class='form-control btn'>Submit</button>
                                                        </div>
                                                    </form>
                                                </div>
                                                <div class='panel-footer'>
                                                    <div class='form-group text-center'>
                                                        " + helper.ActionLink("Cancel", anctionName, controllerName) + @"
                                                        " + helper.ValidationSummary(false) + @"
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");
            return MvcHtmlString.Create(htmlString);
        }


        public static MvcHtmlString ChangePasswordControlForModel<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> userName,
            Expression<Func<TModel, TProperty>> newPasswrod,
            Expression<Func<TModel, TProperty>> confirmPassword,
            string anctionName,
            string controllerName)
        {

            var htmlString = String.Format(@"
                                <div class='changePwdPanel'>
                                    <div class='row'>
                                        <div class='col-lg-3 col-md-6 col-sm-12 col-xs-12'>
                                            <div class='panel panel-default'>
                                                <div class='panel-heading text-center' data-header>Reset Password</div>
                                                <div class='panel-body' data-changePassword>
                                                    <form role='form'>
                                                        " + helper.HiddenFor(userName) + @"
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(newPasswrod).ToHtmlString() + @"
                                                            " + helper.TextBoxFor(newPasswrod, null, new { @class = "form-control" }).ToHtmlString() + @"
                                                        </div>
                                                        <div class='form-group'>
                                                            " + helper.LabelFor(confirmPassword).ToHtmlString() + @"
                                                            " + helper.TextBoxFor(confirmPassword, null, new { @class = "form-control" }).ToHtmlString() + @"
                                                        </div>
                                                        <div class='form-group text-center'>
                                                            <button type='submit' class='form-control btn'>Submit</button>
                                                        </div>
                                                    </form>
                                                </div>
                                                <div class='panel-footer'>
                                                    <div class='form-group text-center'>
                                                        " + helper.ActionLink("Cancel", anctionName, controllerName) + @"
                                                        " + helper.ValidationSummary(false) + @"
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");
            return MvcHtmlString.Create(htmlString);
        }
    }
}