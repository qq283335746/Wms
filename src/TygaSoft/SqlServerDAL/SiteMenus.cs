using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysHelper;

namespace TygaSoft.SqlServerDAL
{
    public partial class SiteMenus
    {
        #region ISiteMenus Member

        public IList<SiteMenusInfo> GetListByParentName(string parentName)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select m.ApplicationId,m.Id,m.ParentId,m.IdStep,m.Title,m.Url,m.Descr,m.Sort,m.LastUpdatedDate
                        from SiteMenus m 
                        join SiteMenus m2 on CHARINDEX(convert(varchar(36),m2.Id),m.IdStep) > 0
                        and m2.Title = @Title
                        order by m.Sort 
                        ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Title",SqlDbType.NVarChar,20)
                                   };
            parms[0].Value = parentName;

            var list = new List<SiteMenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMenusInfo model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.IdStep = reader.GetString(3);
                        model.Title = reader.GetString(4);
                        model.Url = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMenusInfo> GetMenusAccess(string appName, string[] accessIds, bool isAdministrators)
        {
            var list = new List<SiteMenusInfo>();
            IList<SiteMenusAccessInfo> maList = null;

            var parm = new SqlParameter("@ApplicationName", appName);
            if (!isAdministrators)
            {
                var sbIn = new StringBuilder(300);
                foreach (var item in accessIds)
                {
                    sbIn.AppendFormat("'{0}',", item);
                }
                var sqlWhere = string.Format("and a.ApplicationName = @ApplicationName and AccessId in ({0}) ", sbIn.ToString().Trim(','));
                maList = new SiteMenusAccess().GetListByJoin(sqlWhere, parm);
            }

            var cmdText = @"select sm.Id,sm.ParentId,sm.Title,sm.Url,sm.Descr from SiteMenus sm 
                           left join aspnet_Applications a on a.ApplicationId = sm.ApplicationId 
                           where a.ApplicationName = @ApplicationName order by Sort ";


            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new SiteMenusInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.Url = reader.GetString(3);
                        model.Descr = reader.GetString(4);

                        if (isAdministrators)
                        {
                            model.IsView = true;
                            model.IsAdd = true;
                            model.IsEdit = true;
                            model.IsDelete = true;
                        }
                        else
                        {
                            #region 权限控制

                            if (maList != null && maList.Count > 0)
                            {
                                List<SiteMenusAccessItemInfo> maitems = null;

                                var qrmaList = maList.Where(m => m.AccessType == "Roles");
                                if (qrmaList != null && qrmaList.Count() > 0)
                                {
                                    foreach (var item in qrmaList)
                                    {
                                        maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(item.OperationAccess);
                                        var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                                        model.IsView = maitemInfo == null ? false : maitemInfo.IsView;
                                        model.IsAdd = maitemInfo == null ? false : maitemInfo.IsAdd;
                                        model.IsEdit = maitemInfo == null ? false : maitemInfo.IsEdit;
                                        model.IsDelete = maitemInfo == null ? false : maitemInfo.IsDelete;
                                    }
                                }

                                var qumaInfo = maList.FirstOrDefault(m => m.AccessType == "Users");
                                if (qumaInfo != null)
                                {
                                    maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(qumaInfo.OperationAccess);
                                    var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                                    if (maitemInfo != null)
                                    {
                                        if (maitemInfo.IsView) model.IsView = false;
                                        if (maitemInfo.IsAdd) model.IsAdd = false;
                                        if (maitemInfo.IsEdit) model.IsEdit = false;
                                        if (maitemInfo.IsDelete) model.IsDelete = false;
                                    }
                                }
                            }

                            #endregion
                        }

                        if (model.IsView)
                            list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<SiteMenusInfo> GetMenusAccess(Guid appId, Guid accessId, bool isAdministrators)
        {
            var list = new List<SiteMenusInfo>();
            var maInfo = new SiteMenusAccess().GetModel(appId, accessId);
            List<SiteMenusAccessItemInfo> maitems = null;
            if (maInfo != null) maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(maInfo.OperationAccess);

            var cmdText = "select m.Id,m.ParentId,m.Title,m.Url,m.Descr from SiteMenus m where m.ApplicationId = @ApplicationId ";
            var parm = new SqlParameter("@ApplicationId", appId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new SiteMenusInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.Url = reader.GetString(3);
                        model.Descr = reader.GetString(4);

                        if (isAdministrators)
                        {
                            model.IsView = true;
                            model.IsAdd = true;
                            model.IsEdit = true;
                            model.IsDelete = true;
                        }
                        else
                        {
                            if (maitems != null)
                            {
                                var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                                model.IsView = maitemInfo == null ? false : maitemInfo.IsView;
                                model.IsAdd = maitemInfo == null ? false : maitemInfo.IsAdd;
                                model.IsEdit = maitemInfo == null ? false : maitemInfo.IsEdit;
                                model.IsDelete = maitemInfo == null ? false : maitemInfo.IsDelete;
                            }
                        }

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
