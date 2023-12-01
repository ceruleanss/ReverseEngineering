using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reverse.Helper
{
    [Keyless]
	public class InformationSchema
	{
        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; set; }
        [Column("TABLE_CATALOG")]
        public string TableCatalog { get; set; } = null!;
        [Column("TABLE_SCHEMA")]
        public string TableSchema { get; set; } = null!;
        [Column("TABLE_NAME")]
        public string TableName { get; set; } = null!;
        [Column("COLUMN_NAME")]
        public string? ColumnName { get; set; }
        [Column("COLUMN_DEFAULT")]
        public string? ColumnDefault { get; set; }
        [Column("IS_NULLABLE")]
        public string IsNullable { get; set; } = null!;
        [Column("DATA_TYPE")]
        public string DataType { get; set; } = null!;
        [Column("CHARACTER_MAXIMUM_LENGTH")]
        public int? CharacterMaximumLength { get; set; }
        [Column("NUMERIC_PRECISION")]
        public byte? NumericPrecision { get; set; }
        [Column("NUMERIC_PRECISION_RADIX")]
        public short? NumericPrecisionRadix { get; set; }
        [Column("NUMERIC_SCALE")]
        public int? NumericScale { get; set; }
        [Column("DATETIME_PRECISION")]
        public short? DatetimePrecision { get; set; }
    }
}

