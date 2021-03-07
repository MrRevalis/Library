using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.HtmlHelpers
{
    public static class CustomValidationMessage
    {
        public static MvcHtmlString CustomErrorMessage(this HtmlHelper html, ModelStateDictionary modelState, string keyName)
        {
            string errorMessage = modelState.Keys.Where(x => x == keyName).Select(x => modelState[x].Errors[0].ErrorMessage).FirstOrDefault();
            TagBuilder tagBuilder = new TagBuilder("span");
            if (errorMessage != null)
            {
                tagBuilder.InnerHtml = errorMessage;
                tagBuilder.AddCssClass("customError");
            }
            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}