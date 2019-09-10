using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpfCoreDemo.Data.Model
{
    public class Tweet
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string TwitterId { get; set; }

        [StringLength(500)]
        public string AuthorImageUrl { get; set; }

        [StringLength(50)]
        public string AuthorName { get; set; }

        [StringLength(500)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

    }
}
