using System.ComponentModel.DataAnnotations;

namespace Mt.Fruit.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage ="名称不能为空")]
        [StringLength(2000, ErrorMessage = "名称最大为60个字符")]
        public string Name { get; set; }

        public string Code { get; set; }

        [StringLength(2000,ErrorMessage ="描述最大为2000个字符")]
        public string Descript { get; set; }

        public string SmallPic { get; set; }

        public string BigPic { get; set; }

        public int Fllows { get; set; }

        public byte Status { get; set; }
    }
}
