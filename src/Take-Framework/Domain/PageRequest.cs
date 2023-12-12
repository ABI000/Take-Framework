using System.Text.Json.Serialization;

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

        public int SikpCount => (PageIndex < 1 ? 1 : PageIndex) - 1 * PageSize;
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

        public (string, object[] args)? GetSql()
        {
            return Conditions.Count == 0 ? null : (string.Join(" and ", Conditions.Select(x => x.Sql)), Conditions.Select(x => x.FieldValue).ToArray());
        }
        public IEnumerable<(string, string, object?)>? GetExpressions()
        {
            return Conditions.Select(x => x.Expressions);
        }
    }

    /// <summary>
    /// Condition
    /// </summary>
    public class Condition
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
        public virtual (string, string, object?) Expressions => this.ConditionalType switch
        {
            ConditionalType.Equal => ($"{this.FieldName}=@0", this.FieldName, this.FieldValue),
            ConditionalType.Like => ($"{this.FieldName}.Contains(@0)", this.FieldName, this.FieldValue),
            ConditionalType.GreaterThan => ($"{this.FieldName}>@0", this.FieldName, this.FieldValue),
            ConditionalType.GreaterThanOrEqual => ($"{this.FieldName}>=@0", this.FieldName, this.FieldValue),
            ConditionalType.LessThan => ($"{this.FieldName}<@0", this.FieldName, this.FieldValue),
            ConditionalType.LessThanOrEqual => ($"{this.FieldName}<=@0", this.FieldName, this.FieldValue),
            ConditionalType.In => ($"@0.Contains(\"{this.FieldName}\")", this.FieldName, this.FieldValue.Split(",")),
            ConditionalType.NotIn => ($"!@0.Contains(\"{this.FieldName}\")", this.FieldName, this.FieldValue.Split(",")),
            ConditionalType.LikeLeft => ($"{this.FieldName}.StartsWith(@0)", this.FieldName, this.FieldValue),
            ConditionalType.LikeRight => ($"{this.FieldName}.EndsWith(@0)", this.FieldName, this.FieldValue),
            ConditionalType.NoEqual => ($"{this.FieldName}!={this.FieldValue}", this.FieldName, this.FieldValue),
            ConditionalType.IsNullOrEmpty => ($"{this.FieldName}.IsNullOrEmpty(@0)", this.FieldName, this.FieldValue),
            ConditionalType.IsNot => ($"{this.FieldName} is not null", this.FieldName, this.FieldValue),
            ConditionalType.NoLike => ($"!{this.FieldName}.Contains(@0)", this.FieldName, this.FieldValue),
            ConditionalType.EqualNull => throw new NotImplementedException(),
            ConditionalType.InLike => throw new NotImplementedException(),
            _ => (string.Empty, string.Empty, null)
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


}