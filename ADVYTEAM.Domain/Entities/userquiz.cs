namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userquiz")]
    public partial class userquiz
    {
        public long id { get; set; }

        public int? currentQuestionIndex { get; set; }

        public int score { get; set; }

        public long? quiz_id { get; set; }

        public long? user_id { get; set; }

        public virtual quiz quiz { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
