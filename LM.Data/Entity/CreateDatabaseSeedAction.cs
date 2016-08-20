using LM.Core.Data.Migrations;
using LM.Data.Models;
using System;
using System.Data.Entity;



namespace LM.Data
{
    public class CreateDatabaseSeedAction : ISeedAction
    {
        #region Implementation of ISeedAction

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order { get { return 1; } }

        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(DbContext context)
        {
            context.Set<ApprovalType>().Add(new ApprovalType() { Name = "系统管理员" });
        }

        #endregion
    }
}
