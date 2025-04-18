﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Entity.Entities
{
    [Table("staff", Schema = "dbo")]
    public class Staff
    {
        [Key]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("start_date")]
        public DateOnly StartDate { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; } = new List<FeedbackReply>();

        public virtual ICollection<ConsultingForm> ConsultingForms { get; set; } = new List<ConsultingForm>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
