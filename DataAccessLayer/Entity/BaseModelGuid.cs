using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class BaseModelGuid
    {
        public BaseModelGuid()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "Id")]
        [Key]
        public Guid Id { get; set; }




        #region  Relations



        #endregion

        #region ICollection



        #endregion

    }
}
