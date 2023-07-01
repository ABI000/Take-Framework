using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TakeFramework.Domain
{
    /// <summary>
    /// 分页查询请求参数类
    /// </summary>
    public class PageRequest : QueryRequest
    {
        /// <summary>
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// PageSize
        /// </summary>
        public virtual int PageSize { get; set; } = 10;
    }
    /// <summary>
    /// 查询请求参数类
    /// </summary>
    public class QueryRequest
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderField { get; set; } = "id";

        /// <summary>
        /// 排序方式asc/desc
        /// </summary>
        public string OrderType { get; set; } = "desc";

        /// <summary>
        /// 查询条件
        /// </summary>

        public List<Condition> Conditions { get; set; } = new List<Condition>();

        /// <summary>
        /// IConditionalModel
        /// </summary>
        [JsonIgnore]
        public List<IConditionalModel> ConditionalModels
        {
            get
            {
                List<IConditionalModel> result = new List<IConditionalModel>();
                if (Conditions.Any())
                {
                    Conditions.ForEach(f =>
                    {
                        IConditionalModel conditionalModel = new ConditionalModel
                        {
                            FieldName = f.FieldName,
                            FieldValue = f.FieldValue,
                            ConditionalType = f.ConditionalType
                        };
                        result.Add(conditionalModel);
                    });
                }
                return result;
            }
        }
    }

    /// <summary>
    /// Condition
    /// </summary>
    public class Condition : IConditionalModel
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ConditionalType ConditionalType { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        [JsonIgnore]
        public string Expressions => this.ConditionalType switch
        {
            ConditionalType.Equal => $"{this.FieldName}=\"{this.FieldValue}\"",
            ConditionalType.Like => $"{this.FieldName}.Contains(\"{this.FieldValue}\")",
            ConditionalType.GreaterThan => $"{this.FieldName}>\"{this.FieldValue}\"",
            ConditionalType.GreaterThanOrEqual => $"{this.FieldName}>=\"{this.FieldValue}\"",
            ConditionalType.LessThan => $"{this.FieldName}<\"{this.FieldValue}\"",
            ConditionalType.LessThanOrEqual => $"{this.FieldName}<=\"{this.FieldValue}\"",
            ConditionalType.In => $"{this.FieldName}.Contains(\"{this.FieldValue}\")",
            ConditionalType.NotIn => $"!{this.FieldName}.Contains(\"{this.FieldValue}\")",
            ConditionalType.LikeLeft => $"{this.FieldName}.StartsWith(\"{this.FieldValue}\")",
            ConditionalType.LikeRight => $"{this.FieldName}.EndsWith(\"{this.FieldValue}\")",
            ConditionalType.NoEqual => $"{this.FieldName}!={this.FieldValue}",
            ConditionalType.IsNullOrEmpty => $"{this.FieldName}.IsNullOrEmpty(\"{this.FieldValue}\")",
            ConditionalType.IsNot => $"{this.FieldName}!= null",
            ConditionalType.NoLike => $"!{this.FieldName}.Contains(\"{this.FieldValue}\")",

            _ => string.Empty
        };

        /// <summary>
        /// Sql
        /// </summary>
        [JsonIgnore]
        public string Sql => this.ConditionalType switch
        {
            ConditionalType.Equal => $"c.{this.FieldName}=@{this.FieldName}",
            ConditionalType.Like => $"CONTAINS(c.{this.FieldName},@{this.FieldName},true)",
            ConditionalType.GreaterThan => $"c.{this.FieldName} > @{this.FieldName}",
            ConditionalType.GreaterThanOrEqual => $"c.{this.FieldName}>= @{this.FieldName}",
            ConditionalType.LessThan => $"c.{this.FieldName}<@{this.FieldName}",
            ConditionalType.LessThanOrEqual => $"c.{this.FieldName}<=@{this.FieldName}",
            ConditionalType.In => $"c.{this.FieldName}.Contains(@{this.FieldName})",
            ConditionalType.NotIn => $"not c.{this.FieldName}.Contains(@{this.FieldName})",
            ConditionalType.LikeLeft => $"STARTSWITH(c.{this.FieldName},@{this.FieldName},true)",
            ConditionalType.LikeRight => $"ENDSWITH(c.{this.FieldName},@{this.FieldName},true)",
            ConditionalType.NoEqual => $"c.{this.FieldName}!={this.FieldValue}",
            ConditionalType.IsNullOrEmpty => $"c.{this.FieldName}.IsNullOrEmpty(@{this.FieldName})",
            ConditionalType.IsNot => $"c.{this.FieldName}!= null",
            ConditionalType.NoLike => $"not c.{this.FieldName}.Contains(@{this.FieldName})",
            _ => string.Empty
        };
    }
    public interface IConditionalModel
    {
    }
    public enum ConditionalType
    {
        Equal = 0,
        Like = 1,
        GreaterThan = 2,
        GreaterThanOrEqual = 3,
        LessThan = 4,
        LessThanOrEqual = 5,
        In = 6,
        NotIn = 7,
        LikeLeft = 8,
        LikeRight = 9,
        NoEqual = 10,
        IsNullOrEmpty = 11,
        IsNot = 12,
        NoLike = 13,
        EqualNull = 14,
        InLike = 15
    }

    public class ConditionalModel : IConditionalModel
    {
        //public ConditionalModel();

        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string CSharpTypeName { get; set; }
        //public ICustomConditionalFunc CustomConditionalFunc { get; set; }
        public dynamic CustomParameterValue { get; set; }
        public ConditionalType ConditionalType { get; set; }
        [JsonIgnore]
        public Func<string, object> FieldValueConvertFunc { get; set; }

        public static List<IConditionalModel> Create(params IConditionalModel[] conditionalModel)
        {
            return new List<IConditionalModel>();
        }
    }
    //public interface ICustomConditionalFunc
    //{
    //    KeyValuePair<string, SugarParameter[]> GetConditionalSql(ConditionalModel json, int index);
    //}
}
