using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp2020.App_Code
{
    //static class ONLY provides FUNCTION,
    //  it is not designed to be initialized, 
    //    OR we can say, we should create objects for static classes!

    public static class AjaxHelper4Paging
    {

        
        // const is C#, means "READONLY & STATIC"!!
        private const string CURRENT_PAGE_CLASSNAME = "currentPage";
        private const string PAGE_LINK_CLASSNAME = "pageLink";

        //By putting the mouse focus (I) on the type with under-wave, 
        //  AND press "Alt + ENTER", the IDE will prompt you with using a certain namespace
        //     You can just press the ENTER key (and the namespace will be using "automatically"!
        //Reusable code!!
        static void TargetUrlWithParameters(this StringBuilder urlBuilder, string text, string relativeUrl, int currentPageIndex, int pageSize, string extra_url_params, string ajax, string className = "pageLink") //或者是pageLink 或者是 currentPage
        {
            urlBuilder.AppendFormat(
              "<a class='{0}' href='{1}?pageIndex={2}&pageSize={3}&{4}' {5}>{6}</a>",
              className, relativeUrl, currentPageIndex, pageSize, extra_url_params, ajax, text);
        }

        public static HtmlString PagingNavigator(this AjaxHelper AjaxHelper, int currentPageIndex, int pageSize, int totalCount, string ajaxTargetID, string extra_url_params)
        {
            //Resuable function in any view file, @Ajax.PaginNavigator(....)
            // Areas is used for large-scale ASP.NET MVC projects!
            //
            //By using @Ajax... in the View page file ~/Areas/Chapter01Ajax/Welcome.cshtml
            //   Areas: Areas;   Controller: Chapter01Ajax;  Action: Welcome
            //   the corresponding ViewContext will contains the information
            //       such as Areas, Controller, Action
            RouteData route = AjaxHelper.ViewContext.RouteData; //RouteData is a object contains dictionary
            string actionName = route.Values["action"].ToString();
            string controllerName = route.Values["controller"].ToString();
            return PagingNavigator(AjaxHelper, currentPageIndex, pageSize, totalCount, ajaxTargetID, actionName, controllerName, extra_url_params);
        }
        public static HtmlString PagingNavigator(this AjaxHelper AjaxHelper, int currentPageIndex, int pageSize, int totalCount, string ajaxTargetID, string actionName, string extra_url_params)
        {
            RouteData route = AjaxHelper.ViewContext.RouteData;
            string controllerName = route.Values["controller"].ToString();
            return PagingNavigator(AjaxHelper, currentPageIndex, pageSize, totalCount, ajaxTargetID, actionName, controllerName, extra_url_params);
        }

        //with all the parameters(action/controller)
        public static HtmlString PagingNavigator(this AjaxHelper AjaxHelper, int currentPageIndex, int pageSize, int totalCount, string ajaxTargetID, string actionName, string controllerName, string extra_url_params)
        {
            //准备Ajax脚本串:
            string ajaxTag4Html = "data-ajax-mode='replace' data-ajax='true'";
            string ajax = string.Format("data-ajax-update='#{0}' {1}", ajaxTargetID, ajaxTag4Html);
            string targetUrl;
            
            //targetUrl = AjaxHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            
            targetUrl = string.Format("/{0}/{1}", controllerName, actionName);
            pageSize = pageSize == 0 ? 3 : pageSize;
            //101 records can be show 10*10 + 1 additional page = 11 pages
            int totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            StringBuilder output = new StringBuilder();

            if (totalPages > 1)
            {
                //First Page's Link
                if (currentPageIndex != 1)
                {
                    //for reusing code, we defined another method, here just call it!!
                    output.TargetUrlWithParameters("Home Page", targetUrl, 1, pageSize, extra_url_params, ajax, PAGE_LINK_CLASSNAME);
                    //PAGE_LINK_CLASSNAME is used for active page link
                }
                else
                {
                    output.TargetUrlWithParameters("Home Page", targetUrl, 1, pageSize, extra_url_params, ajax, CURRENT_PAGE_CLASSNAME);  
                                                                                                                                   //CURRENT_PAGE_CLASSNAME is used for current page, you can not click it, because
                                                                                                                                   //      you are (the current view) stays on it!!
                                                                                                                                   //  displays in GRAY OR disable
                }
                output.Append(" ");
                
                if (currentPageIndex > 1)
                {
                    output.TargetUrlWithParameters("Previous Page", targetUrl, currentPageIndex - 1, pageSize, extra_url_params, ajax, PAGE_LINK_CLASSNAME); //Previous page
                }
                else 
                {
                    output.TargetUrlWithParameters("Previous Page", targetUrl, 1, pageSize, extra_url_params, ajax, CURRENT_PAGE_CLASSNAME); // 第一页上，显示的 上一页 是灰色的当前页以灰色显示（暗示用户不必点击）
                }

                output.Append(" ");

                
                int startIndex = 0;
                if (currentPageIndex <= 5)
                    startIndex = 1;
                else if (currentPageIndex >= totalPages - 4)
                    startIndex = totalPages - 9;
                else
                    startIndex = currentPageIndex - 5;

                for (int i = startIndex; i <= startIndex + 9 && i <= totalPages; i++)
                {
                    if (i == currentPageIndex)
                    {
                        output.TargetUrlWithParameters(currentPageIndex.ToString("d2"), targetUrl, currentPageIndex, pageSize, extra_url_params, ajax, CURRENT_PAGE_CLASSNAME);
                    }
                    else
                    {
                        output.TargetUrlWithParameters(i.ToString("d2"), targetUrl, i, pageSize, extra_url_params, ajax, PAGE_LINK_CLASSNAME);
                    }
                    output.Append(" ");
                }

                
                if (currentPageIndex < totalPages)
                {
                    output.TargetUrlWithParameters("Next Page", targetUrl, currentPageIndex + 1, pageSize, extra_url_params, ajax, PAGE_LINK_CLASSNAME);
                }
                else 
                {
                    output.TargetUrlWithParameters("Next Page", targetUrl, totalPages, pageSize, extra_url_params, ajax, CURRENT_PAGE_CLASSNAME); //最后一页上，显示的下一页是灰色的
                }
                output.Append(" ");

                
                if (currentPageIndex != totalPages)
                {
                    output.TargetUrlWithParameters("Half Page", targetUrl, totalPages, pageSize, extra_url_params, ajax, PAGE_LINK_CLASSNAME);
                }
                else
                {
                    output.TargetUrlWithParameters("Half Page", targetUrl, totalPages, pageSize, extra_url_params, ajax, CURRENT_PAGE_CLASSNAME);
                }
                output.Append(" ");
            }

            output.AppendFormat("&emsp;First&nbsp;<b>{0}</b>&nbsp;Page / Mutual&nbsp;<b>{1}</b>&nbsp;Page", currentPageIndex, totalPages);
            

            return new HtmlString(output.ToString());
        }
    }
}