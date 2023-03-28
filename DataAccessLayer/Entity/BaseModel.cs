using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class BaseModel
    {
        public BaseModel()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "Id")]
        [Key]
        public int Id { get; set; }



        #region  Relations



        #endregion

        #region ICollection



        #endregion


    }
}
