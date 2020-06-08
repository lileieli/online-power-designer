using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper;
using Pd.Core.Models;

namespace Pd.Core.Areas.Pd.Controllers
{
    public class BaseController : Controller
    {
      
     
        public BaseController()
        {
            
        }
        #region 查询
        /// <summary>
        /// 统一处理参数为空状态下返回数据
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public Dictionary<int, List<T>> GetLikeT<T>(DbSet<T> entities, RequestModel requestModel) where T : class
        {
            var list = new List<T>();
            var count = 0;
            if (requestModel == null) requestModel = new RequestModel();
            if (string.IsNullOrEmpty(requestModel.search))
            {
                list = entities.Skip(requestModel.offset * requestModel.limit).Take(requestModel.limit).ToList();
                count = entities.Count();
                return new Dictionary<int, List<T>> { { count, list } };
            }
            else
            {
                count = entities.AsEnumerable().Where(p => GetLikeWhere(p, requestModel.search)).Count();
                list = entities.AsEnumerable().Where(p => GetLikeWhere(p, requestModel.search)).Skip(requestModel.offset * requestModel.limit).Take(requestModel.limit).ToList();
                return new Dictionary<int, List<T>> { { count, list } };
            }

        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="WhereText"></param>
        /// <returns></returns>
        public bool GetLikeWhere<T>(T Model, string WhereText)
        {
            bool Result = false;
            var Model_ = Model.GetType().GetProperties();
            foreach (var item in Model_)
            {
                string value = "";
                try { value = item.GetValue(Model).ToString(); } catch { }
                if (value.Contains(WhereText))
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }


        public Dictionary<int, List<T>> GetT<T>(DbSet<T> entities, RequestModel requestModel, string Condition) where T : class
        {
            var list = new List<T>();
            var count = 0;
            if (requestModel == null) requestModel = new RequestModel();
            if (string.IsNullOrEmpty(requestModel.search))
            {
                list = entities.Skip(requestModel.offset * requestModel.limit).Take(requestModel.limit).ToList();
                count = entities.Count();
                return new Dictionary<int, List<T>> { { count, list } };
            }
            else
            {
                list = entities.AsEnumerable().Where(p => GetWhere(p, Condition, requestModel.search)).Skip(requestModel.offset * requestModel.limit).Take(requestModel.limit).ToList();
                count = entities.AsEnumerable().Where(p => GetWhere(p, Condition, requestModel.search)).Count();
                return new Dictionary<int, List<T>> { { count, list } };
            }
        }
        /// <summary>
        /// 精确查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Moldel">实体</param>
        /// <param name="Condition">查询字段名称</param>
        /// <param name="WhereText">前台传来的值</param>
        /// <returns></returns>
        public bool GetWhere<T>(T Moldel, string Condition, string WhereText)
        {
            bool Result = false;
            var Model_ = Moldel.GetType().GetProperties();
            foreach (var item in Model_)
            {
                string value = "";
                if (item.Name != Condition) continue;
                try { value = item.GetValue(Moldel).ToString(); } catch { }
                if (value == WhereText)
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }

        #endregion

    }

    /// <summary>
    /// sql封装  貌似官方出了 不用自己封装了
    /// var context = new DbContext();
    /// var userInfo = context.Database.SqlQuery<UserInfo>("select * from user");
    /// </summary>
    public static class EntityFrameworkCoreExtensions
    {
        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection connection, params object[] parameters)
        {
            var conn = facade.GetDbConnection();
            connection = conn;
            conn.Open();
            var cmd = conn.CreateCommand();
            if (facade.IsSqlServer())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
        {
            var command = CreateCommand(facade, sql, out DbConnection conn, parameters);
            var reader = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        public static List<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            var dt = SqlQuery(facade, sql, parameters);
            return dt.ToList<T>();
        }

        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var propertyInfos = typeof(T).GetProperties();
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                        p.SetValue(t, row[p.Name], null);
                }
                list.Add(t);
            }
            return list;
        }
    }
}