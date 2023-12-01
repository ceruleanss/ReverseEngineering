using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reverse.Objects;

[Table("tp_batch")]
public class TpBatch
{
	[Column("id")]
	public string Id { get; set; } = null!;
	[Column("number")]
	public string Number { get; set; } = null!;
	[Column("name")]
	public string Name { get; set; } = null!;
	[Column("date")]
	public DateTime Date { get; set; }
	[Column("customer_id")]
	public string? CustomerId { get; set; }
	[Column("insurance_id")]
	public string? InsuranceId { get; set; }
	[Column("qty_debitur")]
	public int QtyDebitur { get; set; }
	[Column("total_premi")]
	public decimal TotalPremi { get; set; }
	[Column("description")]
	public string? Description { get; set; }
	[Column("is_reconsile")]
	public byte? IsReconsile { get; set; }
	[Column("reconsile_id")]
	public string? ReconsileId { get; set; }
	[Column("reconsile_amount")]
	public decimal? ReconsileAmount { get; set; }
	[Column("reconsile_date")]
	public DateTime? ReconsileDate { get; set; }
	[Column("status")]
	public byte Status { get; set; }
	[Column("posted_at")]
	public DateTime? PostedAt { get; set; }
	[Column("posted_by")]
	public string? PostedBy { get; set; }
	[Column("is_set_insurance")]
	public byte? IsSetInsurance { get; set; }
	[Column("is_old_batch")]
	public byte IsOldBatch { get; set; }
	[Column("created_at")]
	public DateTime CreatedAt { get; set; }
	[Column("created_by")]
	public string CreatedBy { get; set; } = null!;
	[Column("updated_at")]
	public DateTime? UpdatedAt { get; set; }
	[Column("updated_by")]
	public string? UpdatedBy { get; set; }
	[Column("deleted_at")]
	public DateTime? DeletedAt { get; set; }
	[Column("deleted_by")]
	public string? DeletedBy { get; set; }
}