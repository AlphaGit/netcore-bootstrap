using System.ComponentModel.DataAnnotations;

namespace Alpha.Bootstrap.WebApi.Dtos.v1
{
    public class CreatePostRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
