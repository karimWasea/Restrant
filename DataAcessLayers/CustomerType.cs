//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//using static C_Utilities.Enumes;

//namespace DataAcessLayers
//{
//    public class CustomerType
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }
//        public DateTime CreationTime { get; set; } = DateTime.Now;
//         public int? Types { get; set; }  
//         public string? TypesName { get; set; }  
//          public Applicaionuser ApplicationUser { get; set; }
//        public ICollection<PriceProductebytypes> PriceProductebytypes { get; set; }


//        public string SystemUserId { get; set; }
//        public string SystemUserName { get; set; }

//    }

//}
