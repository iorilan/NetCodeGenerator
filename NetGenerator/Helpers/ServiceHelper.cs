using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This code is generated from NetGenerator
/// </summary>


namespace YourProjectName.BusinessLogic.Helpers
{
    public static class ServiceHelper
    {

        // Exception helpers

        public static string FormatException(this DbEntityValidationException ex)
        {
            string error = "";
            foreach (var eve in ex.EntityValidationErrors)
            {
                error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                foreach (var ve in eve.ValidationErrors)
                {
                    error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            return error;
        }

        public static string FormatException(this Exception ex)
        {
            string error = "";
            string message = "\r\n[Exception All Information]";
            if (ex != null)
            {
                message += "\r\n[Message ] " + ex.Message;
                message += "\r\n[SOURCE ] " + ex.Source;
                message += "\r\n[STACK trace ]" + ex.StackTrace;
                if (ex.InnerException != null)
                {
                    ex.InnerException.FillAllInnerExceptionInfo(ref message);
                }
            }
            error += "\r\n" + message;
            return error;
        }

        public static void FillAllInnerExceptionInfo(this Exception ex, ref string info)
        {
            if (ex != null)
            {
                info += "\r\n[Message] : " + ex.Message;
                if (ex.InnerException != null)
                {
                    FillAllInnerExceptionInfo(ex.InnerException, ref info);
                }
            }
        }
   }

    public static class LinQHelper
    {
        private static ILog _log = LogManager.GetLogger(typeof(LinQHelper));
        public static IQueryable<T> OrderByEx<T>(this IQueryable<T> q, string direction, string fieldName)
        {
            try
            {
                var customProperty = typeof(T).GetCustomAttributes(false).OfType<ColumnAttribute>().FirstOrDefault();
                if (customProperty != null)
                {
                    fieldName = customProperty.Name;
                }

                var param = Expression.Parameter(typeof(T), "p");
                var prop = Expression.Property(param, fieldName);
                var exp = Expression.Lambda(prop, param);
                string method = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
                Type[] types = new Type[] { q.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
                return q.Provider.CreateQuery<T>(mce);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("error form OrderByEx.");
                _log.Error(ex);
                throw;
            }
        }
    }
}
