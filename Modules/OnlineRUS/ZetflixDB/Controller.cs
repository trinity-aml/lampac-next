using Microsoft.AspNetCore.Mvc;
using Shared;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ZetflixDB
{
    public class ZetflixDBController : BaseOnlineController
    {
        public ZetflixDBController() : base(ModInit.conf) { }

        [HttpGet]
        [Route("lite/zetflixdb")]
        async public Task<ActionResult> Index(long kinopoisk_id, string title, string original_title, bool rjson = false)
        {
            if (kinopoisk_id == 0)
                return OnError();

            if (await IsRequestBlocked(rch: true))
                return badInitMsg;

            string uri = $"{init.apihost}/embed/AO/kinopoisk/{Encode(kinopoisk_id)}/";

            return LocalRedirect(accsArgs($"/lite/videodb?uri={EncryptQuery(uri)}"));
        }

        static string Encode(long kinopoisk_id)
        {
            // 1. String -> Base64
            byte[] bytes = Encoding.UTF8.GetBytes(kinopoisk_id.ToString());
            string base64 = Convert.ToBase64String(bytes);

            // 2. Remove '=' padding
            string trimmed = base64.TrimEnd('=');

            // 3. Reverse string
            char[] arr = trimmed.ToCharArray();
            Array.Reverse(arr);

            return new string(arr);
        }
    }
}
