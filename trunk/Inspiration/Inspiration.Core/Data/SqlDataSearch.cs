using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Inspiration.Core.Data
{
    public class SqlDataSearch
    {
        public const string SqlGetDatabase = "Select Name FROM Master..SysDatabases";
        public const string SqlGetTable = "SELECT Name FROM {0}..SysObjects Where XType='U' ORDER BY Name";
        public const string SqlGetTableDescription = @"use [{0}]; SELECT objtype, objname, name, value
FROM fn_listextendedproperty (NULL, 'user', 'dbo', 'table', '{1}', NULL, NULL)";
        public const string SqlGetFiledName = "use [{0}]; SELECT " +
         "表名=case   when   a.colorder=1   then   d.name   else   ''   end, " +
         "表说明=case   when   a.colorder=1   then   isnull(f.value,'')   else   ''   end, " +
         "字段序号=a.colorder, " +
         "字段名=a.name, " +
         "标识=case   when   COLUMNPROPERTY(   a.id,a.name,'IsIdentity')=1   then   '√'else   ''   end, " +
         "主键=case   when   exists(SELECT   1   FROM   sysobjects   where   xtype='PK'   and   name   in   ( " +
         "SELECT   name   FROM   sysindexes   WHERE   indid   in( " +
         "SELECT   indid   FROM   sysindexkeys   WHERE   id   =   a.id   AND   colid=a.colid " +
         ")))   then   '√'   else   ''   end, " +
         "类型=b.name, " +
         "占用字节数=a.length, " +
         "长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), " +
         "小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), " +
         "允许空=case   when   a.isnullable=1   then   '√'else   ''   end, " +
         "默认值=isnull(e.text,''), " +
         "字段说明=isnull(g.[value],'') " +
         "FROM   syscolumns   a " +
         "left   join   systypes   b   on   a.xusertype=b.xusertype " +
         "inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype='U'   and     d.name<>'dtproperties' " +
         "left   join   syscomments   e   on   a.cdefault=e.id " +
         "left   join   sys.extended_properties   g   on   a.id=g.major_id   and   a.colid=g.minor_id " +
         "left   join   sys.extended_properties   f   on   d.id=f.major_id   and   f.minor_id=0 " +
         "where  a.id=object_id('{1}')       --如果只查询指定表,加上此条件 " +
         "order   by   a.id,a.colorder ";
        public const string SqlGetRelation = @"use [{0}]; SELECT 
f.name AS ForeignKey,
SCHEMA_NAME(f.SCHEMA_ID) SchemaName,
OBJECT_NAME(f.parent_object_id) AS TableName,
COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnName,
SCHEMA_NAME(o.SCHEMA_ID) ReferenceSchemaName,
OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
COL_NAME(fc.referenced_object_id,fc.referenced_column_id) AS ReferenceColumnName
,is_disabled as NocheckConstraint
,is_not_for_replication as NotForReplication
,delete_referential_action_desc as DeleteReferentialActionDesc
,update_referential_action_desc UpdateReferentialActionDesc
FROM sys.foreign_keys AS f
INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
INNER JOIN sys.objects AS o ON o.OBJECT_ID = fc.referenced_object_id";
        //where OBJECT_NAME(f.parent_object_id)='{1}'";

        public List<string> GetTables(string projectDBName)
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["soiandb"].ConnectionString, System.Data.CommandType.Text,
    string.Format(SqlDataSearch.SqlGetTable, projectDBName));
            var tables = new List<string>();
            while (reader.Read())
            {
                tables.Add(reader[0].ToString());
            }
            reader.Close();
            return tables;
        }

        public List<ColumnInfo> GetFileds(string projectDBName, string tableName)
        {
            List<ColumnInfo> result = new List<ColumnInfo>();

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text,
                string.Format(SqlGetFiledName, projectDBName, tableName));
            while (reader.Read())
            {
                if (result.Where(item => item.Name == reader[0].ToString()).Count() == 0)
                {
                    ColumnInfo info = new ColumnInfo();
                    info.Name = reader["字段名"].ToString();
                    //info.CArgName = "@" + sqlparams[0].SqlValue.ToString() + "_" + info.Name;
                    info.TypeName = reader["类型"].ToString();
                    info.NameCN = reader["字段说明"].ToString();
                    if (reader["允许空"].ToString() == "√")
                    {
                        info.CanNull = true;
                    }
                    else
                    {
                        info.CanNull = false;
                    }
                    if (reader["主键"].ToString() == "√")
                    {
                        info.IsKey = true;
                    }
                    else
                    {
                        info.IsKey = false;
                    }
                    info.Length = int.Parse(reader["长度"].ToString());
                    //info.COrder = int.Parse(reader["字段序号"].ToString());
                    result.Add(info);
                }
            }
            reader.Close();
            return result;
        }
        public List<RelationInfo> GetRelations(string projectDBName)
        {
            List<RelationInfo> result = new List<RelationInfo>();

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text,
                string.Format(SqlGetRelation, projectDBName));
            while (reader.Read())
            {
                if (result.Where(item => item.ForeignKey == reader[0].ToString()).Count() == 0)
                {
                    RelationInfo info = new RelationInfo();
                    info.ColumnName = reader["ColumnName"].ToString();
                    info.DeleteReferentialActionDesc = reader["DeleteReferentialActionDesc"].ToString();
                    info.ForeignKey = reader["ForeignKey"].ToString();
                    info.NocheckConstraint = reader["NocheckConstraint"].ToString();
                    info.NotForReplication = reader["NotForReplication"].ToString();
                    info.ReferenceColumnName = reader["ReferenceColumnName"].ToString();
                    info.ReferenceSchemaName = reader["ReferenceSchemaName"].ToString();
                    info.ReferenceTableName = reader["ReferenceTableName"].ToString();
                    info.SchemaName = reader["SchemaName"].ToString();
                    info.TableName = reader["TableName"].ToString();
                    info.UpdateReferentialActionDesc = reader["UpdateReferentialActionDesc"].ToString();

                    //info.COrder = int.Parse(reader["字段序号"].ToString());
                    result.Add(info);
                }
            }
            reader.Close();
            return result;
        }
        public List<string> GetTableDescription(string projectDBName, string tableName)
        {
            List<string> result = new List<string>();
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text,
                string.Format(SqlGetTableDescription, projectDBName, tableName));
            while (reader.Read())
            {
                //info.COrder = int.Parse(reader["字段序号"].ToString());
                result.Add(reader["value"].ToString());
            }
            reader.Close();
            return result;
        }
    }

    [Serializable]
    public class ColumnInfo
    {
        /// <summary>
        /// 英文名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string NameCN { get; set; }
        /// <summary>
        /// 字段类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool CanNull { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }



    }

    public class RelationInfo
    {
        //FK_Fields_Models_ModelID	dbo	Fields	ModelID	dbo	Models	ID	0	0	NO_ACTION	NO_ACTION
        public string ForeignKey { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferenceSchemaName { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceColumnName { get; set; }
        public string NocheckConstraint { get; set; }
        public string NotForReplication { get; set; }
        /// <summary>
        /// NO_ACTION CASCADE SET_DEFAULT SET_NULL
        /// </summary>
        public string DeleteReferentialActionDesc { get; set; }
        public string UpdateReferentialActionDesc { get; set; }

    }

}
